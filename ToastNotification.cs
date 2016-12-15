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

namespace Cliver
{
    class ToastNotification
    {
        static ToastNotification()
        {
            CreateShortcut();
        }
        private const String APP_ID = "Microsoft.Samples.DesktopToastsSample";

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
            //String shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Desktop Toasts Sample CS.lnk";
            //if (File.Exists(shortcutPath))
            //    return;

            //// Find the path to the current executable
            //String exePath = Process.GetCurrentProcess().MainModule.FileName;
            //IShellLinkW newShortcut = (IShellLinkW)new CShellLink();

            //// Create a shortcut to the exe
            //ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
            //ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

            //// Open the shortcut property store, set the AppUserModelId property
            //IPropertyStore newShortcutProperties = (IPropertyStore)newShortcut;

            //using (PropVariant appId = new PropVariant(APP_ID))
            //{
            //    ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));
            //    ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
            //}

            //// Commit the shortcut to disk
            //IPersistFile newShortcutSave = (IPersistFile)newShortcut;

            //ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutSave.Save(shortcutPath, true));
        }

        static public void Display()
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            for (int i = 0; i < stringElements.Length; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
            }

            // Specify the absolute path to an image
            String imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            // Create the toast and attach event listeners
            Windows.UI.Notifications.ToastNotification toast = new Windows.UI.Notifications.ToastNotification(toastXml);
            toast.Activated += ToastActivated;
            toast.Dismissed += ToastDismissed;
            toast.Failed += ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
        }

       static private void ToastActivated(Windows.UI.Notifications.ToastNotification sender, object e)
        {
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
        }

        static private void ToastFailed(Windows.UI.Notifications.ToastNotification sender, ToastFailedEventArgs e)
        {
        }
    }
}