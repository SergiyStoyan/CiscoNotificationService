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

namespace Cliver
{
    class HttpService
    {
        static HttpService()
        {
        }
        
        static public void Start()
        {
            t = ThreadRoutines.StartTry(() => { run(Properties.Settings.Default.ServicePort); });
        }
        static Thread t = null;

        static void run(int port)
        {
            try
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener.Close();
                }
                listener = new HttpListener();
                listener.Prefixes.Add("http://*:" + port + "/");
                listener.Start();
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    ThreadPool.QueueUserWorkItem((o) => { request_handler(context); });
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception e)
            {
                Message.Error("Http Service broken: " + e.Message);
                Application.Exit();
            }
            finally
            {
                listener.Stop();
                listener.Close();
                listener = null;
            }
        }
        static HttpListener listener = null;

        static public void Stop()
        {
            if (t != null && t.IsAlive)
                t.Abort();
            t = null;
        }

        static private void request_handler(object state)
        {
            var context = (HttpListenerContext)state;
            HttpListenerResponse response = context.Response;
            try
            {
                HttpListenerRequest request = context.Request;
                response.StatusCode = 200;
                string responseString = "OK";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            catch (Exception)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                response.Close();
            }
        }
    }
}