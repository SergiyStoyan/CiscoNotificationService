//********************************************************************************************
//Author: Sergey Stoyan, CliverSoft.com
//        http://cliversoft.com
//        stoyan@cliversoft.com
//        sergey.stoyan@gmail.com
//        27 February 2007
//Copyright: (C) 2007, Sergey Stoyan
//********************************************************************************************

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
    public partial class SysTray : Form //BaseForm// 
    {
        SysTray()
        {
            InitializeComponent();

            StartStop.Checked = Settings.Default.Run;
        }

        public static readonly SysTray This = new SysTray();

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //Notifications_Click(null, null);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm.Open();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.Open();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }

        private void SysTray_VisibleChanged(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void StartStop_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Run = StartStop.Checked;
            Settings.Default.Save();
            Program.UpdateService();
        }

        private void RightClickMenu_Opening(object sender, CancelEventArgs e)
        {
            StartStop.Checked = Program.IsServiceRunning;
        }

        private void Notifications_Click(object sender, EventArgs e)
        {
            NotificationsWindow.Display();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //RightClickMenu.Visible = true;
            if (e.Button == MouseButtons.Left)
                Notifications_Click(null, null);
        }

        private void rtpRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(Settings.Default.RtpStreamStorageFolder))
            {
                Message.Exclaim("Rtp Storage Folder is not set.");
                return;
            }
            OpenFileDialog d = new OpenFileDialog();
            d.InitialDirectory = Settings.Default.RtpStreamStorageFolder;
            d.Title = "Pick a wav file";
            d.Filter = "Filter WAVE files (*.wav)|*.wav|All files (*.*)|*.*";
            if (d.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(d.FileName))
                return;
            System.Diagnostics.Process.Start(d.FileName);
        }
    }
}
