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
    abstract class CiscoObject
    {
        //static void Add(string xml)
        //{

        //}

        //public static void Add(CiscoObject cisco_object)
        //{
        //    cisco_object.add();
        //}

        readonly static List<CiscoObject> cisco_objects = new List<CiscoObject>();

        //readonly static LiteDatabase db = new LiteDatabase(@"MyData.db");
        
        protected CiscoObject(string xml)
        {
            Xml = xml;
            CreateTime = DateTime.Now;
            DeleteTime = DateTime.MinValue;
        }

        protected void add2collection()
        {
            lock (cisco_objects)
            {
                cisco_objects.Add(this);
            }
            CiscoObjectsWindow.AddToTable(this);
            Activate();

            forget_old();
        }

        ~CiscoObject()
        {
        }

        readonly public string Xml;
        readonly public DateTime CreateTime;

        internal abstract void Activate();

        protected abstract void Deleting();

        internal void Delete()
        {
            Deleting();
            CiscoObjectsWindow.DeleteFromTable(this);
            //lock (notifications)
            //{
            //    notifications.Remove(this);
            //}
            DeleteTime = DateTime.Now;
            CiscoObjectsWindow.EnableRestore(true);
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
            CiscoObject n;
            lock (cisco_objects)
            {
                n = cisco_objects.Where(x => x.Deleted).OrderByDescending(x => x.DeleteTime).FirstOrDefault();
                if (n == null)
                {
                    CiscoObjectsWindow.EnableRestore(false);
                    return;
                }
            }
            n.DeleteTime = DateTime.MinValue;
            CiscoObjectsWindow.AddToTable(n);
            CiscoObjectsWindow.EnableRestore(cisco_objects.Where(x => x.Deleted).Count() > 0);
        }

        static void forget_old()
        {
            lock (cisco_objects)
            {
                DateTime forget_t = DateTime.Now.AddDays(-Settings.Default.ForgetNotificationsOlderThanDays);
                var ns = cisco_objects.Where(x => x.CreateTime < forget_t).ToList();
                foreach (CiscoObject n in ns)
                    cisco_objects.Remove(n);
            }
        }
    }
}