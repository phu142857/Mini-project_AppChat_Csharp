using System;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq;
using ChatApp;

public partial class frmRegister : Form
{
    private FirebaseClient firebaseClient;

    public frmRegister()
    {
        InitializeComponent();
        InitializeFirebase();

        // Add event handlers for placeholder text
        txtUsername.Enter += new EventHandler(txtUsername_Enter);
        txtUsername.Leave += new EventHandler(txtUsername_Leave);
        txtEmail.Enter += new EventHandler(txtEmail_Enter);
        txtEmail.Leave += new EventHandler(txtEmail_Leave);
        txtPassword.Enter += new EventHandler(txtPassword_Enter);
        txtPassword.Leave += new EventHandler(txtPassword_Leave);
        txtConfirmPassword.Enter += new EventHandler(txtConfirmPassword_Enter);
        txtConfirmPassword.Leave += new EventHandler(txtConfirmPassword_Leave);
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

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            // Get values from textboxes
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Validate input
            if (username == "Username" || email == "Email" ||
                password == "Password" || confirmPassword == "Confirm Password")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Check if username already exists
            var users = await firebaseClient
                .Child("users")
                .OnceAsync<User>();

            if (users != null && users.Any(u => u.Object.Username == username))
            {
                // User with the specified username exists
                MessageBox.Show("Username already exists. Please choose a different username.");
                return;
            }
            // Hash the password using MD5
            //string hashedPassword = GetMD5Hash(password);
            string hashedPassword = MD5Helper.CreateMD5(password);

            // Create user object
            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            // Add user to Firebase Realtime Database
            await firebaseClient
                .Child("users")
                .PostAsync(user);

            MessageBox.Show("Registration successful!");

            // Clear the form
            ClearForm();

            // Return to login form
            ReturnToLogin();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Registration failed: {ex.Message}");
        }
    }

    private void ClearForm()
    {
        txtUsername.Text = "Username";
        txtEmail.Text = "Email";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
    }

    private void ReturnToLogin()
    {
        frmLogin login = new frmLogin();
        this.Hide();
        login.Show();
    }


    // Your existing event handlers for placeholders
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

    private void txtEmail_Enter(object sender, EventArgs e)
    {
        if (txtEmail.Text == "Email")
        {
            txtEmail.Text = "";
        }
    }

    private void txtEmail_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            txtEmail.Text = "Email";
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

    private void txtConfirmPassword_Enter(object sender, EventArgs e)
    {
        if (txtConfirmPassword.Text == "Confirm Password")
        {
            txtConfirmPassword.Text = "";
            txtConfirmPassword.UseSystemPasswordChar = true;
        }
    }

    private void txtConfirmPassword_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            txtConfirmPassword.Text = "Confirm Password";
            txtConfirmPassword.UseSystemPasswordChar = false;
        }
    }

    // Keep your existing button click handlers for close and minimize
    private void btnClose_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void btnMinimize_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }

    private void lblLogin_Click(object sender, EventArgs e)
    {
        ReturnToLogin();
    }

    private bool mouseDown;
    private Point lastLocation;

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

    private void frmRegister_Load(object sender, EventArgs e)
    {

    }
}
