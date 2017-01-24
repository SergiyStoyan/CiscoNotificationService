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
using System.Windows.Media.Animation;
using Gma.System.MouseKeyHook;
using System.Windows.Forms.Integration;

namespace Cliver.CisteraNotification
{
    public partial class AlertWindow : Window
    {
        static AlertWindow()
        {
        }

        static Window invisible_owner_w;

        static System.Windows.Threading.Dispatcher dispatcher = null;
        static AlertWindow This = null;
        static object lock_object = new object();

        public static AlertWindow Create(string title, string text, string image_url, string action_name, Action action)
        {
            lock (lock_object)
            {
                Action a = () =>
                {
                    if (This != null)
                    {
                        try
                        {//might be closed already
                            This.Close();
                        }
                        catch
                        { }
                    }

                    if (invisible_owner_w == null)
                    {
                        //this window is used to hide notification windows from Alt+Tab panel
                        invisible_owner_w = new Window();
                        invisible_owner_w.WindowStyle = WindowStyle.ToolWindow;
                        invisible_owner_w.Width = 0;
                        invisible_owner_w.Height = 0;
                        //invisible_owner_w.Width = SystemParameters.FullPrimaryScreenWidth;
                        //invisible_owner_w.Height = SystemParameters.FullPrimaryScreenHeight;
                        //invisible_owner_w.AllowsTransparency = true;
                        //invisible_owner_w.Background = Brushes.Transparent;
                        //invisible_owner_w.Topmost = true;
                        invisible_owner_w.ShowInTaskbar = false;
                        invisible_owner_w.Top = 0;
                        invisible_owner_w.Left = 0;
                        //invisible_owner_w.MouseDown += delegate
                        //{
                        //    invisible_owner_w.Hide();
                        //};
                        //invisible_owner_w.KeyDown += delegate
                        //{
                        //    invisible_owner_w.Hide();
                        //};

                        invisible_owner_w.Show();
                        invisible_owner_w.Hide();
                    }

                    This = new AlertWindow(title, text, image_url, action_name, action);
                    //ElementHost.EnableModelessKeyboardInterop(This);
                    WindowInteropHelper wih = new WindowInteropHelper(This);
                    This.handle = wih.EnsureHandle();
                    This.Owner = invisible_owner_w;
                    This.Show();
                    //ThreadRoutines.StartTry(() =>
                    //{
                    //    Thread.Sleep(Settings.Default.InfoWindowLifeTimeInSecs * 1000);
                    //    This.BeginInvoke(() => { This.Close(); });
                    //});
                    if (!string.IsNullOrWhiteSpace(Settings.Default.InfoSoundFile))
                    {
                        SoundPlayer sp = new SoundPlayer(Settings.Default.AlertSoundFile);
                        sp.Play();
                    }
                };

                if (dispatcher == null)
                {//!!!the following code does not work in static constructor because creates a deadlock!!!
                    ThreadRoutines.StartTry(() =>
                    {
                        dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
                        System.Windows.Threading.Dispatcher.Run();
                    }, null, null, true, ApartmentState.STA);
                    if (!SleepRoutines.WaitForCondition(() => { return dispatcher != null; }, 3000))
                        throw new Exception("Could not get dispatcher.");
                }
                dispatcher.Invoke(a);

                //ControlRoutines.InvokeFromUiThread(a);

                return This;
            }
        }

        IntPtr handle;

        AlertWindow()
        {
            InitializeComponent();
        }

