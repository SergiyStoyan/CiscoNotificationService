namespace Cliver
{
    partial class NotificationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.image = new System.Windows.Forms.PictureBox();
            this.text = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.action = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // image
            // 
            this.image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.image.Location = new System.Drawing.Point(9, 8);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(53, 53);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 0;
            this.image.TabStop = false;
            // 
            // text
            // 
            this.text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text.Location = new System.Drawing.Point(0, 13);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(234, 56);
            this.text.TabIndex = 1;
            this.text.Text = "text";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(28, 13);
            this.title.TabIndex = 2;
            this.title.Text = "title";
            // 
            // action
            // 
            this.action.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.action.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.action.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.action.Location = new System.Drawing.Point(0, 69);
            this.action.Margin = new System.Windows.Forms.Padding(0);
            this.action.Name = "action";
            this.action.Size = new System.Drawing.Size(305, 23);
            this.action.TabIndex = 3;
            this.action.Text = "action";
            this.action.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.text);
            this.panel1.Controls.Add(this.title);
            this.panel1.Location = new System.Drawing.Point(71, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 69);
            this.panel1.TabIndex = 4;
            // 
            // NotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.action);
            this.Controls.Add(this.image);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NotificationControl";
            this.Size = new System.Drawing.Size(305, 93);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button action;
        private System.Windows.Forms.Panel panel1;
    }
}
