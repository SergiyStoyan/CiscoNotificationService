using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NF
{
    public class Receiver
    {
        public static bool IsMulticast(IPAddress ip)
        {
            try
            {
                byte[] bs = ip.GetAddressBytes();
                if (bs[0] >= 224 && bs[0] <= 239)
                    return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return false;
        }

        public Receiver(int bufferSize)
        {
            bytes = new Byte[bufferSize];
        }

        protected Socket socket = null;
        protected IPAddress ip;
        protected Int32 port;
        protected EndPoint endPoint;
        Byte[] bytes;

        public delegate void DelegateDataReceived(NF.Receiver mc, Byte[] bytes, int size);
        public delegate void DelegateDisconnected(string Reason);
        public delegate void DelegateExceptionAppeared(Exception ex);
        public event DelegateDataReceived DataReceived;
        public event DelegateDisconnected Disconnected;

        public bool Connected
        {
            get
            {
                return connected;
            }
        }
        bool connected = false;

        public bool Multicast
        {
            get
            {
                return multicast;
            }
        }
        bool multicast = false;

        public void Connect(IPAddress ip, int port)
        {
            this.ip = ip;
            this.port = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            multicast = IsMulticast(ip);
            if (multicast)
            {
                endPoint = new IPEndPoint(IPAddress.Any, port);
                socket.Bind(endPoint);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
            }
            else
            {
                endPoint = new IPEndPoint(ip, port);
                socket.Bind(endPoint);
            }
            socket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(received), socket);
        }

        private void received(IAsyncResult ar)
        {
            try
            {
                int size = socket.EndReceive(ar);                
                if (size > 0)
                {
                    DataReceived?.Invoke(this, bytes, size);
                    socket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(received), null);
                }
                else
                    socket.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("MulticastReceiver.cs | OnDataReceived() | {0}", ex.Message));
                Disconnect();                
            }
        }

        public void Disconnect()
        {
            if (socket != null)
            {
                socket.Close();
                socket = null;
                Disconnected?.Invoke("Connection has been finished");
            }
        }
    }
}