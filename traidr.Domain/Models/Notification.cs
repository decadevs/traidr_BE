using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Dtos.Enums;

namespace traidr.Domain.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public AppUser User { get; set; }

        public string Message {  get; set; }

        public NotificationStatus Status { get; set; }

        public NotificationType NotificationType { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsSeller {  get; set; }
    }
}
