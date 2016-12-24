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

namespace Cliver
{
    public partial class NotificationForm : Form
    {
        public NotificationForm()
        {
            InitializeComponent();

            max_height = Height;

            Height = 10;
        }
        readonly int max_height = 0;

        public static void AddNotification(string title, string text, string image_url, string action_name, Action action)
        {
            if (!This.IsHandleCreated)
                This.CreateHandle();
            ControlRoutines.Invoke(This, () =>
            {
                var c = new InformControl(title, text, image_url, action_name, action);
                c.Dock = DockStyle.Top;
                //c.Width = This.notifications.ClientRectangle.Width;
                This.Controls.Add(c);
                This.header.SendToBack();

                This.display();
            });
        }

        public static void Clear()
        {
            ControlRoutines.Invoke(This, () =>
            {
                while (This.Controls.Count > 1)
                    RemoveNotification((InformControl)This.Controls[0]);
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

        public static void RemoveNotification(InformControl nc)
        {
            ControlRoutines.Invoke(This, () =>
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

        const int right_screen_span = 50;

        public static NotificationForm This = new NotificationForm();

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}