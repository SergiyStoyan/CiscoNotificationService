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
using Bonjour;

namespace Cliver
{
    class BonjourService
    {
        static BonjourService()
        {
            Microsoft.Win32.SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

            eventManager = new DNSSDEventManager();
            eventManager.ServiceRegistered += EventManager_ServiceRegistered;
            eventManager.ServiceFound += EventManager_ServiceFound;
            eventManager.ServiceLost += EventManager_ServiceLost;
            eventManager.ServiceResolved += EventManager_ServiceResolved;
            eventManager.QueryRecordAnswered += EventManager_QueryRecordAnswered;
            eventManager.OperationFailed += EventManager_OperationFailed;
        }

        private static void EventManager_OperationFailed(DNSSDService service, DNSSDError error)
        {
            throw new NotImplementedException();
        }

        private static void EventManager_QueryRecordAnswered(DNSSDService service, DNSSDFlags flags, uint ifIndex, string fullname, DNSSDRRType rrtype, DNSSDRRClass rrclass, object rdata, uint ttl)
        {
            throw new NotImplementedException();
        }

        private static void EventManager_ServiceResolved(DNSSDService service, DNSSDFlags flags, uint ifIndex, string fullname, string hostname, ushort port, TXTRecord record)
        {
            throw new NotImplementedException();
        }

        private static void EventManager_ServiceLost(DNSSDService browser, DNSSDFlags flags, uint ifIndex, string serviceName, string regtype, string domain)
        {
            throw new NotImplementedException();
        }

        private static void EventManager_ServiceFound(DNSSDService browser, DNSSDFlags flags, uint ifIndex, string serviceName, string regtype, string domain)
        {
            throw new NotImplementedException();
        }

        private static void EventManager_ServiceRegistered(DNSSDService service, DNSSDFlags flags, string name, string regtype, string domain)
        {
        }

        static public void Start()
        {
            try
            {
                if(!Properties.Settings.Default.Run)
                    return;
                Stop();
                string service_name = Properties.Settings.Default.UseWindowsUserAsServiceName ? Environment.UserName : Properties.Settings.Default.ServiceName;
                if (string.IsNullOrWhiteSpace(service_name))
                    service_name = "-UNKNOWN-";
                service = new DNSSDService();
                //_rfb._tcp
                if (null == service.Register(0, 0, service_name, "_cisterarb._tcp", null, null, (ushort)Properties.Settings.Default.ServicePort, null, eventManager))
                    throw new Exception("Register returned NULL.");
            }
            catch(Exception e)
            {
                Message.Error("Bonjour Service is not available: " + e.Message);
                Application.Exit();
            }
        }

        private static void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            Start();
        }
        
        static DNSSDService service = null;
        static readonly DNSSDEventManager eventManager = null;

        static public void Stop()
        {
            service?.Stop();
            service = null;
        }
    }
}