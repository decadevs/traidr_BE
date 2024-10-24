using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        public Conversation? Conversation { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }

        [ForeignKey("Recipient")]
        public string RecipientId { get; set; }
        public AppUser Recipient { get; set; }

        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }


    }
}
