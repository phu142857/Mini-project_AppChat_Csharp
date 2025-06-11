using System.Windows.Forms;

namespace ChatApp
{
    partial class frmClient
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.btnExitRoom = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblRoomID = new System.Windows.Forms.Label();
            this.lblUserInfo1 = new System.Windows.Forms.Label();
            this.btnVideoCall = new System.Windows.Forms.Button();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.lstMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 40);
            this.panel1.TabIndex = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(787, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(40, 40);
            this.btnMinimize.TabIndex = 7;
            this.btnMinimize.Text = "_";
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(833, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtRoomID
            // 
            this.txtRoomID.Location = new System.Drawing.Point(45, 96);
            this.txtRoomID.Name = "txtRoomID";
            this.txtRoomID.Size = new System.Drawing.Size(255, 20);
            this.txtRoomID.TabIndex = 8;
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.btnJoinRoom.FlatAppearance.BorderSize = 0;
            this.btnJoinRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinRoom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnJoinRoom.ForeColor = System.Drawing.Color.White;
            this.btnJoinRoom.Location = new System.Drawing.Point(319, 78);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(155, 44);
            this.btnJoinRoom.TabIndex = 9;
            this.btnJoinRoom.Text = "Join Room";
            this.btnJoinRoom.UseVisualStyleBackColor = false;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // btnExitRoom
            // 
            this.btnExitRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(58)))));
            this.btnExitRoom.FlatAppearance.BorderSize = 0;
            this.btnExitRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExitRoom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExitRoom.ForeColor = System.Drawing.Color.White;
            this.btnExitRoom.Location = new System.Drawing.Point(497, 78);
            this.btnExitRoom.Name = "btnExitRoom";
            this.btnExitRoom.Size = new System.Drawing.Size(155, 45);
            this.btnExitRoom.TabIndex = 10;
            this.btnExitRoom.Text = "Exit Room";
            this.btnExitRoom.UseVisualStyleBackColor = false;
            this.btnExitRoom.Click += new System.EventHandler(this.btnExitRoom_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Enabled = false;
            this.txtMessage.Location = new System.Drawing.Point(45, 538);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(590, 40);
            this.txtMessage.TabIndex = 12;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.btnSend.Enabled = false;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(737, 538);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 40);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblRoomID
            // 
            this.lblRoomID.AutoSize = true;
            this.lblRoomID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblRoomID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.lblRoomID.Location = new System.Drawing.Point(41, 43);
            this.lblRoomID.Name = "lblRoomID";
            this.lblRoomID.Size = new System.Drawing.Size(61, 16);
            this.lblRoomID.TabIndex = 14;
            this.lblRoomID.Text = "Room ID";
            // 
            // lblUserInfo1
            // 
            this.lblUserInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.lblUserInfo1.Location = new System.Drawing.Point(12, 496);
            this.lblUserInfo1.Name = "lblUserInfo1";
            this.lblUserInfo1.Size = new System.Drawing.Size(254, 39);
            this.lblUserInfo1.TabIndex = 15;
            this.lblUserInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVideoCall
            // 
            this.btnVideoCall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.btnVideoCall.FlatAppearance.BorderSize = 0;
            this.btnVideoCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVideoCall.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVideoCall.ForeColor = System.Drawing.Color.White;
            this.btnVideoCall.Location = new System.Drawing.Point(678, 78);
            this.btnVideoCall.Name = "btnVideoCall";
            this.btnVideoCall.Size = new System.Drawing.Size(149, 44);
            this.btnVideoCall.TabIndex = 16;
            this.btnVideoCall.Text = "VideoCall";
            this.btnVideoCall.UseVisualStyleBackColor = false;
            this.btnVideoCall.Click += new System.EventHandler(this.btnVideoCall_Click);
            // 
            // btnSendImage
            // 
            this.btnSendImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.btnSendImage.Enabled = false;
            this.btnSendImage.FlatAppearance.BorderSize = 0;
            this.btnSendImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendImage.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSendImage.ForeColor = System.Drawing.Color.White;
            this.btnSendImage.Location = new System.Drawing.Point(641, 538);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(90, 40);
            this.btnSendImage.TabIndex = 17;
            this.btnSendImage.Text = "Send IMG";
            this.btnSendImage.UseVisualStyleBackColor = false;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // lstMessages
            // 
            this.lstMessages.Location = new System.Drawing.Point(45, 142);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(782, 351);
            this.lstMessages.TabIndex = 18;
            // Ensure the FlowLayoutPanel is properly configured to auto-scroll
            lstMessages.FlowDirection = FlowDirection.TopDown;
            lstMessages.AutoScroll = true;
            lstMessages.WrapContents = false;  // Prevent wrapping of content in the FlowLayoutPanel

            // 
            // frmClient
            // 
            this.ClientSize = new System.Drawing.Size(876, 625);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.btnSendImage);
            this.Controls.Add(this.btnVideoCall);
            this.Controls.Add(this.lblUserInfo1);
            this.Controls.Add(this.lblRoomID);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnExitRoom);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.txtRoomID);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat Client";
            this.Load += new System.EventHandler(this.frmClient_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnExitRoom;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblRoomID;
        private Label lblUserInfo1;
        private Button btnVideoCall;
        private Button btnSendImage;
        private FlowLayoutPanel lstMessages;
    }
}
