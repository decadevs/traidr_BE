using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IAddressRepository
    {
        Task<bool> AddAddressAsync(Address address);
        Task<Address> GetAddressByUserIdAsync(string userId);
    }
}
