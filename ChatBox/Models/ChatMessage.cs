using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBox.Models
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Message { get; set; }

        public DateTime SendMessageTime { get; set; }
    }
}