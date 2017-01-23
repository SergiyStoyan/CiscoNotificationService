namespace Cliver.CisteraNotification
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.ServicePort = new System.Windows.Forms.TextBox();
            this.ServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bReset = new System.Windows.Forms.Button();
            this.UseWindowsUserAsServiceName = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.RtpStreamStorageFolder = new System.Windows.Forms.TextBox();
            this.bRtpStreamStorageFolder = new System.Windows.Forms.Button();
            this.RecordIncomingRTPStreams = new System.Windows.Forms.CheckBox();
            this.AudioDevices = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.InformSound = new System.Windows.Forms.TextBox();
            this.bSelectPlayOnInform = new System.Windows.Forms.Button();
            this.InfoWindowRight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InfoWindowLifeTimeInSecs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.InfoWindowBottom = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AlertToastRight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.AlertToastTop = new System.Windows.Forms.TextBox();
            this.AlertSound = new System.Windows.Forms.TextBox();
            this.bSelectPlayOnAlert = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ForgetNotificationsOlderThanDays = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(375, 265);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(294, 265);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 4;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // ServicePort
            // 
            this.ServicePort.Location = new System.Drawing.Point(49, 90);
            this.ServicePort.Name = "ServicePort";
            this.ServicePort.Size = new System.Drawing.Size(52, 20);
            this.ServicePort.TabIndex = 3;
            // 
            // ServiceName
            // 
            this.ServiceName.Location = new System.Drawing.Point(49, 52);
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.Size = new System.Drawing.Size(206, 20);
            this.ServiceName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "User:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port:";
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(12, 265);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(75, 23);
            this.bReset.TabIndex = 12;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // UseWindowsUserAsServiceName
            // 
            this.UseWindowsUserAsServiceName.AutoSize = true;
            this.UseWindowsUserAsServiceName.Location = new System.Drawing.Point(17, 29);
            this.UseWindowsUserAsServiceName.Name = "UseWindowsUserAsServiceName";
            this.UseWindowsUserAsServiceName.Size = new System.Drawing.Size(238, 17);
            this.UseWindowsUserAsServiceName.TabIndex = 14;
            this.UseWindowsUserAsServiceName.Text = "Always Use Windows User As Service Name";
            this.UseWindowsUserAsServiceName.UseVisualStyleBackColor = true;
            this.UseWindowsUserAsServiceName.CheckedChanged += new System.EventHandler(this.UseWindowsUserAsServiceName_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(438, 247);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ForgetNotificationsOlderThanDays);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.RtpStreamStorageFolder);
            this.tabPage4.Controls.Add(this.bRtpStreamStorageFolder);
            this.tabPage4.Controls.Add(this.RecordIncomingRTPStreams);
            this.tabPage4.Controls.Add(this.AudioDevices);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(430, 221);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // RtpStreamStorageFolder
            // 
            this.RtpStreamStorageFolder.Location = new System.Drawing.Point(22, 96);
            this.RtpStreamStorageFolder.Name = "RtpStreamStorageFolder";
            this.RtpStreamStorageFolder.Size = new System.Drawing.Size(354, 20);
            this.RtpStreamStorageFolder.TabIndex = 24;
            // 
            // bRtpStreamStorageFolder
            // 
            this.bRtpStreamStorageFolder.Location = new System.Drawing.Point(382, 94);
            this.bRtpStreamStorageFolder.Name = "bRtpStreamStorageFolder";
            this.bRtpStreamStorageFolder.Size = new System.Drawing.Size(25, 23);
            this.bRtpStreamStorageFolder.TabIndex = 25;
            this.bRtpStreamStorageFolder.Text = "...";
            this.bRtpStreamStorageFolder.UseVisualStyleBackColor = true;
            this.bRtpStreamStorageFolder.Click += new System.EventHandler(this.bRtpStreamStorageFolder_Click);
            // 
            // RecordIncomingRTPStreams
            // 
            this.RecordIncomingRTPStreams.AutoSize = true;
            this.RecordIncomingRTPStreams.Location = new System.Drawing.Point(22, 73);
            this.RecordIncomingRTPStreams.Name = "RecordIncomingRTPStreams";
            this.RecordIncomingRTPStreams.Size = new System.Drawing.Size(224, 17);
            this.RecordIncomingRTPStreams.TabIndex = 22;
            this.RecordIncomingRTPStreams.Text = "Record Incoming RTP Streams To Folder:";
            this.RecordIncomingRTPStreams.UseVisualStyleBackColor = true;
            // 
            // AudioDevices
            // 
            this.AudioDevices.FormattingEnabled = true;
            this.AudioDevices.Location = new System.Drawing.Point(22, 35);
            this.AudioDevices.Name = "AudioDevices";
            this.AudioDevices.Size = new System.Drawing.Size(377, 21);
            this.AudioDevices.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Audio Device:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(430, 221);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bonjour";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UseWindowsUserAsServiceName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ServiceName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ServicePort);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 215);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(430, 221);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Infos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InformSound);
            this.groupBox2.Controls.Add(this.bSelectPlayOnInform);
            this.groupBox2.Controls.Add(this.InfoWindowRight);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.InfoWindowLifeTimeInSecs);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.InfoWindowBottom);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 215);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // InformSound
            // 
            this.InformSound.Location = new System.Drawing.Point(18, 37);
            this.InformSound.Name = "InformSound";
            this.InformSound.Size = new System.Drawing.Size(354, 20);
            this.InformSound.TabIndex = 12;
            // 
            // bSelectPlayOnInform
            // 
            this.bSelectPlayOnInform.Location = new System.Drawing.Point(378, 35);
            this.bSelectPlayOnInform.Name = "bSelectPlayOnInform";
            this.bSelectPlayOnInform.Size = new System.Drawing.Size(25, 23);
            this.bSelectPlayOnInform.TabIndex = 16;
            this.bSelectPlayOnInform.Text = "...";
            this.bSelectPlayOnInform.UseVisualStyleBackColor = true;
            this.bSelectPlayOnInform.Click += new System.EventHandler(this.bSelectPlayOnInform_Click);
            // 
            // InfoWindowRight
            // 
            this.InfoWindowRight.Location = new System.Drawing.Point(18, 76);
            this.InfoWindowRight.Name = "InfoWindowRight";
            this.InfoWindowRight.Size = new System.Drawing.Size(52, 20);
            this.InfoWindowRight.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Toast Display Time (sec):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Play When Appears:";
            // 
            // InfoWindowLifeTimeInSecs
            // 
            this.InfoWindowLifeTimeInSecs.Location = new System.Drawing.Point(18, 154);
            this.InfoWindowLifeTimeInSecs.Name = "InfoWindowLifeTimeInSecs";
            this.InfoWindowLifeTimeInSecs.Size = new System.Drawing.Size(52, 20);
            this.InfoWindowLifeTimeInSecs.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "First Toast Right (px):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "First Toast Bottom (px):";
            // 
            // InfoWindowBottom
            // 
            this.InfoWindowBottom.Location = new System.Drawing.Point(18, 115);
            this.InfoWindowBottom.Name = "InfoWindowBottom";
            this.InfoWindowBottom.Size = new System.Drawing.Size(52, 20);
            this.InfoWindowBottom.TabIndex = 14;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(430, 221);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Alerts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AlertToastRight);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.AlertToastTop);
            this.groupBox3.Controls.Add(this.AlertSound);
            this.groupBox3.Controls.Add(this.bSelectPlayOnAlert);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(424, 215);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // AlertToastRight
            // 
            this.AlertToastRight.Location = new System.Drawing.Point(18, 76);
            this.AlertToastRight.Name = "AlertToastRight";
            this.AlertToastRight.Size = new System.Drawing.Size(52, 20);
            this.AlertToastRight.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Toast Right (px):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Toast Top (px):";
            // 
            // AlertToastTop
            // 
            this.AlertToastTop.Location = new System.Drawing.Point(18, 115);
            this.AlertToastTop.Name = "AlertToastTop";
            this.AlertToastTop.Size = new System.Drawing.Size(52, 20);
            this.AlertToastTop.TabIndex = 29;
            // 
            // AlertSound
            // 
            this.AlertSound.Location = new System.Drawing.Point(18, 37);
            this.AlertSound.Name = "AlertSound";
            this.AlertSound.Size = new System.Drawing.Size(354, 20);
            this.AlertSound.TabIndex = 20;
            // 
            // bSelectPlayOnAlert
            // 
            this.bSelectPlayOnAlert.Location = new System.Drawing.Point(378, 35);
            this.bSelectPlayOnAlert.Name = "bSelectPlayOnAlert";
            this.bSelectPlayOnAlert.Size = new System.Drawing.Size(25, 23);
            this.bSelectPlayOnAlert.TabIndex = 24;
            this.bSelectPlayOnAlert.Text = "...";
            this.bSelectPlayOnAlert.UseVisualStyleBackColor = true;
            this.bSelectPlayOnAlert.Click += new System.EventHandler(this.bSelectPlayOnAlert_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Play When Appears:";
            // 
            // ForgetNotificationsOlderThanDays
            // 
            this.ForgetNotificationsOlderThanDays.Location = new System.Drawing.Point(22, 144);
            this.ForgetNotificationsOlderThanDays.Name = "ForgetNotificationsOlderThanDays";
            this.ForgetNotificationsOlderThanDays.Size = new System.Drawing.Size(47, 20);
            this.ForgetNotificationsOlderThanDays.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(199, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Forget Notifications Received Days Ago:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 297);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.TextBox ServicePort;
        private System.Windows.Forms.TextBox ServiceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.CheckBox UseWindowsUserAsServiceName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InformSound;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InfoWindowRight;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InfoWindowBottom;
        private System.Windows.Forms.Button bSelectPlayOnInform;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox InfoWindowLifeTimeInSecs;
        private System.Windows.Forms.Button bSelectPlayOnAlert;
        private System.Windows.Forms.TextBox AlertSound;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox AudioDevices;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AlertToastRight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox AlertToastTop;
        private System.Windows.Forms.CheckBox RecordIncomingRTPStreams;
        private System.Windows.Forms.TextBox RtpStreamStorageFolder;
        private System.Windows.Forms.Button bRtpStreamStorageFolder;
        private System.Windows.Forms.TextBox ForgetNotificationsOlderThanDays;
        private System.Windows.Forms.Label label12;
    }
}