        AlertWindow(string title, string text, string image_url, string action_name, Action action)
        {
            InitializeComponent();

            Loaded += Window_Loaded;

            Closing += Window_Closing;

            Closed += (s, _) =>
            {
                if (globalHook != null)
                    globalHook.Dispose();
            };

            PreviewMouseDown += delegate
            {
                try
                {//might be closed already
                    Close();
                }
                catch { }
            };
            ShowActivated = true;

            Topmost = true;
            //ShowInTaskbar = true;

            this.title.Text = title;
            this.text.Text = text;
            //var request = System.Net.WebRequest.Create(image_url);
            //request.BeginGetResponse((r) =>
            //{
            //    if (!r.IsCompleted)
            //        return;
            //    using (var stream = ((System.Net.WebRequest)r.AsyncState).EndGetResponse(r).GetResponseStream())
            //    {
            //        image.Image = Bitmap.FromStream(stream);
            //    }
            //}, request);
            if (image_url != null)
            {
                if (!image_url.Contains(":"))
                    image_url = Log.AppDir + image_url;
                try
                {
                    image.Source = new BitmapImage(new Uri(image_url));
                }
                catch
                {
                }
            }
            else
            {
                image.Width = 0;
                image.Height = 0;
            }
            if (action_name != null)
                this.button.Content = action_name;
            this.button.Click += (object sender, RoutedEventArgs e) =>
            {
                action?.Invoke();
                Close();
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var a = new DoubleAnimation(0, 1, (Duration)TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(UIElement.OpacityProperty, a);

            Left = SystemParameters.WorkArea.Right - Settings.Default.AlertToastRight - Width;
            //Top = Settings.Default.AlertToastTop;

            Storyboard sb = new Storyboard();
            DoubleAnimation da;
            da = new DoubleAnimation(-Height, Settings.Default.AlertToastTop, TimeSpan.FromMilliseconds(animation_duration));
            Storyboard.SetTargetProperty(da, new PropertyPath("(Top)")); //Do not miss the '(' and ')'
            //da = new DoubleAnimation(SystemParameters.WorkArea.Right, SystemParameters.WorkArea.Right - Settings.Default.AlertToastRight - Width, TimeSpan.FromMilliseconds(animation_duration));
            //Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'
            sb.Children.Add(da);
            //da = new DoubleAnimation(SystemParameters.PrimaryScreenWidth, (SystemParameters.WorkArea.Right - Width) / 2, TimeSpan.FromMilliseconds(animation_duration));
            //Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'
            //sb.Children.Add(da);
            //da = new DoubleAnimation(SystemParameters.PrimaryScreenHeight, (SystemParameters.WorkArea.Height - Height) / 2, TimeSpan.FromMilliseconds(animation_duration));
            //Storyboard.SetTargetProperty(da, new PropertyPath("(Top)")); //Do not miss the '(' and ')'
            //sb.Children.Add(da);
            BeginStoryboard(sb);

            globalHook = Hook.GlobalEvents();
            globalHook.MouseDownExt += GlobalHook_MouseDownExt;
            globalHook.KeyPress += GlobalHook_KeyPress;

            Activate();
            Focus();
            Win32.SetForegroundWindow(handle);

            //HwndSource hs = HwndSource.FromHwnd(handle);
            //hs.AddHook(WndProc);
        }
        double animation_duration = 500;

        IKeyboardMouseEvents globalHook = null;

        private void GlobalHook_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (Win32.GetForegroundWindow() == handle)
            switch (KeyInterop.KeyFromVirtualKey(Win32.VkKeyScan(e.KeyChar)))
            {
                case Key.Escape:
                    Close();
                    break;
                case Key.Enter:
                    button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                default:
                    activate();
                    break;
            }
            e.Handled = true;
        }

        private void GlobalHook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (e.Location.X > Left && e.Location.X <= Left + Width
                && e.Location.Y > Top && e.Location.Y <= Top + Height
                )
                return;
            e.Handled = true;
            activate();
        }

        void activate()
        {
            //var h = new WindowInteropHelper(this).Handle;
            //Win32.SendMessage(h, Win32.WM_SYSCOMMAND, (int)Win32.SC_RESTORE, 0);
            Activate();
            Focus();
            //if (IntPtr.Zero == Win32.SetForegroundWindow(h))
            //Win32.SendMessage(h, Win32.WM_SYSCOMMAND, (int)Win32.SC_RESTORE, 0);
            ThreadRoutines.StartTry(() => {
                Win32.SetForegroundWindow(handle);
                SystemSounds.Beep.Play();
            });
        }

        //private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    //var htLocation = Win32.DefWindowProc(hwnd, msg, wParam, lParam).ToInt32();
        //    switch ((uint)msg)
        //    {
        //        //case Win32.WM_MOUSEACTIVATE:
        //        //case Win32.WM_NCACTIVATE:
        //        case Win32.WM_ACTIVATE:
        //            if (wParam == new IntPtr(Win32.WA_INACTIVE))
        //            {
        //                handled = true;
        //                activate();
        //            }
        //            break;
        //        case Win32.WM_KILLFOCUS:
        //            handled = true;
        //            activate();
        //            break;
        //    }

        //    return IntPtr.Zero;
        //}

        //new double Top
        //{
        //    get
        //    {
        //        return (double)this.Invoke(() => { return base.Top; });
        //    }
        //    set
        //    {
        //        base.Top = value;
        //    }
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var a = new System.Windows.Media.Animation.DoubleAnimation(0, (Duration)TimeSpan.FromMilliseconds(300));
            a.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, a);
        }
    }
}