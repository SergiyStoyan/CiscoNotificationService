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

namespace Cliver.CisteraNotification
{
    class RtpClient
    {
        static RtpClient()
        {
        }

        static public String SoundDeviceName = "";
        readonly static public int SamplesPerSecond = 8000;//G.711
        readonly static public short BitsPerSample = 8;//G.711
        static public short Channels = 2;
        readonly static public Int32 PacketSize = 4096;
        static public Int32 BufferCount = 8;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="volume">The supplied value is a percentage of the maximum volume level of the device and must be in the range 0-100.</param>
        /// <returns></returns>
        static public Status Play(IPAddress ip, int port, uint? volume100 = null)
        {
            if (player.Opened)
                return Status.BUSY;

            receiver = new NF.Receiver(PacketSize);
            receiver.DataReceived2 += new NF.Receiver.DelegateDataReceived2(OnDataReceived);
            receiver.Disconnected += new NF.Receiver.DelegateDisconnected(OnDisconnected);
            receiver.Connect(ip, port);

            uint? v = null;
            if (volume100 != null)
            {
                if (volume100 > 100)
                    volume100 = 100;
                v = (uint)((float)volume100 / 100 * 0xFFFF);
            }
            player.Open(SoundDeviceName, SamplesPerSecond, BitsPerSample, Channels, BufferCount, v);
            return Status.ACCEPTED;
        }
        readonly static WinSound.Player player = new WinSound.Player();
        static NF.Receiver receiver = null;

        static private void OnDataReceived(NF.Receiver mc, Byte[] bytes)
        {
            try
            {
                WinSound.RTPPacket rtp = new WinSound.RTPPacket(bytes);
                if (rtp.Data != null)
                {
                    Byte[] linearBytes = WinSound.Utils.MuLawToLinear(rtp.Data, BitsPerSample, Channels);
                    player.PlayData(linearBytes, false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | OnDataReceived() | {0}", ex.Message));
            }
        }

        static private void OnDisconnected(string reason)
        {
            try
            {
                player.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | OnDisconnected() | {0}", ex.Message));
            }
        }

        static public void Stop()
        {
            if (receiver != null)
            {
                receiver.Disconnect();
                receiver.DataReceived2 -= new NF.Receiver.DelegateDataReceived2(OnDataReceived);
                receiver.Disconnected -= new NF.Receiver.DelegateDisconnected(OnDisconnected);
                receiver = null;
            }
            if (player != null)
            {
                player.Close();
                //player = null;
            }
        }

        public enum Status
        {
            BUSY,
            ACCEPTED
        }
    }
}