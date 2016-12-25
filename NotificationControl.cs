using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliver.CisteraNotification
{
    public partial class NotificationControl : UserControl
    {
        public NotificationControl(string title, string text, string image_url, string action_name, Action action)
        {
            InitializeComponent();

            this.title.Text = title;
            this.text.Text = text;
            //var request = System.Net.WebRequest.Create(image_url);
            //request.BeginGetResponse((r) =>
            //{
            //    if (!r.IsCompleted)
            //        return;
            //    using (var stream = ((System.Net.WebRequest)r.AsyncState).EndGetResponse(r).GetResponseStream())
            //    {
            //        image.Image = Bitmap.FromStream(stream);
            //    }
            //}, request);
            if (image_url != null)
            {
                image.SizeMode = PictureBoxSizeMode.Zoom;
                if (!image_url.Contains(":"))
                    image_url = Log.AppDir + image_url;
                image.ImageLocation = image_url;
            }
            else
            {
                int iw = image.Width;
                Controls.Remove(image);
                this.panel1.Left -= iw;
                this.panel1.Width += iw;
            }
            //image.Invalidate();
            if (action_name != null)
                this.action.Text = action_name;
            this.action.Click += (object sender, EventArgs e) =>
            {
                action?.Invoke();
                NotificationForm.RemoveNotification(this);
            };
        }
    }
}
