using System;
using System.Runtime.InteropServices;

namespace Lab5_app.DTO.Models
{
    public class UserChatMessage
    {
        public string  Username { get; set; }
        public string  Message { get; set; }
        public DateTime  TimeStamp { get; set; }
        public string TimeStampString => TimeStamp.ToString("dd-MM-yyyy, hh:mm:ss");
    }
}