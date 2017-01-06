//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Net;
//using System.Net.Sockets;

//namespace NF
//{
//	public class UnicastReceiver : Receiver
//    {
//        public UnicastReceiver(int bufferSize):base(bufferSize)
//		{
//        }

//        override public void Connect(IPAddress ip, int port)
//		{
//			m_Address = ip;
//			m_Port = port;
//			m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); 
//			m_Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
//			m_EndPoint = new IPEndPoint(IPAddress.Any, m_Port);
//			m_Socket.Bind(m_EndPoint);
//			//m_Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(m_Address, IPAddress.Any));
//			IsConnected = true;
//			DoRead();
//		}
//	}
//}
