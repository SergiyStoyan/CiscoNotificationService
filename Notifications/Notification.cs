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
//using LiteDB;

namespace Cliver.CisteraNotification
{
    abstract class Notification
    {
        //internal static Notification[] Notifications
        //{
        //    get
        //    {
        //        lock (notifications)
        //            return notifications.Where(n => !n.Deleted).ToArray();
        //    }
        //}

        //static internal bool IsDeleted
        //{
        //    get
        //    {
        //        return notifications.Where(x => x.Deleted).Count() > 0;
        //    }
        //}

        readonly static List<Notification> notifications = new List<Notification>();

        //readonly static LiteDatabase db = new LiteDatabase(@"MyData.db");
        
        protected Notification(string title, string text, string image_url, string action_name, Action action)
        {
            Title = title;
            Text = text;
            ImageUrl = image_url;
            ActionName = action_name;
            Action = action;
            CreateTime = DateTime.Now;
            DeleteTime = DateTime.MinValue;
            lock (notifications)
            {
                notifications.Add(this);
            }
            NotificationsWindow.AddToTable(this);
            Show();

            forget_old();
        }

        ~Notification()
        {
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
            //lock (notifications)
            //{
            //    notifications.Remove(this);
            //}
            DeleteTime = DateTime.Now;
            NotificationsWindow.EnableRestore(true);
        }

        public bool Deleted
        {
            get
            {
                return DeleteTime > DateTime.MinValue;
            }
        }
        public DateTime DeleteTime { get; private set; }

        static internal void RestoreLastDeleted()
        {
            Notification n;
            lock (notifications)
            {
                n = notifications.Where(x => x.Deleted).OrderByDescending(x => x.DeleteTime).FirstOrDefault();
                if (n == null)
                {
                    NotificationsWindow.EnableRestore(false);
                    return;
                }
            }
            n.DeleteTime = DateTime.MinValue;
            NotificationsWindow.AddToTable(n);
            NotificationsWindow.EnableRestore(notifications.Where(x => x.Deleted).Count() > 0);
        }

        static void forget_old()
        {
            lock (notifications)
            {
                DateTime forget_t = DateTime.Now.AddDays(-Settings.Default.ForgetNotificationsOlderThanDays);
                var ns = notifications.Where(x => x.CreateTime < forget_t).ToList();
                foreach (Notification n in ns)
                    notifications.Remove(n);
            }
        }
    }
}