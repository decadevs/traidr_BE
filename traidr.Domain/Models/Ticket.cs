using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Enums;

namespace traidr.Domain.Models
{
    public class Ticket
    {
        public int id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
