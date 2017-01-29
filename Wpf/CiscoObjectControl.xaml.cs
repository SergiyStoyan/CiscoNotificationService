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
                type.Content = "Info:";
                back_color = Colors.Beige;
                control = new CiscoObjectInfoControl((Info)n);
            }
            else if (n is Alert)
            {
                type.Content = "Alert:";
                back_color = Colors.OrangeRed;
                control = new CiscoObjectAlertControl((Alert)n);
            }
            Background = new SolidColorBrush(back_color);
            control.HorizontalAlignment = HorizontalAlignment.Stretch;
            control.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetColumn(control, 1);
            grid.Children.Add(control);

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
                control.Visibility = Visibility.Collapsed;
                xml.Visibility = Visibility.Visible;
            };

            show_xml.Unchecked += (object sender, RoutedEventArgs e) =>
            {
                e.Handled = true;
                control.Visibility = Visibility.Visible;
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
                    Color c = Color.FromArgb(255, (byte)(back_color.R - 30), (byte)(back_color.R - 30), (byte)(back_color.R - 30));
                    Background = new SolidColorBrush(c);
                }
                else
                {
                    Background = new SolidColorBrush(back_color);
                }
            };

            time.Content = n.CreateTime.ToString("yy-MM-dd HH:mm:ss");
        }
        readonly Control control = null;
        readonly Color back_color;

        internal readonly CiscoObject CiscoObject = null;
    }
}