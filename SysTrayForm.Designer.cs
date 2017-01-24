namespace Cliver.CisteraNotification
{
    partial class SysTray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysTray));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Notifications = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartStop = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtpRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.RightClickMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Cistera Notification Service";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Notifications,
            this.settingsToolStripMenuItem,
            this.rtpRecordsToolStripMenuItem,
            this.StartStop,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.RightClickMenu.Name = "Menu";
            this.RightClickMenu.Size = new System.Drawing.Size(153, 164);
            this.RightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(this.RightClickMenu_Opening);
            // 
            // Notifications
            // 
            this.Notifications.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Notifications.Name = "Notifications";
            this.Notifications.Size = new System.Drawing.Size(152, 22);
            this.Notifications.Text = "Notifications";
            this.Notifications.Click += new System.EventHandler(this.Notifications_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // StartStop
            // 
            this.StartStop.CheckOnClick = true;
            this.StartStop.Name = "StartStop";
            this.StartStop.Size = new System.Drawing.Size(152, 22);
            this.StartStop.Text = "Run On...";
            this.StartStop.CheckedChanged += new System.EventHandler(this.StartStop_CheckedChanged);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // rtpRecordsToolStripMenuItem
            // 
            this.rtpRecordsToolStripMenuItem.Name = "rtpRecordsToolStripMenuItem";
            this.rtpRecordsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rtpRecordsToolStripMenuItem.Text = "Rtp Records";
            this.rtpRecordsToolStripMenuItem.Click += new System.EventHandler(this.rtpRecordsToolStripMenuItem_Click);
            // 
            // SysTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 74);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "SysTray";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.VisibleChanged += new System.EventHandler(this.SysTray_VisibleChanged);
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartStop;
        private System.Windows.Forms.ToolStripMenuItem Notifications;
        private System.Windows.Forms.ToolStripMenuItem rtpRecordsToolStripMenuItem;
    }
}