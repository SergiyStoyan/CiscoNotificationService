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
    public partial class AlertForm : Form
    {
        public AlertForm()
        {
            InitializeComponent();

            max_height = Height;

            Disposed += AlertForm_Disposed;
        }

        private void AlertForm_Disposed(object sender, EventArgs e)
        {
            lock (afs)
                afs.Remove(this);
        }

        readonly int max_height = 0;

        public static void AddAlert(string title, string text, string image_url, string action_name, Action action)
        {
            AlertForm a = new AlertForm();
            if (!a.IsHandleCreated)
                a.CreateHandle();
            ControlRoutines.Invoke(a, () =>
            {
                lock (afs)
                    afs.Add(a);

                if (image_url != null)
                {
                    a.image.SizeMode = PictureBoxSizeMode.Zoom;
                    a.image.ImageLocation = image_url;
                }
                else
                    a.image.Image = null;

                //    Button b = new Button();
                //    b.Size = a.bDismiss.Size;
                //    b.Text = action_name;
                //    b.Click += (object sender, EventArgs e)=>
                //{
                //    action?.Invoke();
                //    a.Close();
                //};
                //a.Controls.Add(b);
                if (action_name != null)
                    a.bDismiss.Text = action_name;
                a.bDismiss.Click += (object sender, EventArgs e) =>
                    {
                        action?.Invoke();
                        a.Close();
                    };

                Rectangle wa = Screen.GetWorkingArea(a);
                a.DesktopLocation = new Point(wa.Right - a.Width - right_screen_span, wa.Top);

                a.TopMost = true;
                ControlRoutines.Invoke(a, () => { a.Opacity = 0.3; });
                a.Show();
                //ControlRoutines.SlideVertically(a, 700, wa.Bottom - a.Height);
                ControlRoutines.Condense(a, 0.1, 1);

                a.BringToFront();
            });
        }

        static readonly List<AlertForm> afs = new List<AlertForm>();

        const int right_screen_span = 50;
    }
}
