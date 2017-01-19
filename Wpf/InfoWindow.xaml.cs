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

namespace Cliver.CisteraNotification
{
    public partial class InfoWindow : Window
    {
        //public static void Initialize(SynchronizationContext main_context)
        //{
        //    InfoWindow.main_context = main_context;
        //}
        //private static SynchronizationContext main_context;

        static readonly List<InfoWindow> ws = new List<InfoWindow>();

        static InfoWindow()
        {
        }
        static System.Windows.Threading.Dispatcher dispatcher = null;

        public static InfoWindow Create(string title, string text, string image_url, string action_name, Action action)
        {
            InfoWindow w = null;

            Action a = () =>
            {
                w = new InfoWindow(title, text, image_url, action_name, action);
                WindowInteropHelper h = new WindowInteropHelper(w);
                h.EnsureHandle();
                //w.Visibility = Visibility.Hidden;
                //System.Windows.Threading.Dispatcher.Run();
                w.Show();
                ThreadRoutines.StartTry(() =>
                {
                    Thread.Sleep(Settings.Default.InfoWindowLifeTimeInSecs * 1000);
                    w.BeginInvoke(() => { w.Close(); });
                });
                if (!string.IsNullOrWhiteSpace(Settings.Default.InfoSoundFile))
                {
                    SoundPlayer sp = new SoundPlayer(Settings.Default.InfoSoundFile);
                    sp.Play();
                }
            };

            //Application.Current.Dispatcher.Invoke(new Action(() => {}));
            // main_context.Send(new SendOrPostCallback((_) => { a(); }), null);
            lock (ws)
            {
                if (dispatcher == null)
                {//!!!cannot be done by static constructor!!!
                    ThreadRoutines.StartTry(() =>
                    {
                        dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
                        System.Windows.Threading.Dispatcher.Run();
                    }, null, null, true, ApartmentState.STA);
                    if (!SleepRoutines.WaitForCondition(() => { return dispatcher != null; }, 3000))
                        throw new Exception("Could not get dispatcher.");
                }
            }
            dispatcher.Invoke(a);
            return w;
        }

        InfoWindow()
        {
            InitializeComponent();
        }

        InfoWindow(string title, string text, string image_url, string action_name, Action action)
        {
            InitializeComponent();

            Loaded += Window_Loaded;
            Closing += Window_Closing;
            Closed += Window_Closed;

            Topmost = true;

            this.grid.Children.Add(new InfoControl(title, text, image_url, action_name, action, true));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var a = new DoubleAnimation(0, 1, (Duration)TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(UIElement.OpacityProperty, a);

            Rect wa = System.Windows.SystemParameters.WorkArea;

            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(wa.Right, wa.Right - Width - Settings.Default.InfoWindowRightBottomPosition.X, (Duration)TimeSpan.FromMilliseconds(300));
            Storyboard.SetTargetProperty(da, new PropertyPath("(Left)")); //Do not miss the '(' and ')'
            sb.Children.Add(da);
            BeginStoryboard(sb);

            lock (ws)
            {
                if (ws.Count > 0)
                {
                    Window w = ws[ws.Count - 1];
                    this.Top = w.Top - this.ActualHeight;
                }
                else
                    this.Top = wa.Bottom - this.ActualHeight - Settings.Default.InfoWindowRightBottomPosition.Y;

                ws.Add(this);
            }
        }

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

        private void Window_Closed(object sender, EventArgs e)
        {
            lock (ws)
            {
                int i = ws.IndexOf(this);
                for (int j = i + 1; j < ws.Count; j++)
                {
                    Window w = ws[j];
                    Storyboard sb = new Storyboard();
                    DoubleAnimation da = new DoubleAnimation(w.Top + this.Height, (Duration)TimeSpan.FromMilliseconds(300));
                    Storyboard.SetTargetProperty(da, new PropertyPath("(Top)")); //Do not miss the '(' and ')'
                    sb.Children.Add(da);
                    w.BeginStoryboard(sb);
                }

                ws.Remove(this);
            }
        }
    }
}