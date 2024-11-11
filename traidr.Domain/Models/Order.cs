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


        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }



        [ForeignKey("ProductCategory")]
        public int CategoryId {  get; set; }
        public  ProductCategory ProductCategory {  get; set; }



        [ForeignKey("ShippingDetail")]
        public int AddressId {  get; set; }
        public Address ShippingDetail { get; set; }


       /* [ForeignKey("Tracking")]
        public int TrackingId {  get; set; }*/
        public Tracking Tracking { get; set; }


        
        public DateTime OrderDate {  get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public ICollection<Product> Products { get; set; }



    }
}
