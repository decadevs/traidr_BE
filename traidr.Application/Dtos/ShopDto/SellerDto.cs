using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Application.Dtos.ShopDto
{
    public class SellerDto
    {
       public string ShopName { get; set; }
        public string UserName { get; set; }
      
        public string Email { get; set; }

    }
}
