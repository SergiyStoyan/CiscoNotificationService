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

        //static public String SoundDeviceName = "";
        readonly static public int SamplesPerSecond = 8000;//G.711
        //readonly static public short BitsPerSample = 8;//G.711
        //static public short Channels = 2;
        //readonly static public Int32 MaxUdpPacketSize = 10000;
        //static public Int32 BufferCount = 8;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="volume">The supplied value is a percentage of the maximum volume level of the device and must be in the range 0-100.</param>
        /// <returns></returns>
        static public Status Play(bool multicast, IPAddress source_ip, int port, uint? volume100 = null)
        {
            //if (player.Opened)
            if (sesson != null)
                return Status.BUSY;

            //receiver = new NF.Receiver(MaxUdpPacketSize);
            //receiver.DataReceived += new NF.Receiver.DelegateDataReceived(OnDataReceived);
            //receiver.Disconnected += new NF.Receiver.DelegateDisconnected(OnDisconnected);
            //receiver.Connect(source_ip, port);

            //uint? v = null;
            //if (volume100 != null)
            //{
            //    if (volume100 > 100)
            //        volume100 = 100;
            //    v = (uint)((float)volume100 / 100 * 0xFFFF);
            //}
            //player.Open(SoundDeviceName, SamplesPerSecond, BitsPerSample, Channels, BufferCount, v);
            Rtp.source_ip = source_ip;
            sesson = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
            if(multicast)
                sesson.CreateMulticastSession(new RTP_Clock(0, SamplesPerSecond), new RTP_Address(source_ip, port, port + 1));
            else
                sesson.CreateSession(new RTP_Address(IPAddress.Any, port, port + 1), new RTP_Clock(0, SamplesPerSecond));
            sesson.Sessions[0].NewReceiveStream += new EventHandler<RTP_ReceiveStreamEventArgs>(m_pRtpSession_NewReceiveStream);
            sesson.Sessions[0].Payload = payload;
            sesson.Sessions[0].Start();

            return Status.ACCEPTED;
        }
        static int payload = 0;//8;
        static RTP_MultimediaSession sesson = null;
        static IPAddress source_ip = null;

        static private void m_pRtpSession_NewReceiveStream(object sender, RTP_ReceiveStreamEventArgs e)
        {
            //if(e.Stream.Session.StunPublicEndPoints)
            //e.Stream.Session.Stop();
            foreach (AudioOutDevice device in AudioOut.Devices)
            {
                ao = new AudioOut_RTP(
                    device,
                   e.Stream,
                    new Dictionary<int, AudioCodec> { { payload, new PCMA() } }
                    );
                ao.Start();
                break;
            }
        }
        static AudioOut_RTP ao;

        //readonly static WinSound.Player player = new WinSound.Player();
        //static NF.Receiver receiver = null;

        //static private void OnDataReceived(NF.Receiver mc, Byte[] bytes, int size)
        //{
        //    try
        //    {
        //        WinSound.RTPPacket rtp = new WinSound.RTPPacket(bytes, size);
        //        if (rtp.Data != null)
        //        {
        //            Byte[] linearBytes = WinSound.Utils.MuLawToLinear(rtp.Data, BitsPerSample, Channels);
        //            player.PlayData(linearBytes, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | OnDataReceived() | {0}", ex.Message));
        //    }
        //}

        //static private void OnDisconnected(string reason)
        //{
        //    try
        //    {
        //        player.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | OnDisconnected() | {0}", ex.Message));
        //    }
        //}

        static public void Stop()
        {
            //if (receiver != null)
            //{
            //    receiver.Disconnect();
            //    receiver = null;
            //}
            //if (player != null)
            //{
            //    player.Close();
            //    //player = null;
            //}
            if (sesson != null)
            {
                sesson.Close("Closed.");
                sesson = null;
            }
        }

        public enum Status
        {
            BUSY,
            ACCEPTED
        }
    }
}