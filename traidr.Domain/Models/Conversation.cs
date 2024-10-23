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

        public int BuyerId { get; set; }

        public int SenderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }
    }
}
