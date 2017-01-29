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
            add2collection();
        }

        internal string Record
        {
            set
            {
                if (record != null)
                    throw new Exception("file is set already.");
                record = value;
                ((CiscoObjectExecuteControl)CiscoObjectsWindow.GetControl(this)).FileEnabled = true;
            }
            get
            {
                return record;
            }
        }
        string record = null;

        internal override void Activate()
        {
            try
            {
                if (record != null)
                    p = Process.Start(record);
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
        }

        override protected void Clean()
        {
            if (record != null && File.Exists(record))
                File.Delete(record);
        }
    }
}