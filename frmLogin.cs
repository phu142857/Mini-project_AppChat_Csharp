using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatApp;
using Firebase.Database;

public partial class frmLogin : Form
{
    private FirebaseClient firebaseClient;
    private bool mouseDown;
    private Point lastLocation;

    public frmLogin()
    {
        InitializeComponent();
        InitializeFirebase();

        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Add event handlers for placeholder text
        txtUsername.Enter += new EventHandler(txtUsername_Enter);
        txtUsername.Leave += new EventHandler(txtUsername_Leave);
        txtPassword.Enter += new EventHandler(txtPassword_Enter);
        txtPassword.Leave += new EventHandler(txtPassword_Leave);

        // Add mouse events for dragging functionality
        panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
        panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
        panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);
    }

    private void InitializeFirebase()
    {
        try
        {
            firebaseClient = new FirebaseClient(
                FirebaseConfig.DatabaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(FirebaseConfig.AuthToken)
                });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Firebase initialization error: {ex.Message}");
        }
    }

    private void txtUsername_Enter(object sender, EventArgs e)
    {
        if (txtUsername.Text == "Username")
        {
            txtUsername.Text = "";
        }
    }

    private void txtUsername_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            txtUsername.Text = "Username";
        }
    }

    private void txtPassword_Enter(object sender, EventArgs e)
    {
        if (txtPassword.Text == "Password")
        {
            txtPassword.Text = "";
            txtPassword.UseSystemPasswordChar = true;
        }
    }

    private void txtPassword_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            txtPassword.Text = "Password";
            txtPassword.UseSystemPasswordChar = false;
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void btnMinimize_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }

    private void lblRegister_Click(object sender, EventArgs e)
    {
        frmRegister register = new frmRegister();
        this.Hide();
        register.Show();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Hash the input password
            string hashedPassword = MD5Helper.CreateMD5(password);

            // Retrieve users from Firebase
            var users = await firebaseClient
                .Child("users")
                .OnceAsync<User>();

            // Check if the user exists and the password is correct
            var user = users.FirstOrDefault(u => u.Object.Username == username && u.Object.Password == hashedPassword);

            if (user != null)
            {
                MessageBox.Show("Login successful!");

                // Pass the username to the frmClient
                frmClient clientForm = new frmClient(username); // Pass username
                clientForm.Show(); // Show the client form
                this.Close(); // Close the current login form
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Login failed: {ex.Message}");
        }
    }

    private void frmLogin_Load(object sender, EventArgs e)
    {
        // Optional: any initialization on load
    }

    // Dragging functionality
    private void panel1_MouseDown(object sender, MouseEventArgs e)
    {
        mouseDown = true;
        lastLocation = e.Location;
    }

    private void panel1_MouseMove(object sender, MouseEventArgs e)
    {
        if (mouseDown)
        {
            this.Location = new Point(
                (this.Location.X - lastLocation.X) + e.X,
                (this.Location.Y - lastLocation.Y) + e.Y);
            this.Update();
        }
    }

    private void panel1_MouseUp(object sender, MouseEventArgs e)
    {
        mouseDown = false;
    }
}