ChatApp-C#
A simple C# Windows Forms chat application using Firebase Realtime Database for real-time messaging, image sharing, and video call features.

Features
- User registration and login (with MD5 password hashing)
- Real-time chat rooms (create/join)
- Image sharing in chat
- Video call (experimental)
- User-friendly UI with drag-to-move windows

ChatApp-C#/
│
├── Program.cs                // Entry point
├── FirebaseConfig.cs         // Firebase configuration (edit this!)
├── frmLogin.cs               // Login form
├── frmRegister.cs            // Registration form
├── frmDashboard.cs           // Dashboard/main menu
├── frmClient.cs              // Chat client form
├── frmServer.cs              // Chat server/room form
├── frmVideoCall.cs           // Video call form
├── Message.cs                // Message model
├── ChatRoom.cs               // Chat room model
├── MD5Helper.cs              // MD5 hashing helper
├── ImageViewer.cs            // Image viewer form
├── serviceAccountKey.json    // (Optional) Firebase Admin SDK key (not used by client)
└── ... (other files/resources)

Getting Started
1. Clone the repository
git clone https://github.com/yourusername/ChatApp-CSharp.git
cd ChatApp-CSharp

2. Install dependencies
Open the solution in Visual Studio or VS Code.
Restore NuGet packages (right-click solution > Restore NuGet Packages).

3. Configure Firebase
⚠️ Important: Edit your Firebase credentials
Open FirebaseConfig.cs:
public static class FirebaseConfig
{
    public const string DatabaseUrl = "https://chatapp-12345.firebaseio.com/"; // Replace with your actual Firebase database URL
    public const string AuthToken = "your_firebase_auth_token_here"; // Replace with your actual Firebase auth token
}

Replace DatabaseUrl with your Firebase Realtime Database URL.

Replace AuthToken with your Firebase Database secret (not your personal password).

replace serviceAccountKey.json with your database service.json

⚠️ Never commit your real AuthToken or secrets to public repositories.
(Optional) serviceAccountKey.json
This file is for Firebase Admin SDK (server-side use).
You do NOT need this for the client app to work.
If you use it, keep it private and never commit it to public repositories.

4. Run the Application
- Press F5 or click Start in Visual Studio/VS Code.

Usage
- Register a new user.
- Login with your credentials.
- Create or join a chat room.
- Send messages or images.
- Start a video call (experimental).

Notes
- All sensitive configuration is in FirebaseConfig.cs.
- Always keep your AuthToken secret.
- The app uses MD5 for password hashing (for demo purposes).
- For production, use a stronger hash (e.g., bcrypt).
- Video call is basic and for demonstration only.

Credits
- Firebase Realtime Database
- NAudio (audio)
- AForge.NET (video)

Remember:
- Edit FirebaseConfig.cs, and replace serviceAccountKey.json with your database service.json before running.
- Never share your real Firebase secrets publicly.

