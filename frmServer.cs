using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin.Messaging;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class frmServer : Form
    {
        private readonly FirebaseClient firebase;
        private IDisposable messageSubscription;

        public string ServerId { get; private set; }

        public event Action<Message> MessageReceived;

        public frmServer()
        {
            InitializeComponent();
            firebase = new FirebaseClient(
                FirebaseConfig.DatabaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(FirebaseConfig.AuthToken)
                });

            // Attempt to set or reuse ServerId
            Task.Run(async () => await InitializeServerIdAsync());
        }

        private string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        private async Task InitializeServerIdAsync()
        {
            string potentialServerId = GenerateUniqueCode();
            var existingRoom = await firebase
                .Child("chatrooms")
                .Child(potentialServerId)
                .OnceSingleAsync<ChatRoom>();

            if (existingRoom != null && DateTime.TryParse(existingRoom.CreationDate, out DateTime creationDate))
            {
                // Check if the room is older than 2 hours
                if ((DateTime.UtcNow - creationDate).TotalHours >= 2)
                {
                    // Delete the old room
                    await DeleteChatRoom(potentialServerId);
                    ServerId = potentialServerId;
                }
                else
                {
                    // Generate a new ServerId if the existing room is still within 1 day
                    ServerId = GenerateUniqueCode();
                }
            }
            else
            {
                // No existing room with this ID; use the generated ServerId
                ServerId = potentialServerId;
            }

            // Update UI with the server ID
            lblRoomCode.Invoke((MethodInvoker)(() => lblRoomCode.Text = ServerId));

            // Send the server creation message
            SendCreationMessageAsync();
        }

        private async void SendCreationMessageAsync()
        {
            string creationTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            string creationMessage = $"Server created at: {creationTime}";

            await SendMessage(ServerId, "Server", creationMessage);

            // Save room information with creation date
            var chatRoom = new ChatRoom
            {
                RoomId = ServerId,
                CreationDate = creationTime
            };

            await firebase
                .Child("chatrooms")
                .Child(ServerId)
                .PutAsync(chatRoom);
        }

        public async Task SendMessage(string roomId, string userId, string message)
        {
            var messageData = new Message
            {
                SenderId = userId,
                Content = message,
                Timestamp = DateTime.UtcNow.ToString("o")
            };

            try
            {
                await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .Child("messages")
                    .PostAsync(messageData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }

        public void ListenToChatRoomMessages(string roomId)
        {
            messageSubscription = firebase
                .Child("chatrooms")
                .Child(roomId)
                .Child("messages")
                .AsObservable<Message>()
                .Subscribe(message =>
                {
                    if (message.Object != null)
                    {
                        MessageReceived?.Invoke(message.Object);
                    }
                });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            messageSubscription?.Dispose();
            base.OnFormClosing(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task DeleteChatRoom(string roomId)
        {
            try
            {
                await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .DeleteAsync();
                MessageBox.Show("Chat room deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting chat room: {ex.Message}");
            }
        }

        private async void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(ServerId))
            {
                await DeleteChatRoom(ServerId);
            }
            this.Close();
        }

        private void lblRoomCode_Click(object sender, EventArgs e)
        {
            // Change the background color to indicate a successful copy action
            lblRoomCode.BackColor = System.Drawing.Color.LightGreen;

            Thread clipboardThread = new Thread(() =>
            {
                Clipboard.Clear();
                Clipboard.SetText(lblRoomCode.Text); // Copy the text to the clipboard
            });
            clipboardThread.SetApartmentState(ApartmentState.STA);
            clipboardThread.Start(); // Start the clipboard thread

            // Revert the color after a brief delay
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 500; // 500 milliseconds
            timer.Tick += (s, args) =>
            {
                lblRoomCode.BackColor = System.Drawing.Color.Transparent; // Reset to original color
                timer.Stop(); // Stop the timer
                timer.Dispose(); // Dispose of the timer
            };
            timer.Start(); // Start the timer
        }


    }
}
