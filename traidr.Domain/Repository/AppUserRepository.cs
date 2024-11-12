using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Context;
using traidr.Domain.Helper;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Domain.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        public readonly ApplicationDbContext _context;

        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AppUser>> GetAllAppUser(QueryObject query)
        {
            var appUsers = _context.Users.AsQueryable();
            
            //pagination
            var skipNumber = (query.PageNumber-1) * query.PageSize;

            return await appUsers.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<AppUser> GetAppUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
