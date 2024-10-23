using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
