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

namespace Cliver.CisteraNotification
{
    public partial class NotificationWindow : Window
    {
        public static NotificationWindow This
        {
            get
            {
                if (_This == null)
                {//!!!the following code does not work in static constructor because creates a deadlock!!!
                    bool ready = false;
                    ThreadRoutines.StartTry(() =>
                    {
                        _This = new NotificationWindow();
                        WindowInteropHelper h = new WindowInteropHelper(_This);
                        h.EnsureHandle();
                        _This.Visibility = Visibility.Hidden;
                        ready = true;
                        System.Windows.Forms.Application.Run();
                    });
                    SleepRoutines.WaitForCondition(() => { return ready; }, 1000);
                    if (_This == null)
                        throw new Exception("Cound not create NotificationForm");
                }
                return _This;
            }
        }
        static NotificationWindow _This = null;

        private NotificationWindow()
        {
            InitializeComponent();

            Height = 10;
        }

        public static void AddNotification(string title, string text, string image_url, string action_name, Action action, string sound_file)
        {
            This.Invoke(() =>
            {
                var c = new NotificationControl(title, text, image_url, action_name, action);
                Grid.SetRow(c, 1);
                This.grid.Children.Insert(0, c);

                if (!string.IsNullOrWhiteSpace(sound_file))
                {
                    SoundPlayer sp = new SoundPlayer(sound_file);
                    sp.Play();
                }

                This.display();
            });
        }

        public static void Clear()
        {
            This.Invoke(() =>
            {
                while (This.grid.Children.Count > 1)
                    RemoveNotification((NotificationControl)This.grid.Children[This.grid.Children.Count - 1]);
            });
        }

        //static bool remove_oldeset_one()
        //{
        //    if (This.Controls.Count > 1)
        //    {
        //        RemoveNotification((NotificationControl)This.Controls[0]);
        //        return true;
        //    }
        //    return false;
        //}

        public static void RemoveNotification(NotificationControl nc)
        {
            This.Invoke(() =>
            {
                This.grid.Children.Remove(nc);

                UIElement last_e = This.grid.Children[This.grid.Children.Count-1];
                var gt = last_e.TransformToAncestor(This);
                double b = gt.Transform(new Point(0, last_e.RenderSize.Height)).Y;

                Rect r= VisualTreeHelper.GetDescendantBounds(This); 
                if (r.Bottom > b)
                {
                    double h = r.Bottom - b;
                    This.Height -= h;
                    //This.Top += h;
                    WindowControlRoutines.SlideVertically(This, 0.3, This.Top + h);
                }

                if (This.grid.Children.Count <= 1)
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
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = wa.Right - Width - Settings.Default.NotificationFormRightPosition;
            this.Top = wa.Bottom;

            Topmost = false;
            WindowControlRoutines.Invoke(this, () => { Opacity = 0.3; });
            Show();
            WindowControlRoutines.SlideVertically(This, 0.3, wa.Bottom - Height, 1, tune_height);
            WindowControlRoutines.Condense(This, 0.1, 1);
        }

        void tune_height()
        {
            UIElement last_e = This.grid.Children[This.grid.Children.Count - 1];
            var gt = last_e.TransformToAncestor(This);
            double b = gt.Transform(new Point(0, last_e.RenderSize.Height)).Y;
            Rect r = VisualTreeHelper.GetDescendantBounds(This);
            if (r.Bottom < b && Height < Settings.Default.NotificationFormHeight)
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
