﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class WishListItem
    {
        public int Id { get; set; }

        [ForeignKey("WishList")]
        public int WishListId { get; set; }

        public WishList WishList { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

       

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
