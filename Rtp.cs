//********************************************************************************************
//Author: Sergey Stoyan 
//        sergey.stoyan@gmail.com
//        sergey_stoyan@yahoo.com
//        http://www.cliversoft.com
//        16 October 2007
//Copyright: (C) 2006-2007, Sergey Stoyan
//********************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Web;
using System.Xml;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using LumiSoft.Net.RTP;
using LumiSoft.Net.Media;
using LumiSoft.Net.Media.Codec.Audio;

namespace Cliver.CisteraNotification
{
    class Rtp
    {
        static Rtp()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="volume">The supplied value is a percentage of the maximum volume level of the device and must be in the range 0-100.</param>
        /// <returns></returns>
        static public Status Play(bool multicast, IPAddress source_ip, int port, uint? volume100 = null)
        {
            if (session != null)
            {
                if(!Message.YesNo("Would you like to drop the previous stream?"))
                    return Status.BUSY;
                session.Stop();
                session.Dispose();
            }

            Rtp.source_ip = source_ip;
            Rtp.volume100 = volume100;
            Rtp.multicast = multicast;
            session = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
            if(multicast)
                session.CreateMulticastSession(null, new RTP_Clock(0, 8000), new RTP_Address(source_ip, port, port + 1));
            else
                session.CreateSession(new RTP_Address(IPAddress.Any, port, port + 1), new RTP_Clock(0, 8000));
            session.Sessions[0].NewReceiveStream += new EventHandler<RTP_ReceiveStreamEventArgs>(m_pRtpSession_NewReceiveStream);
            session.Sessions[0].Payload = payload;
            session.Sessions[0].Start();

            return Status.ACCEPTED;
        }
        static int payload = 0;//8;
        static RTP_MultimediaSession session = null;
        static IPAddress source_ip = null;
        static uint? volume100 = null;
        static bool multicast = false;

        static private void m_pRtpSession_NewReceiveStream(object sender, RTP_ReceiveStreamEventArgs e)
        {
            try
            {
                //for unicast make sure that the stream is from the expected source ip 
                if (!multicast && !e.Stream.SSRC.RtpEP.Address.Equals(source_ip))
                    return;
                AudioOutDevice device = AudioOut.Devices.Where(d => d.Name == Settings.Default.AudioDeviceName).FirstOrDefault();
                if (device == null)
                {
                    Message.Error("Could not find audio device: " + Settings.Default.AudioDeviceName);
                    return;
                }
                AudioCodec ac = new PCMU();
                ao = new AudioOut_RTP(
                    device,
                    e.Stream,
                    new Dictionary<int, AudioCodec> { { payload, ac } }
                    );
                Dictionary<AudioCodec, string> acs2of = new Dictionary<AudioCodec, string>();
                if (Settings.Default.RecordIncomingRtpStreams)
                    acs2of[ac] = PathRoutines.CreateDirectory(Settings.Default.RtpStreamStorageFolder) + "\\" + e.Stream.SSRC.RtpEP.Address.ToString() + DateTime.Now.ToString("_yyyy-MM-dd_HH-mm-ss") + ".wav";
                ao.Start(volume100, acs2of);
            }
            catch(Exception ex)
            {
                Message.Error(ex);
            }
        }
        static AudioOut_RTP ao;

        static public void Stop()
        {
            if (session != null)
            {
                ao.Stop();
                ao.Dispose();
                ao = null;
                session.Close("Closed.");
                session.Dispose();
                session = null;
            }
        }

        public enum Status
        {
            BUSY,
            ACCEPTED
        }
    }
}