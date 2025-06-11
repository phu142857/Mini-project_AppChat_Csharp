using Firebase.Database;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database.Query;
using System.Collections.Generic;
using Grpc.Core;

namespace ChatApp
{
    public partial class frmDashborad : Form
    {
        private FirebaseClient firebase; // Declare the FirebaseClient
        private FlowLayoutPanel serverFlowPanel;
        private bool mouseDown;
        private Point lastLocation;
 
        public frmDashborad()
        {
            InitializeComponent();

            // Add mouse events for dragging functionality
            firebase = new FirebaseClient(FirebaseConfig.FirebaseUrl); // connect to firebase // delete all room when closing

            topPanel.MouseDown += new MouseEventHandler(mainPanel_MouseDown);
            topPanel.MouseMove += new MouseEventHandler(mainPanel_MouseMove);
            topPanel.MouseUp += new MouseEventHandler(mainPanel_MouseUp);
        }

        private void InitializeServerPanel()
        {
            // Initialize FlowLayoutPanel if it doesn't exist
            if (serverFlowPanel == null)
            {
                serverFlowPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    WrapContents = true,
                    FlowDirection = FlowDirection.LeftToRight
                };
                mainPanel.Controls.Add(serverFlowPanel);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            InitializeServerPanel();

            Task.Run(() =>
            {
                frmServer frmServer = new frmServer
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Margin = new Padding(10)
                };

                // Ensure updates to UI happen on the UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    serverFlowPanel.Controls.Add(frmServer);
                    frmServer.Show();
                });
            });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Add any painting logic here
        }

        // Dragging functionality methods
        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void mainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void frmDashborad_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}