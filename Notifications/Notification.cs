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
    abstract class Notification
    {
        internal static Notification[] Notifications
        {
            get
            {
                lock (notifications)
                    return notifications.ToArray();
            }
        }

        readonly static List<Notification> notifications = new List<Notification>();

        protected Notification(string title, string text, string image_url, string action_name, Action action)
        {
            Title = title;
            Text = text;
            ImageUrl = image_url;
            ActionName = action_name;
            Action = action;
            CreateTime = DateTime.Now;
            lock (notifications)
            {
                notifications.Add(this);
            }
            NotificationsWindow.AddToTable(this);
            Show();
        }

        ~Notification()
        {
            Delete();
        }

        readonly public string Title;
        readonly public string Text;
        readonly public string ImageUrl;
        readonly public string ActionName;
        readonly public Action Action;
        readonly public DateTime CreateTime;

        internal abstract void Show();

        protected abstract void Deleting();

        internal void Delete()
        {
            Deleting();
            NotificationsWindow.DeleteFromTable(this);
            lock (notifications)
            {
                notifications.Remove(this);
            }
        }
    }
}