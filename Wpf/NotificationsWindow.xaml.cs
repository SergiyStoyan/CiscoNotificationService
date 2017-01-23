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
using System.Windows.Media.Animation;

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
            if(This.Visibility == Visibility.Visible)
            {
                This.Close();
                return;
            }
            This.Show();
            This.Activate();
        }

        static readonly NotificationsWindow This = null;

        NotificationsWindow()
        {
            InitializeComponent();

            //Icon = new BitmapImage().; System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            
            IsVisibleChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                if (Visibility != Visibility.Visible)
                    return;
                show(true);
            };

            Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                e.Cancel = true;
                //This.Hide();
                show(false);
            };

            //Closed += (object sender, EventArgs e) =>
            //{
            //    This = null;
            //};

            restore.IsEnabled = false;

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

            restore.Click += (object sender, RoutedEventArgs e) =>
            {
                Notification.RestoreLastDeleted();
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
                nc.Visibility = (show_alerts.IsChecked ?? true) ? Visibility.Visible : Visibility.Collapsed;
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

                    int i = 0;
                    for (; i < This.notifications.Children.Count; i++)
                        if (((NotificationControl)This.notifications.Children[i]).Notification.CreateTime < n.CreateTime)
                            break;
                    This.notifications.Children.Insert(i, nc);
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

        static internal void EnableRestore(bool enable)
        {
            This.restore.IsEnabled = enable;
        }

        void show(bool show)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da;
            Rect wa = System.Windows.SystemParameters.WorkArea;
            if (show)
            {
                if (position.Y < 0)
                    position = new Point(Left, Top);

                da = new DoubleAnimation(wa.Right, position.X, (Duration)TimeSpan.FromMilliseconds(animation_duration));
                Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'
                sb.Children.Add(da);
                da = new DoubleAnimation(wa.Bottom, position.Y, (Duration)TimeSpan.FromMilliseconds(animation_duration));
                Storyboard.SetTargetProperty(da, new PropertyPath("(Top)")); //Do not miss the '(' and ')'
                sb.Children.Add(da);

                //da = new DoubleAnimation(10, this.Width, (Duration)TimeSpan.FromMilliseconds(1000));
                //Storyboard.SetTargetProperty(da, new PropertyPath("(Width)")); //Do not miss the '(' and ')'
                //sb.Children.Add(da);
                //da = new DoubleAnimation(10, this.Height, (Duration)TimeSpan.FromMilliseconds(1000));
                //Storyboard.SetTargetProperty(da, new PropertyPath("(Height)")); //Do not miss the '(' and ')'
                //sb.Children.Add(da);
            }
            else
            {
                position = new Point(Left, Top);

                da = new DoubleAnimation(wa.Right, (Duration)TimeSpan.FromMilliseconds(animation_duration));
                Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'                
                //RenderOptions.SetCachingHint(_PictureBrush, CachingHint.Cache);
                //RenderOptions.SetBitmapScalingMode(_PictureBrush, BitmapScalingMode.LowQuality);
                sb.Children.Add(da);
                da = new DoubleAnimation(wa.Bottom, (Duration)TimeSpan.FromMilliseconds(animation_duration));
                da.Completed += (object sender, EventArgs e) => { Hide(); };
                Storyboard.SetTargetProperty(da, new PropertyPath("(Top)")); //Do not miss the '(' and ')'
                sb.Children.Add(da);

                //da = new DoubleAnimation(10, this.Width, (Duration)TimeSpan.FromMilliseconds(1000));
                //Storyboard.SetTargetProperty(da, new PropertyPath("(Width)")); //Do not miss the '(' and ')'
                //sb.Children.Add(da);
                //da = new DoubleAnimation(10, this.Height, (Duration)TimeSpan.FromMilliseconds(1000));
                //Storyboard.SetTargetProperty(da, new PropertyPath("(Height)")); //Do not miss the '(' and ')'
                //sb.Children.Add(da);                
            }
            BeginStoryboard(sb);
        }        
        Point position = new Point(-1, -1);
        double animation_duration = 500;
    }
}