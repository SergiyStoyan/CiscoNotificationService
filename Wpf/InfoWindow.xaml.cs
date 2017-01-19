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

namespace Cliver.CisteraNotification
{
    public partial class InfoWindow : Window
    {
        static readonly List<InfoWindow> ws = new List<InfoWindow>();

        public static InfoWindow Create(string title, string text, string image_url, string action_name, Action action)
        {
            InfoWindow w = null;
            Thread t = ThreadRoutines.StartTry(() =>
          {
              w = new InfoWindow(title, text, image_url, action_name, action);
              WindowInteropHelper h = new WindowInteropHelper(w);
              h.EnsureHandle();
              //w.Visibility = Visibility.Hidden;
              System.Windows.Threading.Dispatcher.Run();
          },
            null,
            null,
            true,
            ApartmentState.STA
            );
            if (!SleepRoutines.WaitForCondition(() => { return w != null; }, 3000))
                throw new Exception("Could not create InfoWindow");

            WpfControlRoutines.BeginInvoke(w, () =>
            {
                //Rectangle wa = Screen.GetWorkingArea(a);
                //w.DesktopLocation = new Point(wa.Right - a.Width - Settings.Default.AlertFormRightPosition, wa.Top);

                if (!string.IsNullOrWhiteSpace(Settings.Default.InformSoundFile))
                {
                    SoundPlayer sp = new SoundPlayer(Settings.Default.InformSoundFile);
                    sp.Play();
                }

                w.Topmost = true;
                w.Opacity = 0.3;
                w.Show();
                WpfControlRoutines.Condense(w, 0.002, 1);
            });
            return w;
        }

        InfoWindow()
        {
            InitializeComponent();
        }

        InfoWindow(string title, string text, string image_url, string action_name, Action action)
        {
            InitializeComponent();

            this.grid.Children.Add(new InfoControl(title, text, image_url, action_name, action, true));

            lock (ws)
                ws.Add(this);

            Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                lock (ws)
                    ws.Remove(this);
            };

            //this.title.Text = title;
            //this.text.Text = text;
            //if (image_url != null)
            //{
            //    if (!image_url.Contains(":"))
            //        image_url = Log.AppDir + image_url;
            //    try
            //    {
            //        image.Source = new BitmapImage(new Uri(image_url));
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
            //    image_container.Width = 0;
            //    image_container.Margin = new Thickness(0);
            //}
            //if (action_name != null)
            //    button.Content = action_name;
            //button.Click += (object sender, RoutedEventArgs e) =>
            //{
            //    action?.Invoke();
            //    Close();
            //};
        }
    }
}