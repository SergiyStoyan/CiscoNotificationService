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
    public partial class AlertForm : Form
    {
        public AlertForm()
        {
            InitializeComponent();

            FormClosed += AlertForm_FormClosed;
        }

        static readonly List<AlertForm> afs = new List<AlertForm>();

        private void AlertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            lock (afs)
                afs.Remove(this);
        }

        public static AlertForm AddAlert(string title, string text, string image_url, string action_name, Action action)
        {
            //return (AlertForm)ControlRoutines.InvokeFromUiThread((Func<object>)(() =>
            //{
            //    AlertForm a = new AlertForm();

            AlertForm a = null;
            bool ready = false;
            ThreadRoutines.StartTry(() =>
            {
                a = new AlertForm();
                if (!a.IsHandleCreated)
                    a.CreateHandle();
                a.Opacity = 0;
                ready = true;
                a.ShowDialog();
            });
            SleepRoutines.WaitForCondition(() => { return ready; }, 1000);
            if (a == null)
                throw new Exception("Could not create AlertForm");

            ControlRoutines.Invoke(a, () =>
            {
                lock (afs)
                    afs.Add(a);

                if (image_url != null)
                {
                    //a.image.SizeMode = PictureBoxSizeMode.Zoom;
                    a.image.ImageLocation = image_url;
                }
                else
                {
                    int ih = a.image.Height;
                    a.Controls.Remove(a.image);
                    a.Height -= ih;
                }

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
                a.DesktopLocation = new Point(wa.Right - a.Width - Settings.Default.AlertFormRightPosition, wa.Top);

                if (!string.IsNullOrWhiteSpace(Settings.Default.AlertSoundFile))
                {
                    SoundPlayer sp = new SoundPlayer(Settings.Default.AlertSoundFile);
                    sp.Play();
                }

                a.TopMost = true;
                a.Opacity = 0.3;
                a.Show();
                //ControlRoutines.SlideVertically(a, 700, wa.Bottom - a.Height);
                double centOpacityPerMss = 0.007;
                double centOpacityPerMss2 = 0.007;
                double opacity2 = 0.3;
                double delta = 0.2;
                ControlRoutines.Condense(a, centOpacityPerMss, 1, delta, () =>
                {
                    ControlRoutines.Condense(a, centOpacityPerMss2, opacity2, delta, () =>
                    {
                        ControlRoutines.Condense(a, centOpacityPerMss, 1, delta, () =>
                        {
                            ControlRoutines.Condense(a, centOpacityPerMss2, opacity2, delta, () =>
                            {
                                ControlRoutines.Condense(a, centOpacityPerMss, 1, delta);
                            });
                        });
                    });
                });

                a.BringToFront();
            });
            return a;
        }

        private void image_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Size s = image.Size;
            Size s2 = image.Image.Size;
            Size fs2 = new Size(this.Width + s2.Width - s.Width, this.Height + s2.Height - s.Height);

            if (fs2.Width < this.Width)
                fs2.Width = this.Width;
            if (fs2.Height < this.Height)
                fs2.Height = this.Height;

            double rw = 1;
            if (fs2.Width + Settings.Default.AlertFormRightPosition > Screen.PrimaryScreen.WorkingArea.Width)
                rw = Screen.PrimaryScreen.WorkingArea.Width / fs2.Width;
            double rh = 1;
            if (fs2.Height > Screen.PrimaryScreen.WorkingArea.Height)
                rh = Screen.PrimaryScreen.WorkingArea.Height / fs2.Height;
            double r = Math.Min(rw,rh);
            if (r < 1)
                fs2 = new Size((int)(fs2.Width * r), (int)(fs2.Height * r));

            this.Left -= fs2.Width - this.Width;
            this.Size = fs2;
        }

        //Thread condensing_t = null;
    }
}