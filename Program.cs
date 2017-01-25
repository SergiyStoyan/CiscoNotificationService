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

//TBD:
//- long audio device names
//- storing notifications to disk
//- 
//- 


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
                NotificationsWindow.Initialize();

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
            if (Settings.Default.Run)
            {
                string service_name = Settings.Default.UseWindowsUserAsServiceName ? Environment.UserName : Settings.Default.ServiceName;
                if (string.IsNullOrWhiteSpace(service_name))
                    service_name = "-UNKNOWN-";
                HttpService.Start(service_name, Settings.Default.ServicePort);
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