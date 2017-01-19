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
using System.Runtime.InteropServices;
using System.Net;
using Bonjour;

namespace Cliver.CisteraNotification
{
    static class Notifications
    {
        static Notifications()
        {
        }

        public static void Add(Notification notification)
        {
            lock (notifications)
            {
                notifications.Add(notification);
            }

            notification.Show();
        }

        public static void Delete(Notification notification)
        {
            lock (notifications)
            {
                notifications.Remove(notification);
            }

            notification.Deleting();
        }

        static List<Notification> notifications = new List<Notification>();
    }
}