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
                return Status.BUSY;

            Rtp.source_ip = source_ip;
            Rtp.volume100 = volume100;
            session = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
            if(multicast)
                session.CreateMulticastSession(null, new RTP_Clock(0, 8000), new RTP_Address(source_ip, port, port + 1));
            else
                //TBD: for unicast we have to check source ip when accept a stream
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

        static private void m_pRtpSession_NewReceiveStream(object sender, RTP_ReceiveStreamEventArgs e)
        {
            //TBD: for unicast we have to check source ip 
            //if(e.Stream.Session.StunPublicEndPoints)
            //e.Stream.Session.Stop();
            foreach (AudioOutDevice device in AudioOut.Devices)
            {
                ao = new AudioOut_RTP(
                    device,
                   e.Stream,
                    new Dictionary<int, AudioCodec> { { payload, new PCMA() } }
                    );
                ao.Start(volume100);
                break;
            }
        }
        static AudioOut_RTP ao;

        static public void Stop()
        {
            if (session != null)
            {
                session.Close("Closed.");
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