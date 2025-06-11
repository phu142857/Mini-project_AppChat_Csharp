using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    internal class ChatApp_Model_User
    {
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }  // This will store MD5 hashed password
        public string Email { get; set; }
        public string UserId { get; set; }    // Unique identifier
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
        public string Status { get; set; }

        public User() { }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;  // Should be already hashed when setting
            Email = email;
        }
    }
}
