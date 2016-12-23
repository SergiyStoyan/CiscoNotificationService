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
using System.Net;

namespace Cliver
{
    static class Program
    {
        static Program()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs args)
            {
                Exception e = (Exception)args.ExceptionObject;
                Message.Error(e);
                Application.Exit();
            };
        }

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                //NotificationForm nf = new NotificationForm();
                //nf.Show();
                //for (int i = 0; i < 4; i++)
                //{
                //    var c = new NotificationControl("t"+i, "text "+i, "action " + i, ()=> { Message.Inform("clicked"); });
                //    c.Dock = DockStyle.Top;
                //    nf.Controls.Add(c);
                //    c.BringToFront();
                //}

                BrowserForm bf = new BrowserForm();
                bf.Show();

                Application.Run(SysTray.This);
            }
            catch (Exception e)
            {
                Message.Error(e);
            }
            finally
            {
                Exit();
            }
        }

        static public void Exit()
        {
            Application.Exit();
            Environment.Exit(0);
        }

        internal static void UpdateService()
        {
            if (Properties.Settings.Default.Run)
            {
                string service_name = Properties.Settings.Default.UseWindowsUserAsServiceName ? Environment.UserName : Properties.Settings.Default.ServiceName;
                if (string.IsNullOrWhiteSpace(service_name))
                    service_name = "-UNKNOWN-";
                HttpService.Start(service_name, Properties.Settings.Default.ServicePort);
                BonjourService.Start(HttpService.Name, HttpService.Port);             
            }
            else
            {
                BonjourService.Stop();
                HttpService.Stop();
            }
        }
    }
}