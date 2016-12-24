namespace Cliver.CisteraNotification
{
    partial class AlertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertForm));
            this.title = new System.Windows.Forms.Label();
            this.bDismiss = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.Label();
            this.image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(12, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(28, 13);
            this.title.TabIndex = 1;
            this.title.Text = "title";
            // 
            // bDismiss
            // 
            this.bDismiss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDismiss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDismiss.Location = new System.Drawing.Point(12, 268);
            this.bDismiss.Name = "bDismiss";
            this.bDismiss.Size = new System.Drawing.Size(217, 23);
            this.bDismiss.TabIndex = 2;
            this.bDismiss.Text = "Dismiss";
            this.bDismiss.UseVisualStyleBackColor = true;
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(12, 22);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(217, 38);
            this.message.TabIndex = 3;
            this.message.Text = "message";
            // 
            // image
            // 
            this.image.Image = ((System.Drawing.Image)(resources.GetObject("image.Image")));
            this.image.Location = new System.Drawing.Point(12, 63);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(217, 199);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.image.TabIndex = 4;
            this.image.TabStop = false;
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(241, 303);
            this.ControlBox = false;
            this.Controls.Add(this.image);
            this.Controls.Add(this.message);
            this.Controls.Add(this.bDismiss);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlertForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "NotificationForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button bDismiss;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.PictureBox image;
    }
}