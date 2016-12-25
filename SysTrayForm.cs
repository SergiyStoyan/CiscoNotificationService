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

            StartStop.Checked = Properties.Settings.Default.Run;
        }

        public static readonly SysTray This = new SysTray();

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            settingsToolStripMenuItem_Click(null, null);
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

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void StartStop_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Run = StartStop.Checked;
            Properties.Settings.Default.Save();
            Program.UpdateService();
        }

        private void RightClickMenu_Opening(object sender, CancelEventArgs e)
        {
            StartStop.Checked = Program.IsServiceRunning;
        }
    }
}
