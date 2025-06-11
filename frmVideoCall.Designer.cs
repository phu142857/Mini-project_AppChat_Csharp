namespace ChatApp
{
    partial class frmVideoCall
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
            this.pictureBoxLocal = new System.Windows.Forms.PictureBox();
            this.pictureBoxRemote = new System.Windows.Forms.PictureBox();
            this.btnEndCall = new System.Windows.Forms.Button();
            this.btnMuteUnmute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemote)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLocal
            // 
            this.pictureBoxLocal.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBoxLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxLocal.Location = new System.Drawing.Point(35, 83);
            this.pictureBoxLocal.Name = "pictureBoxLocal";
            this.pictureBoxLocal.Size = new System.Drawing.Size(672, 460);
            this.pictureBoxLocal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLocal.TabIndex = 0;
            this.pictureBoxLocal.TabStop = false;
            // 
            // pictureBoxRemote
            // 
            this.pictureBoxRemote.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBoxRemote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxRemote.Location = new System.Drawing.Point(743, 83);
            this.pictureBoxRemote.Name = "pictureBoxRemote";
            this.pictureBoxRemote.Size = new System.Drawing.Size(697, 460);
            this.pictureBoxRemote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRemote.TabIndex = 1;
            this.pictureBoxRemote.TabStop = false;
            // 
            // btnEndCall
            // 
            this.btnEndCall.Location = new System.Drawing.Point(10, 741);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Size = new System.Drawing.Size(100, 30);
            this.btnEndCall.TabIndex = 2;
            this.btnEndCall.Text = "End Call";
            this.btnEndCall.UseVisualStyleBackColor = true;
            this.btnEndCall.Click += new System.EventHandler(this.BtnEndCall_Click);
            // 
            // btnMuteUnmute
            // 
            this.btnMuteUnmute.Location = new System.Drawing.Point(116, 741);
            this.btnMuteUnmute.Name = "btnMuteUnmute";
            this.btnMuteUnmute.Size = new System.Drawing.Size(100, 30);
            this.btnMuteUnmute.TabIndex = 3;
            this.btnMuteUnmute.Text = "Mute";
            this.btnMuteUnmute.UseVisualStyleBackColor = true;
            this.btnMuteUnmute.Click += new System.EventHandler(this.BtnMuteUnmute_Click);
            // 
            // frmVideoCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1461, 781);
            this.Controls.Add(this.btnMuteUnmute);
            this.Controls.Add(this.btnEndCall);
            this.Controls.Add(this.pictureBoxRemote);
            this.Controls.Add(this.pictureBoxLocal);
            this.Name = "frmVideoCall";
            this.Text = "Video Call";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVideoCall_FormClosing);
            this.Load += new System.EventHandler(this.frmVideoCall_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLocal;
        private System.Windows.Forms.PictureBox pictureBoxRemote;
        private System.Windows.Forms.Button btnEndCall;
        private System.Windows.Forms.Button btnMuteUnmute;
    }
}