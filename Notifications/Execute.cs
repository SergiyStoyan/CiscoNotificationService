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
using System.Diagnostics;

namespace Cliver.CisteraNotification
{
    /// <summary>
    /// CiscoIPPhoneExecute
    /// </summary>
    class Execute : CiscoObject
    {
        internal Execute(string xml) : base(xml)
        {

        }

        internal void SetFile(string file)
        {
            this.file = file;
        }
        string file = null;

        internal override void Activate()
        {
            try
            {
                if (file != null)
                    p = Process.Start(file);
            }
            catch (Exception e)
            {
                Message.Error(e);
            }
        }
        Process p = null;

        protected override void Deleting()
        {
            try
            {
                p?.Kill();
            }
            catch { }
            try
            {
                p?.Dispose();
            }
            catch { }
            p = null;

            if (file != null)
                File.Delete(file);
        }
    }
}