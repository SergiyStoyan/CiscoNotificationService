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
    public partial class NotificationsWindow : Window
    {
        static public void Initialize()
        {
            //_This = new InformWindow();
            //WindowInteropHelper h = new WindowInteropHelper(_This);
            //h.EnsureHandle();
            //_This.Visibility = Visibility.Hidden;
        }

        public static NotificationsWindow This
        {
            get
            {
                if (_This == null)
                {//!!!the following code does not work in static constructor because creates a deadlock!!!
                    Thread t = ThreadRoutines.StartTry(() =>
                     {
                         NotificationsWindow w = new NotificationsWindow();
                         WindowInteropHelper h = new WindowInteropHelper(w);
                         h.EnsureHandle();
                         w.Visibility = Visibility.Hidden;
                         _This = w;
                         System.Windows.Threading.Dispatcher.Run();
                     },
                       null,
                       null,
                       true,
                       ApartmentState.STA
                       );
                    if(!SleepRoutines.WaitForCondition(() => { return _This!=null; }, 3000))
                        throw new Exception("Could not create InformWindow");
                }
                return _This;
            }
        }
        static NotificationsWindow _This = null;

        NotificationsWindow()
        {
            InitializeComponent();

            // Height = 10;
            //if(_This!=null)
            //    throw new Exception("InformWindow should not be created more than once!");
            //_This = this;
        }

        public static void AddInform(string title, string text, string image_url, string action_name, Action action)
        {            
            This.Invoke(() =>
            {
                var c = new InformControl(title, text, image_url, action_name, action);
                This.infos.Children.Insert(0, c);

                if (!string.IsNullOrWhiteSpace(Settings.Default.InformSoundFile))
                {
                    SoundPlayer sp = new SoundPlayer(Settings.Default.InformSoundFile);
                    sp.Play();
                }
                This.Topmost = true;
                This.display();
            });
        }

        public static void Clear()
        {
            This.Invoke(() =>
            {
                while (This.infos.Children.Count > 0)
                    RemoveNotification((InformControl)This.infos.Children[This.infos.Children.Count - 1]);
            });
        }

        //static bool remove_oldeset_one()
        //{
        //    if (This.Controls.Count > 1)
        //    {
        //        RemoveNotification((InformControl)This.Controls[0]);
        //        return true;
        //    }
        //    return false;
        //}

        public static void RemoveNotification(InformControl nc)
        {
            This.Invoke(() =>
            {
                This.infos.Children.Remove(nc);

                if (This.infos.Children.Count > 0)
                {
                    UIElement last_e = This.infos.Children[This.infos.Children.Count - 1];
                    var gt = last_e.TransformToAncestor(This);
                    double b = gt.Transform(new Point(0, last_e.RenderSize.Height)).Y;

                    Rect r = VisualTreeHelper.GetDescendantBounds(This);
                    if (r.Bottom > b)
                    {
                        double h = r.Bottom - b;
                        This.Height -= h;
                        //This.Top += h;
                        WindowControlRoutines.SlideVertically(This, 0.3, This.Top + h);
                    }
                }

                if (This.infos.Children.Count < 1)
                    WindowControlRoutines.Condense(This, 0.1, 0, 0.1, () =>
                    {
                        This.Visibility = Visibility.Hidden;
                    });
            });
        }

        void display()
        {
            if (This.Visibility == Visibility.Visible)
            {
                Activate();
                tune_height();
                return;
            }
            Rect wa = System.Windows.SystemParameters.WorkArea;
            this.Left = wa.Right - Width - Settings.Default.InformFormRightPosition;
 
             this.Top = wa.Bottom;
            
            WindowControlRoutines.Invoke(this, () => { Opacity = 0.3; });
            Show();
            WindowControlRoutines.SlideVertically(This, 0.3, wa.Bottom - ActualHeight, 1, tune_height);
            WindowControlRoutines.Condense(This, 0.1, 1);
        }

        void tune_height()
        {
            //Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //Arrange(new Rect(0, 0, DesiredSize.Width, DesiredSize.Height));

            UIElement last_e = This.infos.Children[This.infos.Children.Count - 1];
            var gt = last_e.TransformToAncestor(This);
            double b = gt.Transform(new Point(0, last_e.RenderSize.Height)).Y;
            Rect r = VisualTreeHelper.GetDescendantBounds(This);
            if (r.Bottom < b && Height < Settings.Default.InformFormHeight)
            {
                double h = b - r.Bottom;
                Height += h;
                //This.Top -= h;
                WindowControlRoutines.SlideVertically(this, 0.3, Top - h);
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
