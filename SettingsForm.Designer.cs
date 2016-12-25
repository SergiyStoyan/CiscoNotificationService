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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bSelectPlayOnInform = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.NotificationFormRightPosition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NotificationFormHeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PlayOnInform = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NotificationFormWidth = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bSelectPlayOnAlert = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.AlertFormRightPosition = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AlertFormHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PlayOnAlert = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AlertFormWidth = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(438, 247);
            this.tabControl1.TabIndex = 15;
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
            this.tabPage2.Controls.Add(this.bSelectPlayOnInform);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.NotificationFormRightPosition);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.NotificationFormHeight);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.PlayOnInform);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.NotificationFormWidth);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(430, 221);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Informs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bSelectPlayOnInform
            // 
            this.bSelectPlayOnInform.Location = new System.Drawing.Point(381, 28);
            this.bSelectPlayOnInform.Name = "bSelectPlayOnInform";
            this.bSelectPlayOnInform.Size = new System.Drawing.Size(25, 23);
            this.bSelectPlayOnInform.TabIndex = 16;
            this.bSelectPlayOnInform.Text = "...";
            this.bSelectPlayOnInform.UseVisualStyleBackColor = true;
            this.bSelectPlayOnInform.Click += new System.EventHandler(this.bSelectPlayOnInform_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Position From Screen Right Boundary:";
            // 
            // NotificationFormRightPosition
            // 
            this.NotificationFormRightPosition.Location = new System.Drawing.Point(21, 147);
            this.NotificationFormRightPosition.Name = "NotificationFormRightPosition";
            this.NotificationFormRightPosition.Size = new System.Drawing.Size(52, 20);
            this.NotificationFormRightPosition.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Height:";
            // 
            // NotificationFormHeight
            // 
            this.NotificationFormHeight.Location = new System.Drawing.Point(21, 108);
            this.NotificationFormHeight.Name = "NotificationFormHeight";
            this.NotificationFormHeight.Size = new System.Drawing.Size(52, 20);
            this.NotificationFormHeight.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Width:";
            // 
            // PlayOnInform
            // 
            this.PlayOnInform.Location = new System.Drawing.Point(21, 30);
            this.PlayOnInform.Name = "PlayOnInform";
            this.PlayOnInform.Size = new System.Drawing.Size(354, 20);
            this.PlayOnInform.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Play When Appears:";
            // 
            // NotificationFormWidth
            // 
            this.NotificationFormWidth.Enabled = false;
            this.NotificationFormWidth.Location = new System.Drawing.Point(21, 69);
            this.NotificationFormWidth.Name = "NotificationFormWidth";
            this.NotificationFormWidth.Size = new System.Drawing.Size(52, 20);
            this.NotificationFormWidth.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.bSelectPlayOnAlert);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.AlertFormRightPosition);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.AlertFormHeight);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.PlayOnAlert);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.AlertFormWidth);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(430, 221);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Alerts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // bSelectPlayOnAlert
            // 
            this.bSelectPlayOnAlert.Location = new System.Drawing.Point(380, 31);
            this.bSelectPlayOnAlert.Name = "bSelectPlayOnAlert";
            this.bSelectPlayOnAlert.Size = new System.Drawing.Size(25, 23);
            this.bSelectPlayOnAlert.TabIndex = 24;
            this.bSelectPlayOnAlert.Text = "...";
            this.bSelectPlayOnAlert.UseVisualStyleBackColor = true;
            this.bSelectPlayOnAlert.Click += new System.EventHandler(this.bSelectPlayOnAlert_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Position From Screen Right Boundary:";
            // 
            // AlertFormRightPosition
            // 
            this.AlertFormRightPosition.Location = new System.Drawing.Point(20, 150);
            this.AlertFormRightPosition.Name = "AlertFormRightPosition";
            this.AlertFormRightPosition.Size = new System.Drawing.Size(52, 20);
            this.AlertFormRightPosition.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Height:";
            // 
            // AlertFormHeight
            // 
            this.AlertFormHeight.Enabled = false;
            this.AlertFormHeight.Location = new System.Drawing.Point(20, 111);
            this.AlertFormHeight.Name = "AlertFormHeight";
            this.AlertFormHeight.Size = new System.Drawing.Size(52, 20);
            this.AlertFormHeight.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Width:";
            // 
            // PlayOnAlert
            // 
            this.PlayOnAlert.Location = new System.Drawing.Point(20, 33);
            this.PlayOnAlert.Name = "PlayOnAlert";
            this.PlayOnAlert.Size = new System.Drawing.Size(354, 20);
            this.PlayOnAlert.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Play When Appears:";
            // 
            // AlertFormWidth
            // 
            this.AlertFormWidth.Enabled = false;
            this.AlertFormWidth.Location = new System.Drawing.Point(20, 72);
            this.AlertFormWidth.Name = "AlertFormWidth";
            this.AlertFormWidth.Size = new System.Drawing.Size(52, 20);
            this.AlertFormWidth.TabIndex = 18;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 297);
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
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.TextBox PlayOnInform;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NotificationFormWidth;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox NotificationFormHeight;
        private System.Windows.Forms.Button bSelectPlayOnInform;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox NotificationFormRightPosition;
        private System.Windows.Forms.Button bSelectPlayOnAlert;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AlertFormRightPosition;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox AlertFormHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox PlayOnAlert;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox AlertFormWidth;
    }
}