using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Context;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Domain.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Address> GetAddressByUserIdAsync(string userId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}
