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
    public partial class NotificationControl : UserControl
    {
        public NotificationControl()
        {
            InitializeComponent();
        }

        internal NotificationControl(Notification n)
        {
            InitializeComponent();

            Notification = n;

            if (n is Info)
            {
                type.Content = "Info:";
                grid.Background = Brushes.Beige;
            }
            else if (n is Alert)
            {
                type.Content = "Alert:";
                grid.Background = Brushes.OrangeRed;
            }

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
            this.button.Click += (object sender, RoutedEventArgs e) =>
            {
                n.Action?.Invoke();
                e.Handled = true;
            };

            delete.Click += (object sender, RoutedEventArgs e) =>
            {
                n.Delete();
                e.Handled = true;
            };

            show.Click += (object sender, RoutedEventArgs e) =>
            {
                n.Show();
                e.Handled = true;
            };

            grid.PreviewMouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                checkBox.IsChecked = !checkBox.IsChecked;
                if (checkBox.IsChecked??true)
                    box.Background = Brushes.LightGray;
                else
                    box.Background = Brushes.White;
            };

            time.Content = n.CreateTime.ToString("yy-MM-dd HH:mm:ss");
        }

        internal readonly Notification Notification = null;
    }
}