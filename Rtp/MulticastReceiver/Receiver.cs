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

        protected Socket socket;
        protected IPAddress ip;
        protected Int32 port;
        protected EndPoint endPoint;
        Byte[] bytes;

        public delegate void DelegateDataReceived2(NF.Receiver mc, Byte[] bytes);
        public delegate void DelegateDisconnected(string Reason);
        public delegate void DelegateExceptionAppeared(Exception ex);
        public event DelegateDataReceived2 DataReceived2;
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
            multicast = IsMulticast(ip);
            this.ip = ip;
            this.port = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            endPoint = new IPEndPoint(IPAddress.Any, port);
            socket.Bind(endPoint);
            if (multicast)
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
            connected = true;
            DoRead();
        }

        protected void DoRead()
        {
            try
            {
                socket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnDataReceived), socket);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void OnDataReceived(IAsyncResult ar)
        {
            try
            {
                if (connected)
                {
                    //Anzahl gelesener Bytes
                    int read = socket.EndReceive(ar);

                    //Wenn Daten vorhanden
                    if (read > 0)
                    {
                        //Wenn das Event verwendet wird
                        if (this.DataReceived2 != null)
                        {
                            //Wenn gelesene Bytes und Datengrösse übereinstimmen
                            if (read == bytes.Length)
                            {
                                //Event abschicken
                                this.DataReceived2(this, bytes);
                            }
                            else
                            {
                                //Nur den gelesenen Teil übermitteln
                                Byte[] readed = new Byte[read];
                                Array.Copy(bytes, readed, read);

                                //Event abschicken
                                this.DataReceived2(this, readed);
                            }
                        }

                        //Weiterlesen
                        socket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnDataReceived), null);
                    }
                    else
                    {
                        //Verbindung beendet
                        socket.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("MulticastReceiver.cs | OnDataReceived() | {0}", ex.Message));
                //Verbindung beenden
                Disconnect();
            }
        }

        public void Disconnect()
        {
            if (connected)
            {
                socket.Close();
                Disconnected?.Invoke("Connection has been finished");
                connected = false;
            }
        }
    }
}