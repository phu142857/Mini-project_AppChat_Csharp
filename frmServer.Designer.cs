using System;

namespace ChatApp
{
    partial class frmServer
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblRoomCode = new System.Windows.Forms.Label();
            this.btlClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(33, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 68);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoomCode
            // 
            this.lblRoomCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblRoomCode.Location = new System.Drawing.Point(200, 42);
            this.lblRoomCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRoomCode.Name = "lblRoomCode";
            this.lblRoomCode.Size = new System.Drawing.Size(322, 68);
            this.lblRoomCode.TabIndex = 1;
            this.lblRoomCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoomCode.Click += new System.EventHandler(this.lblRoomCode_Click);
            // 
            // btlClose
            // 
            this.btlClose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btlClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btlClose.Location = new System.Drawing.Point(566, 42);
            this.btlClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btlClose.Name = "btlClose";
            this.btlClose.Size = new System.Drawing.Size(216, 68);
            this.btlClose.TabIndex = 2;
            this.btlClose.Text = "Close Room";
            this.btlClose.UseVisualStyleBackColor = false;
            this.btlClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(831, 171);
            this.Controls.Add(this.btlClose);
            this.Controls.Add(this.lblRoomCode);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmServer";
            this.Text = "frmServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServer_FormClosing);
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.ResumeLayout(false);

        }

        private void frmServer_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRoomCode;
        private System.Windows.Forms.Button btlClose;
    }
}