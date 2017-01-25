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
    /// <summary>
    /// CiscoIPPhoneExecute
    /// </summary>
    class Execute : Notification
    {
        internal Execute(string url) : base(null, null, null, null, null)
        {
            
        }

        internal override void Show()
        {//play
        }

        protected override void Deleting()
        {
            //try
            //{
            //    w?.Close();
            //}
            //catch { }
            //w = null;
        }
    }
}