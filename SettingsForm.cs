using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Cliver.CisteraNotification
{
    public partial class SettingsForm : Form // BaseForm//
    {
        SettingsForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width, Screen.PrimaryScreen.WorkingArea.Bottom - this.Height);
            //UseWindowsUserAsServiceName_CheckedChanged(null, null);
        }

        static public void Open()
        {
            if (sf == null)
                sf = new SettingsForm();
            sf.Show();
            sf.Activate();
        }
        static SettingsForm sf = null;

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sf = null;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.ServicePort = ushort.Parse(ServicePort.Text);
                Properties.Settings.Default.UseWindowsUserAsServiceName = UseWindowsUserAsServiceName.Checked;
                if (string.IsNullOrWhiteSpace(ServiceName.Text))
                    throw new Exception("Service Name cannot be empty.");
                if (!UseWindowsUserAsServiceName.Checked)
                    Properties.Settings.Default.ServiceName = ServiceName.Text;
                Properties.Settings.Default.PlayOnAlert = PlayOnAlert.Text;
                Properties.Settings.Default.PlayOnInform = PlayOnInform.Text;
                Properties.Settings.Default.NotificationFormHeight = int.Parse(NotificationFormHeight.Text);
                Properties.Settings.Default.NotificationFormRightPosition = int.Parse(NotificationFormRightPosition.Text);
                Properties.Settings.Default.AlertFormRightPosition = int.Parse(AlertFormRightPosition.Text);
            }
            catch (Exception ex)
            {
                Message.Error(ex);
                return;
            }

            Properties.Settings.Default.Save();
            this.Close();
            Program.UpdateService();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            ServicePort.Text = Properties.Settings.Default.ServicePort.ToString();
            ServiceName.Text = Properties.Settings.Default.ServiceName;
            UseWindowsUserAsServiceName.Checked = Properties.Settings.Default.UseWindowsUserAsServiceName;
            PlayOnAlert.Text = Properties.Settings.Default.PlayOnAlert;
            PlayOnInform.Text = Properties.Settings.Default.PlayOnInform;
            NotificationFormHeight.Text = Properties.Settings.Default.NotificationFormHeight.ToString();
            NotificationFormRightPosition.Text = Properties.Settings.Default.NotificationFormRightPosition.ToString();
            AlertFormRightPosition.Text = Properties.Settings.Default.AlertFormRightPosition.ToString();
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            if (!Message.YesNo("The settings will be reset to the initial state. Proceed?", this))
                return;
            Properties.Settings.Default.Reset();
            SettingsForm_Load(null, null);
        }

        private void UseWindowsUserAsServiceName_CheckedChanged(object sender, EventArgs e)
        {
            ServiceName.Enabled = !UseWindowsUserAsServiceName.Checked;
            if (UseWindowsUserAsServiceName.Checked)
                ServiceName.Text = Environment.UserName;
            else
                ServiceName.Text = Properties.Settings.Default.ServiceName;
        }

        private void bSelectPlayOnAlert_Click(object sender, EventArgs e)
        {
            PlayOnAlert.Text = get_sound_file();
        }

        private string get_sound_file()
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Pick an wav file";
            d.Filter = "Filter sound files (*.wav)|*.wav|All files (*.*)|*.*";
            if (d.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(d.FileName))
                return null;
            return d.FileName;
        }

        private void bSelectPlayOnInform_Click(object sender, EventArgs e)
        {
            PlayOnInform.Text = get_sound_file();
        }
    }
}