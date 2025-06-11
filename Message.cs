using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Message
{
    public string SenderId { get; set; }
    public string Content { get; set; }
    public string ImageData { get; set; } // Base64 string of the image
    public bool IsImage => !string.IsNullOrEmpty(ImageData);
    public bool IsVideoCall { get; set; }
    public string Timestamp { get; set; }
}
