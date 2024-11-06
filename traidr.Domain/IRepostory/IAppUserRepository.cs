using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Helper;
using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetAllAppUser(QueryObject query);
        Task<AppUser> GetAppUserById(string id);
        

    }
}
