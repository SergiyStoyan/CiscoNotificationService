using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliver
{
    public partial class NotificationControl : UserControl
    {
        public NotificationControl(string title, string text, string action_name, Action action)
        {
            InitializeComponent();

            this.title.Text = title;
            this.text.Text = text;
            this.action.Text = action_name;
            this.action.Click += (object sender, EventArgs e) => { action?.Invoke(); };
        }
    }
}
