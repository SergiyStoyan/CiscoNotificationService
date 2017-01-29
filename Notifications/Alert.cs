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
    class Alert : CiscoObject
    {
        internal Alert(string xml, string title, string text, string image_url, string action_name, Action action):base(xml)
        {
            Title = title;
            Text = text;
            ImageUrl = image_url;
            ActionName = action_name;
            Action = action;

            add2collection();
        }
        readonly public string Title;
        readonly public string Text;
        readonly public string ImageUrl;
        readonly public string ActionName;
        readonly public Action Action;

        internal override void Activate()
        {
            Deleting();
            w = AlertWindow.Create(Title, Text, ImageUrl, ActionName, Action);
        }
        AlertWindow w = null;

        protected override void Deleting()
        {
            if (w != null)
                w.Invoke(() =>
                {
                    try
                    {
                        w.Close();
                    }
                    catch { }
                    w = null;
                });
        }
    }
}