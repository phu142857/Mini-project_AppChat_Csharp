using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Grpc.Core;
using System.Drawing;

namespace ChatApp
{
    public partial class frmClient : Form
    {
        private readonly FirebaseClient firebase;
        private IDisposable messageSubscription;
        private string roomId;
        private string userId;

        public frmClient(string username)
        {
            InitializeComponent();
            firebase = new FirebaseClient(
                FirebaseConfig.DatabaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(FirebaseConfig.AuthToken)
                });

            btnVideoCall.Enabled = false;
            btnExitRoom.Enabled = false;

            userId = username;
            panel1.MouseDown += new MouseEventHandler(mainPanel_MouseDown);
            panel1.MouseMove += new MouseEventHandler(mainPanel_MouseMove);
            panel1.MouseUp += new MouseEventHandler(mainPanel_MouseUp);
            lblUserInfo1.Text = $"Logged in as: {userId}";
        }

        private async void btnJoinRoom_Click(object sender, EventArgs e)
        {
            roomId = txtRoomID.Text.Trim();

            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Please enter a valid Room ID.");
                return;
            }

            try
            {
                // Check if the room exists
                var room = await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .OnceSingleAsync<object>();

                if (room == null)
                {
                    MessageBox.Show("The specified Room ID does not exist. Please check and try again.");
                    return;
                }

                // Start listening for messages in the specified room
                ListenToChatRoomMessages(roomId);

                // Enable messaging controls
                lstMessages.Enabled = true;
                txtMessage.Enabled = true;
                btnSendImage.Enabled = true;
                btnSend.Enabled = true;
                btnExitRoom.Enabled = true;
                btnVideoCall.Enabled = true;
                btnJoinRoom.Enabled = false;
                txtRoomID.Enabled = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error joining room: {ex.Message}");
            }
        }


        private void ListenToChatRoomMessages(string roomId)
        {
            // Subscribe to messages in the specified chat room
            messageSubscription = firebase
                .Child("chatrooms")
                .Child(roomId)
                .Child("messages")
                .AsObservable<Message>()
                .Subscribe(message =>
                {
                    if (message.Object != null)
                    {
                        DisplayMessage(message.Object);
                    }
                });
        }

        private void DisplayMessage(Message message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Create a container panel for the message
                Panel messagePanel = new Panel
                {
                    Width = lstMessages.Width - 30,
                    Padding = new Padding(3),
                    Margin = new Padding(3)
                };

                bool isSender = message.SenderId == userId;

                if (message.IsImage)
                {
                    byte[] imageBytes = Convert.FromBase64String(message.ImageData);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                        // Calculate the new size (1/5 of the original dimensions)
                        int newWidth = image.Width / 5;
                        int newHeight = image.Height / 5;

                        PictureBox pb = new PictureBox
                        {
                            Image = image,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Width = newWidth,  // Set width to 1/5 of original
                            Height = newHeight, // Set height to 1/5 of original
                            Margin = new Padding(3),
                            Padding = new Padding(5)
                        };

                        // Add click event handler to enlarge the image
                        pb.Click += (s, e) =>
                        {
                            ImageViewer viewer = new ImageViewer(image);
                            viewer.ShowDialog();
                        };

                        // Add sender's name inside the message box
                        Label senderLabel = new Label
                        {
                            Text = message.SenderId,
                            AutoSize = true,
                            Font = new Font("Arial", 9, FontStyle.Bold),
                            ForeColor = Color.DarkSlateGray,
                            Padding = new Padding(3),
                            Margin = new Padding(0, 0, 0, 5),
                            TextAlign = ContentAlignment.MiddleCenter
                        };

                        // Add sender label and image to the panel
                        messagePanel.Controls.Add(senderLabel);
                        messagePanel.Controls.Add(pb);
                    }
                }
                else
                {
                    Label messageLabel = new Label
                    {
                        Text = $"{message.SenderId}: {message.Content}",
                        AutoSize = true,
                        MaximumSize = new Size(lstMessages.Width - 80, 0),
                        Padding = new Padding(isSender ? 10 : 20, 5, 5, 5),
                        Margin = new Padding(3),
                        BackColor = isSender ? Color.LightBlue : Color.LightGray,
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Arial", 10, FontStyle.Regular),
                        ForeColor = Color.Black,
                        TextAlign = isSender ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft
                    };

                    messagePanel.Controls.Add(messageLabel);
                }

                lstMessages.Controls.Add(messagePanel);
                lstMessages.ScrollControlIntoView(messagePanel);
            });
        }

        private async void btnSendImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Please join a room first.");
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    string base64Image = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath));

                    var message = new Message
                    {
                        SenderId = userId,
                        ImageData = base64Image,
                        Timestamp = DateTime.UtcNow.ToString("o")
                    };

                    try
                    {
                        await firebase
                            .Child("chatrooms")
                            .Child(roomId)
                            .Child("messages")
                            .PostAsync(message);

                        MessageBox.Show("Image sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error sending image: {ex.Message}");
                    }
                }
            }
        }


        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Please join a room first.");
                return;
            }

            string messageContent = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(messageContent))
            {
                return;
            }

            var message = new Message
            {
                SenderId = userId,
                Content = messageContent,
                Timestamp = DateTime.UtcNow.ToString("o")
            };

            try
            {
                // Send the message to Firebase
                await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .Child("messages")
                    .PostAsync(message);

                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }

        private void btnExitRoom_Click(object sender, EventArgs e)
        {
            try
            {
                // Stop listening to the chat room
                messageSubscription?.Dispose();

                // Disable messaging controls
                lstMessages.Enabled = false;
                txtMessage.Enabled = false;
                btnSend.Enabled = false;
                btnExitRoom.Enabled = false;
                btnJoinRoom.Enabled = true;
                txtRoomID.Enabled = true;
                btnSendImage.Enabled = false;
                btnVideoCall.Enabled = false;
                // Clear the room ID
                roomId = null;
                txtRoomID.Clear();
                lstMessages.Controls.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exiting room: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dispose of the subscription on form close
            messageSubscription?.Dispose();
            base.OnFormClosing(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmClient_Load(object sender, EventArgs e)
        {

        }

        // Dragging functionality methods

        private bool mouseDown;
        private Point lastLocation;

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

        private async void btnVideoCall_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Please join a room first.");
                return;
            }

            // Send video call message to Firebase
            var message = new Message
            {
                SenderId = userId,
                Content = "Video call initiated.",
                IsVideoCall = true,
                Timestamp = DateTime.UtcNow.ToString("o")
            };

            try
            {
                await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .Child("messages")
                    .PostAsync(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending video call message: {ex.Message}");
            }

            // Open video call form
            frmVideoCall videoCallForm = new frmVideoCall(roomId,userId);
            videoCallForm.Show();
        }

    }
}


