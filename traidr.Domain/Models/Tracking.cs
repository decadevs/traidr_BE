using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Enums;

namespace traidr.Domain.Models
{
    public class Tracking
    {
        public int TrackingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public TrackingStatus TrackingStatus  { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string TrackingNumber { get; set; }
    }
}
