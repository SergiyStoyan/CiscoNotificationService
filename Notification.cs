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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net;
using System.Web;
//using System.Xml;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Cliver
{
    class Notification
    {
        static Notification()
        {
        }

        //static public void Inform(string title, string text, string prompt = null)
        //{
        //    //EMERGENCY, LOCKDOWN, ALERT, WARNING, #URGENT
        //}

        static public void Inform(string title, string text, string image_url = null, string action_name = null, Action action = null)
        {
            NotificationForm.AddNotification(title, text, image_url, action_name, action);
        }

        static public void Alert(string title, string text, string image_url = null, string action_name = null, Action action = null)
        {
            AlertForm.AddAlert(title, text, image_url, action_name, action);
        }
    }
}