using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliver
{
    public partial class NotificationForm : Form
    {
        public NotificationForm()
        {
            InitializeComponent();

            max_height = Height;
        }
        readonly int max_height = 0;

        public static void AddNotification(string title, string text, string image_url, string action_name, Action action)
        {
            This.display();
            var c = new NotificationControl(title, text, image_url, action_name, action);
            c.Dock = DockStyle.Top;
            //c.Width = This.notifications.ClientRectangle.Width;
            This.Controls.Add(c);
            c.BringToFront();

            int b = This.Controls[0].Bottom;
            if (This.ClientRectangle.Bottom < b && This.Height < This.max_height)
            {
                int h = b - This.ClientRectangle.Bottom;
                This.Height += h;
                //This.Top -= h;
                ControlRoutines.SlideVertically(This, 300, This.Top - h);
            }
        }

        public static void Clear()
        {
            while (This.Controls.Count > 1)
                RemoveNotification((NotificationControl)This.Controls[0]);
        }

        public static void RemoveNotification(NotificationControl nc)
        {
            This.Controls.Remove(nc);

            int b = This.Controls[0].Bottom;
            if (This.ClientRectangle.Bottom > b)
            {
                int h = This.ClientRectangle.Bottom - b;
                This.Height -= h;
                //This.Top += h;
                ControlRoutines.SlideVertically(This, 300, This.Top + h);
            }

            if (This.Controls.Count <= 1)
                ControlRoutines.Condense(This, 300, 0, () =>
                {
                    This.Visible = false;
                });
        }

        void display()
        {
            if (Visible)
                return;
            
            Rectangle wa = Screen.GetWorkingArea(this);
            this.DesktopLocation = new Point(wa.Right - Width - right_screen_span, wa.Bottom);

            this.TopMost = true;
            ControlRoutines.Invoke(this, () => { Opacity = 0.3; });
            Show();
            ControlRoutines.SlideVertically(This, 700, wa.Bottom - Height);
            ControlRoutines.Condense(This, 1000, 1);
        }

        //void slide_vertically(uint mss, int p2)
        //{
        //    if (st != null && st.IsAlive)
        //        return;

        //    int delta = Top > p2?- 1:1;
        //    int sleep = (int)((double)mss / ((p2-Top) / delta));
        //    st = ThreadRoutines.Start(() =>
        //    {
        //        while (
        //            !(bool)ControlRoutines.Invoke(this, () =>
        //            {
        //                Top = Top + delta;
        //                return delta<0? Top <= p2: Top >= p2;
        //            })
        //        )
        //            System.Threading.Thread.Sleep(sleep);
        //    });
        //}
        //System.Threading.Thread st = null;

        //void condense(uint mss, double o2)
        //{
        //    if (ct != null && ct.IsAlive)
        //        return;

        //    double delta = Opacity < o2 ? 0.01:-0.01;
        //    int sleep = (int)((double)mss / ((1.0 - Opacity) / delta));
        //    ct = ThreadRoutines.Start(() =>
        //    {
        //        while (
        //            !(bool)ControlRoutines.Invoke(this, () =>
        //            {
        //                Opacity = Opacity + delta;
        //                return delta>0? Opacity >= o2 : Opacity <= o2;
        //            })
        //        )
        //            System.Threading.Thread.Sleep(sleep);
        //    });
        //}
        //System.Threading.Thread ct = null;

        const int right_screen_span = 50;

        static NotificationForm This = new NotificationForm();

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
