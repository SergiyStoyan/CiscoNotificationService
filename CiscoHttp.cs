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
                        XmlNode xn = xd.DocumentElement.SelectSingleNode("Title");
                        string title = xn?.InnerText;
                        xn = xd.DocumentElement.SelectSingleNode("Prompt");
                        string prompt = xn?.InnerText;
                        xn = xd.DocumentElement.SelectSingleNode("Text");
                        string text = xn?.InnerText;

                        xn = xd.DocumentElement.SelectSingleNode("SoftKeyItem");
                        if (xn != null)
                        {
                            string name = xn.SelectSingleNode("Name")?.InnerText;
                            string url = xn.SelectSingleNode("URL")?.InnerText;
                            string position = xn.SelectSingleNode("Position")?.InnerText;
                            AlertForm.AddAlert(title, text, null, name, () =>
                            {
                                if (!string.IsNullOrWhiteSpace(url))
                                    Process.Start(url);
                            });
                        }
                        else
                        {
                            NotificationForm.AddNotification(title, text, null, prompt, null);
                        }
                    }
                    break;
                case "CiscoIPPhoneImageFile":
                    {
                        XmlNode xn = xd.DocumentElement.SelectSingleNode("Title");
                        string title = xn?.InnerText;
                        xn = xd.DocumentElement.SelectSingleNode("Prompt");
                        string prompt = xn?.InnerText;
                        xn = xd.DocumentElement.SelectSingleNode("LocationX");
                        string locationX = xn?.InnerText;
                        xn = xd.DocumentElement.SelectSingleNode("LocationY");
                        string locationY = xn?.InnerText;
                        xn = xd.DocumentElement.SelectSingleNode("URL");
                        string image_url = xn?.InnerText;
                        
                        xn = xd.DocumentElement.SelectSingleNode("SoftKeyItem");
                        if (xn != null)
                        {
                            string name = xn.SelectSingleNode("Name")?.InnerText;
                            string url = xn.SelectSingleNode("URL")?.InnerText;
                            string position = xn.SelectSingleNode("Position")?.InnerText;
                            AlertForm.AddAlert(title, null, image_url, name, () =>
                            {
                                if (!string.IsNullOrWhiteSpace(url))
                                    Process.Start(url);
                            });
                        }
                        else
                        {
                            NotificationForm.AddNotification(title, null, image_url, prompt, null);
                        }
                    }
                    break;
                case "CiscoIPPhoneExecute":
                    {
                        foreach (XmlNode xn in xd.DocumentElement.SelectNodes("ExecuteItem"))
                        {
                            string priority = xn.Attributes["Priority"]?.Value;
                            string url = xn.Attributes["URL"]?.Value.Trim();
                            if (Regex.IsMatch(url, @"https?\:", RegexOptions.IgnoreCase))
                            {
                                Process.Start(url);
                                continue;
                            }
                            m = Regex.Match(url, @"(?'Type'RTPRx|RTPMRx)\:(?'Ip'.*?)\:(?'Port'.*?)(\:(?'Volume'.*?))?$");
                            if (m.Success)
                            {
                                switch (Rtp.Play(m.Groups["Type"].Value == "RTPMRx", IPAddress.Parse(m.Groups["Ip"].Value), int.Parse(m.Groups["Port"].Value), uint.Parse(m.Groups["Volume"].Value)))
                                {
                                    case Rtp.Status.ACCEPTED:
                                        break;
                                    case Rtp.Status.BUSY:
                                        NotificationForm.AddNotification("Error!", "A stream is being received already.", null, null, null);
                                        return get_CiscoIPPhoneError(Error.Parsing, "A stream is being received already.");
                                    default:
                                        throw new Exception("Unknown option.");
                                }
                                continue;
                            }
                            m = Regex.Match(url, @"RTPRx\:Stop");
                            if (m.Success)
                            {
                                Rtp.Stop();
                                continue;
                            }
                            m = Regex.Match(url, @"Play\:(?'File'.*)");
                            if (m.Success)
                            {



                                continue;
                            }
                            NotificationForm.AddNotification("Error", "URL is not supported: " + url, null, null, null);
                        }
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
            //XmlDocument xd = new XmlDocument();
            //XmlNode xn = xd.CreateNode()
            //xd.AppendChild()
            StringBuilder sb = new StringBuilder();
            sb.Append("<CiscoIPPhoneResponse>");
            sb.Append("<test>" + HttpService.Name + ":" + HttpService.Port + "</test>");          
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
            return @"<CiscoIPPhoneError Number=""" + error + @""">" + SecurityElement.Escape(message) + @"</CiscoIPPhoneError>";
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