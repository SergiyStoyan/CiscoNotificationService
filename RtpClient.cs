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
using System.Net;
using System.Web;
using System.Xml;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Cliver.CisteraNotification
{
    class RtpClient
    {
        static RtpClient()
        {
        }

        static public Status Play(IPAddress ip, uint port, uint? volume = null)
        {
            return Status.BUSY;
        }

        static public void Stop()
        {
        }

        public enum Status
        {
            BUSY,
            ACCEPTED
        }
    }
}