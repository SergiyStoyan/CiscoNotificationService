using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Cliver.CisteraNotification
{
    public partial class NotificationForm : Form
    {
        static NotificationForm()
        {
        }

        public static NotificationForm This
        {
            get
            {
                if (_This == null)
                {//!!!the following code does not work in static constructor because creates a deadlock!!!
                    ThreadRoutines.StartTry(() =>
                    {
                        _This = new NotificationForm();
                        if (!_This.IsHandleCreated)
                            _This.CreateHandle();
                        Application.Run(_This);
                    });
                    SleepRoutines.WaitForObject(() => { return _This; }, 1000);
                    if (_This == null)
                        throw new Exception("Cound not create NotificationForm");
                }
                return _This;
            }
        }
        static NotificationForm _This = null;

        private NotificationForm()
        {
            InitializeComponent();

            max_height = Height;

            Height = 10;
        }
        readonly int max_height = 0;
        const int right_screen_span = 50;

        public static void AddNotification(string title, string text, string image_url, string action_name, Action action)
        {
            This.Invoke(() =>
            {
                var c = new NotificationControl(title, text, image_url, action_name, action);
                c.Dock = DockStyle.Top;
                //c.Width = This.notifications.ClientRectangle.Width;
                This.Controls.Add(c);
                This.header.SendToBack();

                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.PlayOnNotification))
                {
                    SoundPlayer sp = new SoundPlayer(Properties.Settings.Default.PlayOnNotification);
                    sp.Play();
                }

                This.display();
            });
        }

        public static void Clear()
        {
            This.Invoke(() =>
            {
                while (This.Controls.Count > 1)
                    RemoveNotification((NotificationControl)This.Controls[0]);
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
                This.Controls.Remove(nc);

                int b = This.Controls[0].Bottom;
                if (This.ClientRectangle.Bottom > b)
                {
                    int h = This.ClientRectangle.Bottom - b;
                    This.Height -= h;
                    //This.Top += h;
                    ControlRoutines.SlideVertically(This, 0.3, This.Top + h);
                }

                if (This.Controls.Count <= 1)
                    ControlRoutines.Condense(This, 0.1, 0, () =>
                    {
                        This.Visible = false;
                    });
            });
        }

        void display()
        {
            if (Visible)
            {
                Activate();
                tune_height();
                return;
            }
            Rectangle wa = Screen.GetWorkingArea(this);
            DesktopLocation = new Point(wa.Right - Width - right_screen_span, wa.Bottom);

            TopMost = false;
            ControlRoutines.Invoke(this, () => { Opacity = 0.3; });
            Show();
            ControlRoutines.SlideVertically(This, 0.3, wa.Bottom - Height, tune_height);
            ControlRoutines.Condense(This, 0.1, 1);
        }

        void tune_height()
        {
            int b = Controls[0].Bottom;
            if (ClientRectangle.Bottom < b && Height < max_height)
            {
                int h = b - ClientRectangle.Bottom;
                Height += h;
                //This.Top -= h;
                ControlRoutines.SlideVertically(this, 0.3, Top - h);
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}