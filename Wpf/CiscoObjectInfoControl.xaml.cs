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

namespace Cliver.CisteraNotification
{
    public partial class CiscoObjectInfoControl : UserControl
    {
        public CiscoObjectInfoControl()
        {
            InitializeComponent();
        }

        internal CiscoObjectInfoControl(Info n)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(n.Text))
                this.title.Text = n.Title;
            else
                this.title.Text = n.Title + new LineBreak();
            this.text.Text = n.Text;
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
            string image_url = n.ImageUrl;
            if (image_url != null)
            {
                if (!image_url.Contains(":"))
                    image_url = Log.AppDir + n.ImageUrl;
                try
                {
                    image.Source = new BitmapImage(new Uri(image_url));
                }
                catch
                {
                }
            }
            else
            {
                image_container.Width = 0;
                image_container.Margin = new Thickness(0);
            }
            if (n.ActionName != null)
                this.button.Content = n.ActionName;
            if (n.Action != null)
            {
                this.button.Click += (object sender, RoutedEventArgs e) =>
                {
                    n.Action?.Invoke();
                    e.Handled = true;
                };
            }
            else
                button.Visibility = Visibility.Collapsed;
        }
    }
}