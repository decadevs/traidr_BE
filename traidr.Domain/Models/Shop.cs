    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }

        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public AppUser Seller { get; set; }       
    }
}
