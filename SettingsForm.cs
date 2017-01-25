using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                if (!UseWindowsUserAsServiceName.Checked)
                { 
                    if (string.IsNullOrWhiteSpace(ServiceName.Text))
                        throw new Exception("Service Name cannot be empty.");
                    Settings.Default.ServiceName = ServiceName.Text;
                }
                Settings.Default.AlertSoundFile = AlertSound.Text;
                Settings.Default.AlertToastRight = int.Parse(AlertToastRight.Text);
                Settings.Default.AlertToastTop = int.Parse(AlertToastTop.Text);
                Settings.Default.InfoSoundFile = InformSound.Text;
                Settings.Default.InfoToastRight = int.Parse(InfoWindowRight.Text);
                Settings.Default.InfoToastBottom = int.Parse(InfoWindowBottom.Text);
                Settings.Default.InfoToastLifeTimeInSecs = int.Parse(InfoWindowLifeTimeInSecs.Text);
                if (RecordIncomingRtpStreams.Checked && string.IsNullOrWhiteSpace(RtpStreamStorageFolder.Text))
                    throw new Exception("Rtp Stream Storage Folder cannot be empty.");
                Settings.Default.RecordIncomingRtpStreams = RecordIncomingRtpStreams.Checked;
                Settings.Default.RtpStreamStorageFolder = RtpStreamStorageFolder.Text;
                Settings.Default.ForgetNotificationsOlderThanDays = int.Parse(ForgetNotificationsOlderThanDays.Text);
            }
            catch (Exception ex)
            {
                Message.Error2(ex);
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
            AlertSound.Text = Settings.Default.AlertSoundFile;
            AlertToastRight.Text = Settings.Default.AlertToastRight.ToString();
            AlertToastTop.Text = Settings.Default.AlertToastTop.ToString();
            InformSound.Text = Settings.Default.InfoSoundFile;
            InfoWindowRight.Text = Settings.Default.InfoToastRight.ToString();
            InfoWindowBottom.Text = Settings.Default.InfoToastBottom.ToString();
            InfoWindowLifeTimeInSecs.Text = Settings.Default.InfoToastLifeTimeInSecs.ToString();
            RecordIncomingRtpStreams.Checked = Settings.Default.RecordIncomingRtpStreams;
            RtpStreamStorageFolder.Text = Settings.Default.RtpStreamStorageFolder;
            RtpStreamStorageFolder.Enabled = RecordIncomingRtpStreams.Checked;
            ForgetNotificationsOlderThanDays.Text = Settings.Default.ForgetNotificationsOlderThanDays.ToString();
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
            d.Title = "Pick an audio file";
            d.Filter = "Filter WAVE files (*.wav)|*.wav|All files (*.*)|*.*";
            if (d.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(d.FileName))
                return null;
            return d.FileName;
        }

        private void bSelectPlayOnInform_Click(object sender, EventArgs e)
        {
            InformSound.Text = get_sound_file();
        }

        private void bRtpStreamStorageFolder_Click(object sender, EventArgs e)
        {
           FolderBrowserDialog d = new FolderBrowserDialog();
            d.Description = "Pick a folder where RTP streams will be stored";
            if (d.ShowDialog(this) == DialogResult.OK)
                RtpStreamStorageFolder.Text = d.SelectedPath;
        }

        private void RecordIncomingRtpStreams_CheckedChanged(object sender, EventArgs e)
        {
            RtpStreamStorageFolder.Enabled = RecordIncomingRtpStreams.Checked;
            if (RecordIncomingRtpStreams.Checked && string.IsNullOrWhiteSpace(RtpStreamStorageFolder.Text))
                RtpStreamStorageFolder.Text = Cliver.Log.AppCommonDataDir + "\\ReceivedRtpStreams";
        }
    }
}