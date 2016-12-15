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
            Match m = Regex.Match(data, @"(^|&)XML\s*=\s*(?'Xml'.*)(&|$)", RegexOptions.Singleline);
            if (!m.Success)
                return get_CiscoIPPhoneError(Error.Parsing, "No Xml in request.");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(HttpUtility.UrlDecode(m.Groups["Xml"].Value));
            switch(xd.DocumentElement.Name)
            {
                case "CiscoIPPhoneText":
                    {
                        XmlNode xn = xd.DocumentElement.SelectSingleNode("*//Title");
                        string title = xn?.Value;
                        xn = xd.DocumentElement.SelectSingleNode("*//Prompt");
                        string prompt = xn?.Value;
                        xn = xd.DocumentElement.SelectSingleNode("*//Text");
                        string text = xn?.Value;
                    }
                    break;
                case "CiscoIPPhoneImageFile":
                    {
                        XmlNode xn = xd.DocumentElement.SelectSingleNode("*//Title");
                        string title = xn?.Value;
                        xn = xd.DocumentElement.SelectSingleNode("*//Prompt");
                        string prompt = xn?.Value;
                        xn = xd.DocumentElement.SelectSingleNode("*//LocationX");
                        string locationX = xn?.Value;
                        xn = xd.DocumentElement.SelectSingleNode("*//LocationY");
                        string locationY = xn?.Value;
                        xn = xd.DocumentElement.SelectSingleNode("*//URL");
                        string url = xn?.Value;
                    }
                    break;
                default:
                    return get_CiscoIPPhoneError(Error.Internal, "This object is not supported: " + xd.DocumentElement.Name);
            }
            return get_CiscoIPPhoneResponse();//? - not clear if it is expected after any request
        }

        //? - not clear if it is suitable for any request
        static string get_CiscoIPPhoneResponse(params ResponseItem[] ris)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<CiscoIPPhoneResponse>");
            foreach (ResponseItem ri in ris)
                sb.Append("<ResponseItem Status=\"" + ri.Status + "\" Data=\"" + SecurityElement.Escape(ri.Data) + "\" URL=\"" + SecurityElement.Escape(ri.Url) + "\"/>");
            sb.Append("</CiscoIPPhoneResponse>");
            return sb.ToString();
        }
        public class ResponseItem
        {
            public string Url;
            public int Status = 0;
            public string Data = "Success";
        }

        //? - not clear if it is suitable for any request
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