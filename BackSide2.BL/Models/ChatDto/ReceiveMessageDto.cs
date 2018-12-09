using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.BL.Models.ChatDto
{
    public class ReceiveMessageDto
    {
        public string SenderUserName { get; set; }
        public long SenderId { get; set; }
        public bool Unread { get; set; }
        public DateTime LastChange { get; set; }
        public string MessageContent { get; set; }
    }
}