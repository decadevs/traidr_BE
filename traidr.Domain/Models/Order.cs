using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public AppUser User { get; set; }

        [ForeignKey("ProductCategory")]
        public int CategoryId {  get; set; }

        public  ProductCategory ProductCategory {  get; set; }

        public int ShippingAddressId {  get; set; }

        public string Tracking {  get; set; }
        
        public DateTime OrderDate {  get; set; }

        public Decimal TotalAmount { get; set; }



    }
}
