using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }

        [ForeignKey("Buyer")]
        public string BuyerId { get; set; }
        public AppUser Buyer { get; set; }

        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public AppUser Seller { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
