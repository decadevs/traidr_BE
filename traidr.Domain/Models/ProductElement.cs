using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class ProductElement
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string Color { get; set; }
        public int QuantityInStock { get; set; }

        public int Sku { get; set; } = new Random().Next(100000, 999999);


    }
}
