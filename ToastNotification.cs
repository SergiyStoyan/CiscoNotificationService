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
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Cliver.ShellHelpers;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace Cliver
{
    class ToastNotification
    {
        static ToastNotification()
        {
            CreateShortcut();
        }
        static readonly String APP_ID = "CliverSoft." + Log.EntryAssemblyName;

        // In order to display toasts, a desktop application must have a shortcut on the Start menu.
        // Also, an AppUserModelID must be set on that shortcut.
        // The shortcut should be created as part of the installer. The following code shows how to create
        // a shortcut and assign an AppUserModelID using Windows APIs. You must download and include the 
        // Windows API Code Pack for Microsoft .NET Framework for this code to function
        //
        // Included in this project is a wxs file that be used with the WiX toolkit
        // to make an installer that creates the necessary shortcut. One or the other should be used.
        static private void CreateShortcut()
        {
            String shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\" + Log.EntryAssemblyName + ".lnk";
            if (File.Exists(shortcutPath))
                return;

            // Find the path to the current executable
            String exePath = Process.GetCurrentProcess().MainModule.FileName;
            IShellLinkW newShortcut = (IShellLinkW)new CShellLink();

            // Create a shortcut to the exe
            ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
            ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

            // Open the shortcut property store, set the AppUserModelId property
            IPropertyStore newShortcutProperties = (IPropertyStore)newShortcut;

            using (PropVariant appId = new PropVariant(APP_ID))
            {
                ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));
                ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
            }

            // Commit the shortcut to disk
            IPersistFile newShortcutSave = (IPersistFile)newShortcut;

            ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutSave.Save(shortcutPath, true));
        }

        static public void Text(string title, string prompt, string text)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText04);

            XmlNodeList texts = toastXml.GetElementsByTagName("text");
            texts[0].AppendChild(toastXml.CreateTextNode(title));
            texts[1].AppendChild(toastXml.CreateTextNode(prompt));
            texts[2].AppendChild(toastXml.CreateTextNode(text));

            IXmlNode toast = toastXml.SelectSingleNode("/toast");
            //((XmlElement)toast).SetAttribute("duration", "long");                        
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.IM");
            //audio.SetAttribute("silent", "true");
            toast.AppendChild(audio);

            //XmlElement commands = toastXml.CreateElement("commands");
            //toast.AppendChild(commands);
            //XmlElement command = toastXml.CreateElement("command");
            //command.SetAttribute("id", "test");
            //commands.AppendChild(command);

            Windows.UI.Notifications.ToastNotification tn = new Windows.UI.Notifications.ToastNotification(toastXml);
            tn.Activated += ToastActivated;
            tn.Dismissed += ToastDismissed;
            tn.Failed += ToastFailed;

            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(tn);
        }

        static public void TextImage(string title, string prompt, string imageUrl)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            XmlNodeList texts = toastXml.GetElementsByTagName("text");
            texts[0].AppendChild(toastXml.CreateTextNode(title));
            texts[1].AppendChild(toastXml.CreateTextNode(prompt));

            XmlNodeList images = toastXml.GetElementsByTagName("image");
            //desktop apps can use only local images; web images are not supported
            if (!imageUrl.Contains(":"))
                imageUrl = Cliver.ProgramRoutines.GetAppDirectory() + "\\" + imageUrl;
            ((XmlElement)images[0]).SetAttribute("src", "file:///" + Path.GetFullPath(imageUrl));
            ((XmlElement)images[0]).SetAttribute("alt", "[ ]");

            IXmlNode toast = toastXml.SelectSingleNode("/toast");
            //((XmlElement)toast).SetAttribute("duration", "long");                        
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.IM");
            //audio.SetAttribute("silent", "true");
            toast.AppendChild(audio);

            Windows.UI.Notifications.ToastNotification tn = new Windows.UI.Notifications.ToastNotification(toastXml);
            tn.Activated += ToastActivated;
            tn.Dismissed += ToastDismissed;
            tn.Failed += ToastFailed;

            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(tn);
        }

        static private void ToastActivated(Windows.UI.Notifications.ToastNotification sender, object e)
        {
            Message.Inform("Activated");
        }

        static private void ToastDismissed(Windows.UI.Notifications.ToastNotification sender, ToastDismissedEventArgs e)
        {
            String outputText = "";
            switch (e.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "The app hid the toast using ToastNotifier.Hide";
                    break;
                case ToastDismissalReason.UserCanceled:
                    outputText = "The user dismissed the toast";
                    break;
                case ToastDismissalReason.TimedOut:
                    outputText = "The toast has timed out";
                    break;
            }
            Message.Inform(outputText);
        }

        static private void ToastFailed(Windows.UI.Notifications.ToastNotification sender, ToastFailedEventArgs e)
        {
            Message.Inform("failed");
        }
    }
}