using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using NAudio.Wave;

namespace ChatApp
{
    public partial class frmVideoCall : Form
    {
        private VideoCaptureDevice videoSource;
        private WaveInEvent waveSource;
        private FirebaseClient firebase;
        private string roomId;
        private string senderId;
        private int frameCounter = 0;

        public frmVideoCall(string roomId, string senderId)
        {
            InitializeComponent();
            this.roomId = roomId;
            this.senderId = senderId;

            firebase = new FirebaseClient(
                FirebaseConfig.DatabaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(FirebaseConfig.AuthToken)
                });

            CreateFramesForFirebase();
            StartVideoCapture();
            StartAudioCapture();
            ReceiveFrames();
        }

        private async void CreateFramesForFirebase()
        {
            var message = new Message
            {
                SenderId = "",
                Content = "",
                IsVideoCall = true,
                Timestamp = DateTime.UtcNow.ToString("o")
            };

            try
            {
                await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .Child("videoFrames")
                    .PostAsync(message);
                Console.WriteLine("VideoFrames node created inside the roomId node.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating videoFrames node: {ex.Message}");
            }
        }

        private void StartVideoCapture()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("No video devices found.");
            }
        }

        private void StartAudioCapture()
        {
            waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(16000, 1); // 16 kHz, mono
            waveSource.DataAvailable += OnAudioDataAvailable;
            waveSource.StartRecording();
        }

        private void OnAudioDataAvailable(object sender, WaveInEventArgs e)
        {
            var audioData = new byte[e.Buffer.Length];
            Array.Copy(e.Buffer, audioData, e.Buffer.Length);
            SendAudioToFirebase(audioData);
        }

        private async void SendAudioToFirebase(byte[] audioData)
        {
            string base64Audio = Convert.ToBase64String(audioData);

            var jsonData = new VideocallDataModel
            {
                frameData = null,
                audioData = base64Audio,
                senderId = senderId
            };

            await SendDataToFirebase(jsonData);
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            frameCounter++;
            if (frameCounter % 2 == 0) // send every 2 frame
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                SendFrameToFirebase(bitmap);
            }

            UpdateLocalPictureBox(eventArgs.Frame);
        }   

        private void UpdateLocalPictureBox(Bitmap frame)
        {
            if (pictureBoxLocal.InvokeRequired)
            {
                pictureBoxLocal.Invoke(new Action(() =>
                {
                    pictureBoxLocal.Image?.Dispose();
                    pictureBoxLocal.Image = (Bitmap)frame.Clone();
                }));
            }
            else
            {
                pictureBoxLocal.Image?.Dispose();
                pictureBoxLocal.Image = (Bitmap)frame.Clone();
            }
        }

        private async void SendFrameToFirebase(Bitmap frame)
        {
            const int targetWidth = 320;  // Resize dimensions
            const int targetHeight = 240;

            using (var resizedFrame = ResizeFrame(frame, targetWidth, targetHeight))
            {
                using (var ms = new MemoryStream())
                {
                    var jpegEncoder = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders()
                        .FirstOrDefault(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                    var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 30L); // Lower quality

                    resizedFrame.Save(ms, jpegEncoder, encoderParams);
                    string base64String = Convert.ToBase64String(ms.ToArray());

                    var jsonData = new VideocallDataModel
                    {
                        frameData = base64String,
                        audioData = null,
                        senderId = senderId
                    };

                    await SendDataToFirebase(jsonData);
                }
            }
        }

        private Bitmap ResizeFrame(Bitmap frame, int width, int height)
        {
            return new Bitmap(frame, new Size(width, height));
        }

        private async Task SendDataToFirebase(VideocallDataModel data)
        {
            try
            {
                Console.WriteLine("Sending data to Firebase...");
                await firebase
                    .Child("chatrooms")
                    .Child(roomId)
                    .Child("videoFrames")
                    .PostAsync(data);
                Console.WriteLine("Data sent to Firebase.");
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine($"Firebase Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        private void ReceiveFrames()
        {
            firebase.Child("chatrooms").Child(roomId).Child("videoFrames").AsObservable<VideocallDataModel>()
                .Subscribe(snapshot =>
                {
                    var dataModel = snapshot.Object;

                    if (dataModel != null)
                    {
                        if (!string.IsNullOrEmpty(dataModel.frameData) && dataModel.senderId != this.senderId)
                        {
                            DisplayReceivedFrame(dataModel.frameData);
                        }

                        if (!string.IsNullOrEmpty(dataModel.audioData) && dataModel.senderId != this.senderId)
                        {
                            PlayReceivedAudio(dataModel.audioData);
                        }
                    }
                });
        }

        private void DisplayReceivedFrame(string base64Frame)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Frame);
                using (var ms = new MemoryStream(imageBytes))
                {
                    Bitmap receivedFrame = new Bitmap(ms);
                    if (pictureBoxRemote.InvokeRequired)
                    {
                        pictureBoxRemote.Invoke(new Action(() =>
                        {
                            pictureBoxRemote.Image?.Dispose();
                            pictureBoxRemote.Image = receivedFrame;
                        }));
                    }
                    else
                    {
                        pictureBoxRemote.Image?.Dispose();
                        pictureBoxRemote.Image = receivedFrame;
                    }
                }
                Console.WriteLine("Received frame from Firebase and displayed.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error decoding base64 string: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying received frame: {ex.Message}");
            }
        }

        private void PlayReceivedAudio(string base64Audio)
        {
            try
            {
                byte[] audioBytes = Convert.FromBase64String(base64Audio);
                using (var ms = new MemoryStream(audioBytes))
                using (var waveOut = new WaveOutEvent())
                {
                    var audioFile = new RawSourceWaveStream(ms, new WaveFormat(16000, 1));
                    waveOut.Init(audioFile);
                    waveOut.Play();

                    // Wait for the audio to finish playing
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Task.Delay(100).Wait();
                    }
                }
                Console.WriteLine("Played received audio.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing received audio: {ex.Message}");
            }
        }

        private void StopVideoCapture()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            StopAudioCapture();
            ClearOldFramesAsync();
        }

        private void StopAudioCapture()
        {
            if (waveSource != null)
            {
                waveSource.StopRecording();
                waveSource.Dispose();
            }
        }

        private async void ClearOldFramesAsync()
        {
            try
            {
                await firebase.Child("chatrooms").Child(roomId).Child("videoFrames").DeleteAsync();
                Console.WriteLine("Old frames cleared from Firebase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to clear old frames: {ex.Message}");
            }
        }

        private void BtnEndCall_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMuteUnmute_Click(object sender, EventArgs e)
        {
            if (waveSource != null)
            {
                if (btnMuteUnmute.Text == "Mute")
                {
                    waveSource.StopRecording();
                    btnMuteUnmute.Text = "Unmute";
                }
                else
                {
                    waveSource.StartRecording();
                    btnMuteUnmute.Text = "Mute";
                }
            }
        }

        private void frmVideoCall_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopVideoCapture();
            pictureBoxLocal.Image?.Dispose();
            pictureBoxRemote.Image?.Dispose();
        }

        private void frmVideoCall_Load(object sender, EventArgs e)
        {
            // Any additional setup needed when the form loads
        }
    }

    public class VideocallDataModel
    {
        public string frameData { get; set; } // Base64 string of the image
        public string audioData { get; set; } // Base64 string of audio
        public string senderId { get; set; }  // Unique identifier for the sender
    }
}