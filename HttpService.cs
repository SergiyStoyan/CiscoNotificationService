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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Cliver
{
    class HttpService
    {
        static HttpService()
        {
        }

        static public void Start(string service_name, ushort port)
        {
            Stop();
            t?.Join();

#if DEBUG
            //Name = "*";//works in LAN
            //Name = "192.168.2.13";//the actual ip works in LAN
            //Name = "localhost";//not work in LAN
            //Name = "127.0.0.1";//not work in LAN
            //Name = "localhos";//some string not work in LAN
            //Name = "192.168.2.*";//does not start
            Name = "127.0.0.1";//not work in LAN
            Name = "localhost";//not work in LAN
#else
            Name = service_name;
            Name = "*";
#endif
            Port = port;
            t = ThreadRoutines.StartTry(() => { run(); });
        }
        static Thread t = null;

        public static string Name
        {
            get; private set;
        }

        public static ushort Port
        {
            get; private set;
        }

        static void run()
        {
            try
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener.Close();
                }
                listener = new HttpListener();
                listener.Prefixes.Add("http://" + Name + ":" + Port + "/");
                listener.Start();
                while (listener.IsListening)
                {
                    HttpListenerContext context = listener.GetContext();
                    ThreadPool.QueueUserWorkItem((o) => { request_handler(context); });
                }
            }
            catch (ThreadAbortException)
            { }
            catch (Exception e)
            {
                if (e is System.Net.HttpListenerException && ((System.Net.HttpListenerException)e).NativeErrorCode == 5)
                {
                    if(!Message.YesNo("Http Service requires the application to run with Administartor's privileges.\r\nRestart?"))
                        Environment.Exit(0);

                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.UseShellExecute = true;
                    psi.WorkingDirectory = Environment.CurrentDirectory;
                    psi.FileName = Application.ExecutablePath;
                    psi.Verb = "runas";
                    Process.Start(psi);
                    Environment.Exit(0);
                }

                Message.Error("Http Service broken: " + e.Message);
                Application.Exit();
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener.Close();
                    listener = null;
                }
            }
        }
        static HttpListener listener = null;

        static public void Stop()
        {
            if (listener != null)
            {
                listener.Stop();
                listener.Close();
                listener = null;
            }
            if (t != null && t.IsAlive)
                t.Abort();
            t = null;
        }

        static private void request_handler(object _context)
        {
            HttpListenerContext context = (HttpListenerContext)_context;
            HttpListenerResponse response = context.Response;
            try
            {
                HttpListenerRequest request = context.Request;
                if (!Regex.IsMatch(request.ContentType, @"application/x-www-form-urlencoded", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                {
                    response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    return;
                }
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ContentEncoding = request.ContentEncoding;
#if DEBUG 
                response.AddHeader("Access-Control-Allow-Origin", "*");//prevents errors when testing by ajax
#endif
                string data = CiscoHttp.ProcessRequest(context.Request);
                byte[] buffer = response.ContentEncoding.GetBytes(data);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string m;
                string d;
                Log.GetExceptionMessage(e, out m, out d);
                byte[] buffer = response.ContentEncoding.GetBytes(m + "\r\n<br>\r\n<br>" + d);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                response.Close();
            }
        }
    }
}