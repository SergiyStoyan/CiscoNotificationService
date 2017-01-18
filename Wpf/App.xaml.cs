using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cliver.CisteraNotification
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private System.Windows.Forms.NotifyIcon _notifyIcon;
        //private bool _isExit;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InformWindow.AddInform("title:", "text...", "http://www.testserver.com/acknowledge.jsp", "action...", () => { });
            //InformWindow.AddNotification("title:", "text...", "http://www.testserver.com/acknowledge.jsp", "action...", () => { });
            //InformWindow.Closing += MainWindow_Closing;

            //_notifyIcon = new System.Windows.Forms.NotifyIcon();
            //_notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            //_notifyIcon.Icon = BackgroundApplication.Properties.Resources.MyIcon;
            //_notifyIcon.Visible = true;

            //CreateContextMenu();


        }

        //private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (!_isExit)
        //    {
        //        e.Cancel = true;
        //        InformWindow.Hide(); // A hidden window can be shown again, a closed one not
        //    }
        //}

        //private void CreateContextMenu()
        //{
        //    _notifyIcon.ContextMenuStrip =              new System.Windows.Forms.ContextMenuStrip();
        //    _notifyIcon.ContextMenuStrip.Items.Add("InformWindow...").Click += (s, e) => ShowMainWindow();
        //    _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        //}

        //private void ExitApplication()
        //{
        //    _isExit = true;
        //    InformWindow.Close();
        //    _notifyIcon.Dispose();
        //    _notifyIcon = null;
        //}

        //private void ShowMainWindow()
        //{
        //    if (InformWindow.IsVisible)
        //    {
        //        if (InformWindow.WindowState == WindowState.Minimized)
        //        {
        //            InformWindow.WindowState = WindowState.Normal;
        //        }
        //        InformWindow.Activate();
        //    }
        //    else
        //    {
        //        InformWindow.Show();
        //    }
        //}
    }
}
