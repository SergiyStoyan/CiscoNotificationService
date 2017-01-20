using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.Windows.Interop;
using System.Threading;
using System.Windows.Forms.Integration;

namespace Cliver.CisteraNotification
{
    public partial class NotificationsWindow : Window
    {
        /// <summary>
        /// Must be called from the main UI thread to trigger the static constructor
        /// </summary>
        public static void Initialize()
        {
        }

        static NotificationsWindow()
        {
            //System.Windows.Forms.Application.OpenForms[0].Invoke(() =>
            //{
                This = new NotificationsWindow();
                ElementHost.EnableModelessKeyboardInterop(This);
            //});
        }

        public static void Display()
        {
            This.Show();
            This.Activate();
        }

        static readonly NotificationsWindow This = null;

        NotificationsWindow()
        {
            InitializeComponent();

            Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                //This = null;
                e.Cancel = true;
                This.Hide();
            };

            //Closed += (object sender, EventArgs e) =>
            //{
            //    This = null;
            //};

            select_all.Click += (object sender, RoutedEventArgs e) =>
            {
                lock (this.notifications.Children)
                {
                    foreach (NotificationControl nc in this.notifications.Children)
                        nc.checkBox.IsChecked = true;
                }
            };

            clear_selection.Click += (object sender, RoutedEventArgs e) =>
            {
                lock (this.notifications.Children)
                {
                    foreach (NotificationControl nc in this.notifications.Children)
                        nc.checkBox.IsChecked = false;
                }
            };

            delete_selected.Click += (object sender, RoutedEventArgs e) =>
            {
                List<Notification> ns = new List<Notification>();
                lock (this.notifications.Children)
                {
                    foreach (NotificationControl nc in this.notifications.Children)
                        if (nc.checkBox.IsChecked ?? true)
                            ns.Add(nc.Notification);
                }
                foreach (Notification n in ns)
                    n.Delete();
            };

            show_infos.Click += (object sender, RoutedEventArgs e) =>
            {
                set_visibility();
            };

            show_alerts.Click += (object sender, RoutedEventArgs e) =>
            {
                set_visibility();
            };
        }

        void set_visibility()
        {
            lock (this.notifications.Children)
            {
                foreach (NotificationControl nc in this.notifications.Children)
                    set_visibility(nc);
            }
        }

        void set_visibility(NotificationControl nc)
        {
            if (nc.Notification is Info)
                nc.Visibility = (show_infos.IsChecked ?? true) ? Visibility.Visible : Visibility.Collapsed;
            else if (nc.Notification is Alert)
                nc.Visibility = (show_infos.IsChecked ?? true) ? Visibility.Visible : Visibility.Collapsed;
        }

        static internal void AddToTable(Notification n)
        {
            This.BeginInvoke(() =>
            {
                NotificationControl nc = new NotificationControl(n);
                lock (This.notifications.Children)
                {
                    nc.HorizontalAlignment = HorizontalAlignment.Stretch;
                    This.set_visibility(nc);
                    This.notifications.Children.Insert(0, nc);
                }
            });
        }

        static internal void DeleteFromTable(Notification n)
        {
            This.BeginInvoke(() =>
            {
                lock (This.notifications.Children)
                {
                    for (int i = This.notifications.Children.Count - 1; i >= 0; i--)
                    {
                        NotificationControl nc = (NotificationControl)This.notifications.Children[i];
                        if (nc.Notification == n)
                            This.notifications.Children.RemoveAt(i);
                    }
                }
            });
        }
    }
}