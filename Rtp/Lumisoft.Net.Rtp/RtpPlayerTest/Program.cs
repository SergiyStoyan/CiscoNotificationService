using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumiSoft.Net.RTP;
using LumiSoft.Net.Media;
using LumiSoft.Net.Media.Codec.Audio;
using System.Net;

namespace TestReceive
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            //string ip = "224.0.0.1";
            int port = 20700;




            RTP_MultimediaSession session = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
            session.CreateMulticastSession(null, new RTP_Clock(0, 8000), new RTP_Address(IPAddress.Parse(ip), port, port + 1));
            //session.CreateSession(new RTP_Address(IPAddress.Any, port, port + 1), new RTP_Clock(0, 8000));
            session.Sessions[0].NewReceiveStream += new EventHandler<RTP_ReceiveStreamEventArgs>(m_pRtpSession_NewReceiveStream);
            session.Sessions[0].Payload = payload;
            session.Sessions[0].Start();
            

            Console.ReadKey();
        }
        static int payload = 0;//8;

       static private void m_pRtpSession_NewReceiveStream(object sender, RTP_ReceiveStreamEventArgs e)
        {
            //if(e.Stream.Session.StunPublicEndPoints)
            //e.Stream.Session.Stop();
            foreach (AudioOutDevice device in AudioOut.Devices)
            {
                //WavePlayer wp = new WavePlayer(device);
                //wp.Play(,)
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
    }
}
