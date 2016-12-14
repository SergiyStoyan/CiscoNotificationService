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
using System.Text.RegularExpressions;

namespace Cliver
{
    class CiscoHttp
    {
        static CiscoHttp()
        {
        }

        static public string ProcessRequest(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
                return get_CiscoIPPhoneError(Error.Parsing, "No data in request.");
            string data;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
            {
                data = reader.ReadToEnd();
            }
            Match m = Regex.Match(data, @"(^|&)xml\s*=\s*(?'Xml'.*)(&|$)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (!m.Success)
                return get_CiscoIPPhoneError(Error.Parsing, "No Xml in request.");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(HttpUtility.UrlDecode(m.Groups["Xml"].Value));

            return get_CiscoIPPhoneResponse("test");
        }

        static string get_CiscoIPPhoneResponse(string url, int status = 0, string data = "Success")
        {
            return @"<CiscoIPPhoneResponse>
<ResponseItem Status=""" + status + @"""
Data=""" + SecurityElement.Escape(data) + @"""
URL=""" + SecurityElement.Escape(url) + @"""/>
</CiscoIPPhoneResponse>";
        }

        static string get_CiscoIPPhoneError(Error error, string message = "")
        {
            return @"<CiscoIPPhoneError Number=""" + error + @"""/>" + SecurityElement.Escape(message) + @"<CiscoIPPhoneError>";
        }

        public enum Error : int
        {
            Parsing = 1,
            Framing = 2,
            Internal = 3,
            Authentication = 4
        }
    }
}