using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Enums;

namespace traidr.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? ShopName { get; set; }
        public bool IsSeller { get; set; } = false;

        public ICollection<Conversation> Conversations { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();


        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

      
        [ForeignKey("WishList")]
        public int WishListId { get; set; }
        public ShoppingCart WishList { get; set; }


    }
}
