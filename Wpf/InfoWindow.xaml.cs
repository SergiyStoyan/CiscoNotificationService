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

        public static InfoWindow AddInform(string title, string text, string image_url, string action_name, Action action)
        {
            InfoWindow w = null;
                   Thread t = ThreadRoutines.StartTry(() =>
                 {
                     w = new InfoWindow();
                     WindowInteropHelper h = new WindowInteropHelper(w);
                     h.EnsureHandle();
                     //w.Visibility = Visibility.Hidden;
                     //System.Windows.Threading.Dispatcher.Run();
                 },
                   null,
                   null,
                   true,
                   ApartmentState.STA
                   );
                if (!SleepRoutines.WaitForCondition(() => { return w != null; }, 3000))
                    throw new Exception("Could not create InfoWindow");

            WindowControlRoutines.Invoke(w, () =>
            {
                lock (ws)
                    ws.Add(w);

                //Rectangle wa = Screen.GetWorkingArea(a);
                //w.DesktopLocation = new Point(wa.Right - a.Width - Settings.Default.AlertFormRightPosition, wa.Top);

                if (!string.IsNullOrWhiteSpace(Settings.Default.InformSoundFile))
                {
                    SoundPlayer sp = new SoundPlayer(Settings.Default.InformSoundFile);
                    sp.Play();
                }

                w.Topmost = true;
                w.Opacity = 0.3;
                w.ShowDialog();
                double centOpacityPerMss = 0.7;
                double delta = 0.2;
                WindowControlRoutines.Condense(w, centOpacityPerMss, 1, delta, () =>
                {
                });
            });
            return w;
        }

        InfoWindow()
        {
            InitializeComponent();

           // this.grid.
        }
    }
}
