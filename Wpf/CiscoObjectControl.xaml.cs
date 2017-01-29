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
    public partial class CiscoObjectControl : UserControl
    {
        public CiscoObjectControl()
        {
            InitializeComponent();
        }

        internal CiscoObjectControl(CiscoObject n)
        {
            InitializeComponent();

            CiscoObject = n;

            xml.Text = CiscoObject.Xml;
            xml.Visibility = Visibility.Collapsed;

            if (n is Info)
            {
                type.Content = "Info";
                back_color = Colors.Beige;
                Control = new CiscoObjectInfoControl((Info)n);
            }
            else if (n is Alert)
            {
                type.Content = "Alert";
                back_color = Colors.OrangeRed;
                Control = new CiscoObjectAlertControl((Alert)n);
            }
            else if (n is Execute)
            {
                type.Content = "Execute";
                back_color = Colors.LightGreen;
                Control = new CiscoObjectExecuteControl((Execute)n);
            }
            else
                throw new Exception("Unknown type: " + n.GetType());

            Background = new SolidColorBrush(back_color);
            Control.HorizontalAlignment = HorizontalAlignment.Stretch;
            Control.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetColumn(Control, 1);
            grid.Children.Add(Control);

            delete.Click += (object sender, RoutedEventArgs e) =>
            {
                n.Delete();
                e.Handled = true;
            };

            show_xml.Click += (object sender, RoutedEventArgs e) =>
            {
                e.Handled = true;
            };

            show_xml.Checked += (object sender, RoutedEventArgs e) =>
            {
                e.Handled = true;
                Control.Visibility = Visibility.Collapsed;
                xml.Visibility = Visibility.Visible;
            };

            show_xml.Unchecked += (object sender, RoutedEventArgs e) =>
            {
                e.Handled = true;
                Control.Visibility = Visibility.Visible;
                xml.Visibility = Visibility.Collapsed;
            };

            activate.Click += (object sender, RoutedEventArgs e) =>
            {
                e.Handled = true;
                CiscoObject.Activate();
            };

            MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                checkBox.IsChecked = !checkBox.IsChecked;
                if (checkBox.IsChecked ?? true)
                {
                    Color c = Color.FromArgb(255, (byte)(back_color.R * .6), (byte)(back_color.G * .6), (byte)(back_color.B * .6));
                    Background = new SolidColorBrush(c);
                }
                else
                {
                    Background = new SolidColorBrush(back_color);
                }
            };

            time.Content = n.CreateTime.ToString("yy-MM-dd HH:mm:ss");
        }
        internal readonly Control Control = null;
        readonly Color back_color;

        internal readonly CiscoObject CiscoObject = null;
    }
}