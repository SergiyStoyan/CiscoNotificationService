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
    public partial class CiscoObjectExecuteControl : UserControl
    {
        public CiscoObjectExecuteControl()
        {
            InitializeComponent();

            file.IsEnabled = false;
        }

        internal CiscoObjectExecuteControl(Execute n)
        {
            InitializeComponent();

            this.title.Text = "";

            file.IsEnabled = n.Record != null;
            this.file.Click += (object sender, RoutedEventArgs e) =>
            {
                e.Handled = true;

                if (n.Record == null)
                    return;
                System.Diagnostics.Process.Start("explorer.exe", " /select, \"" + n.Record + "\"");
            };
        }

        internal bool FileEnabled
        {
            set
            {
                this.BeginInvoke(() =>
                {
                    file.IsEnabled = value;
                });
            }
        }
    }
}