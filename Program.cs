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
using System.Threading;
using System.Text.RegularExpressions;

namespace Cliver.CisteraNotification
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
                //NotificationForm nf = NotificationForm.This;
                //NotificationForm.AddNotification("title", "message", "test.png", "action", null);
                // NotificationForm.Clear();
                //for (int i = 0; i < 4; i++)
                //    NotificationForm.AddNotification("title" + i, "message", "https://www.google.com/logos/doodles/2016/holidays-2016-day-1-5727116688621568.2-res.png", "action", () => { });
                //NotificationForm.AddNotification("title" + i, "message", "test.png", "action", () => { Message.Inform("clicked"); });

                //AlertForm a = AlertForm.AddAlert("dsgfsdg", "sdgfsdgsgfdsgdf", "test.png", "action", () => { Message.Inform("clicked"); });
                //a.Close();
                //BrowserForm bf = new BrowserForm();
                //bf.Show();

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

        internal static bool IsServiceRunning
        {
            get
            {
                return BonjourService.Running && HttpService.Running;
            }
        }
    }
}