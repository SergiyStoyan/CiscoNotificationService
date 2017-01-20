using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LumiSoft.Net.Media;


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
                if (AudioDevices.Items.Count > 0 && AudioDevices.SelectedItem == null)
                    throw new Exception("Audio Device is not selected.");
                Settings.Default.AudioDeviceName = ((AudioOutDevice)AudioDevices.SelectedItem).Name;
                Settings.Default.ServicePort = ushort.Parse(ServicePort.Text);
                Settings.Default.UseWindowsUserAsServiceName = UseWindowsUserAsServiceName.Checked;
                if (string.IsNullOrWhiteSpace(ServiceName.Text))
                    throw new Exception("Service Name cannot be empty.");
                if (!UseWindowsUserAsServiceName.Checked)
                    Settings.Default.ServiceName = ServiceName.Text;
                Settings.Default.AlertSound = AlertSound.Text;
                Settings.Default.InfoSoundFile = InformSound.Text;
                Settings.Default.NotificationFormHeight = int.Parse(NotificationFormHeight.Text);
                Settings.Default.NotificationFormRightPosition = int.Parse(NotificationFormRightPosition.Text);
                Settings.Default.AlertFormRightPosition = int.Parse(AlertFormRightPosition.Text);
            }
            catch (Exception ex)
            {
                Message.Error(ex);
                return;
            }

            Settings.Default.Save();
            this.Close();
            Program.UpdateService();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AudioDevices.DisplayMember = "Name";
            foreach (AudioOutDevice device in AudioOut.Devices)
            {
                AudioDevices.Items.Add(device);
                if (device.Name == Settings.Default.AudioDeviceName)
                    AudioDevices.SelectedItem = device;
            }
            if (AudioDevices.SelectedItem == null && AudioDevices.Items.Count > 0)
                AudioDevices.SelectedIndex = 0;
            ServicePort.Text = Settings.Default.ServicePort.ToString();
            ServiceName.Text = Settings.Default.ServiceName;
            UseWindowsUserAsServiceName.Checked = Settings.Default.UseWindowsUserAsServiceName;
            AlertSound.Text = Settings.Default.AlertSound;
            InformSound.Text = Settings.Default.InfoSoundFile;
            NotificationFormHeight.Text = Settings.Default.NotificationFormHeight.ToString();
            NotificationFormRightPosition.Text = Settings.Default.NotificationFormRightPosition.ToString();
            AlertFormRightPosition.Text = Settings.Default.AlertFormRightPosition.ToString();
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            if (!Message.YesNo("The settings will be reset to the initial state. Proceed?", this))
                return;
            Settings.Default.Reset();
            SettingsForm_Load(null, null);
        }

        private void UseWindowsUserAsServiceName_CheckedChanged(object sender, EventArgs e)
        {
            ServiceName.Enabled = !UseWindowsUserAsServiceName.Checked;
            if (UseWindowsUserAsServiceName.Checked)
                ServiceName.Text = Environment.UserName;
            else
                ServiceName.Text = Settings.Default.ServiceName;
        }

        private void bSelectPlayOnAlert_Click(object sender, EventArgs e)
        {
            AlertSound.Text = get_sound_file();
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
            InformSound.Text = get_sound_file();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}