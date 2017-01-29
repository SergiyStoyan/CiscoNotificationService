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
    public partial class CiscoObjectsWindow : Window
    {
        /// <summary>
        /// Must be called from the main UI thread to trigger the static constructor
        /// </summary>
        public static void Initialize()
        {
        }

        static CiscoObjectsWindow()
        {
            //System.Windows.Forms.Application.OpenForms[0].Invoke(() =>
            //{
            This = new CiscoObjectsWindow();
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
            This.WindowState = WindowState.Normal;
        }

        static readonly CiscoObjectsWindow This = null;

        CiscoObjectsWindow()
        {
            InitializeComponent();

            Icon = WindowRoutines.GetAppIcon();

            //ShowInTaskbar = false;

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

            select_all.Click += delegate
            {
                lock (this.cisco_objects.Children)
                {
                    foreach (CiscoObjectControl nc in this.cisco_objects.Children)
                        nc.checkBox.IsChecked = true;
                }
            };

            clear_selection.Click += delegate
            {
                lock (this.cisco_objects.Children)
                {
                    foreach (CiscoObjectControl nc in this.cisco_objects.Children)
                        nc.checkBox.IsChecked = false;
                }
            };

            delete_selected.Click += delegate
            {
                List<CiscoObject> ns = new List<CiscoObject>();
                lock (this.cisco_objects.Children)
                {
                    foreach (CiscoObjectControl nc in this.cisco_objects.Children)
                        if (nc.checkBox.IsChecked ?? true)
                            ns.Add(nc.CiscoObject);
                }
                foreach (CiscoObject n in ns)
                    n.Delete();
            };

            restore.Click += delegate
            {
                CiscoObject.RestoreLastDeleted();
            };

            show_infos.Click += delegate
            {
                set_visibility();
            };

            show_alerts.Click += delegate
            {
                set_visibility();
            };
        }
        
        void set_visibility()
        {
            lock (this.cisco_objects.Children)
            {
                foreach (CiscoObjectControl nc in this.cisco_objects.Children)
                    set_visibility(nc);
            }
        }

        void set_visibility(CiscoObjectControl nc)
        {
            if (nc.CiscoObject is Info)
                nc.Visibility = (show_infos.IsChecked ?? true) ? Visibility.Visible : Visibility.Collapsed;
            else if (nc.CiscoObject is Alert)
                nc.Visibility = (show_alerts.IsChecked ?? true) ? Visibility.Visible : Visibility.Collapsed;
        }

        static internal void AddToTable(CiscoObject n)
        {
            This.BeginInvoke(() =>
            {
                CiscoObjectControl nc = new CiscoObjectControl(n);
                lock (This.cisco_objects.Children)
                {
                    nc.HorizontalAlignment = HorizontalAlignment.Stretch;
                    This.set_visibility(nc);

                    int i = 0;
                    for (; i < This.cisco_objects.Children.Count; i++)
                        if (((CiscoObjectControl)This.cisco_objects.Children[i]).CiscoObject.CreateTime < n.CreateTime)
                            break;
                    This.cisco_objects.Children.Insert(i, nc);
                }
            });
        }

        static internal void DeleteFromTable(CiscoObject n)
        {
            This.BeginInvoke(() =>
            {
                lock (This.cisco_objects.Children)
                {
                    for (int i = This.cisco_objects.Children.Count - 1; i >= 0; i--)
                    {
                        CiscoObjectControl nc = (CiscoObjectControl)This.cisco_objects.Children[i];
                        if (nc.CiscoObject == n)
                            This.cisco_objects.Children.RemoveAt(i);
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
            if (show)
            {
                if (position.Y < 0)
                    position = new Point(Left, Top);

                da = new DoubleAnimation(SystemParameters.PrimaryScreenWidth, position.X, TimeSpan.FromMilliseconds(animation_duration));
                Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'
                sb.Children.Add(da);
                da = new DoubleAnimation(SystemParameters.PrimaryScreenHeight, position.Y, TimeSpan.FromMilliseconds(animation_duration));
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

                da = new DoubleAnimation(SystemParameters.PrimaryScreenWidth, (Duration)TimeSpan.FromMilliseconds(animation_duration));
                Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'                
                //RenderOptions.SetCachingHint(_PictureBrush, CachingHint.Cache);
                //RenderOptions.SetBitmapScalingMode(_PictureBrush, BitmapScalingMode.LowQuality);
                sb.Children.Add(da);
                da = new DoubleAnimation(SystemParameters.PrimaryScreenHeight, (Duration)TimeSpan.FromMilliseconds(animation_duration));
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