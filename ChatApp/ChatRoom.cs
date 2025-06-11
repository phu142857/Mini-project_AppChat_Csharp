using System;
using System.Collections.Generic;

public class ChatRoom
{
    public string RoomId { get; set; }
    public string RoomName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; } = true;
    public List<string> Messages { get; set; } = new List<string>();
    public List<string> ActiveUsers { get; set; } = new List<string>();
}
