using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using traidr.Application.Dtos.ResponseObjects;
using traidr.Application.IServices;
using traidr.Domain.ExceptionHandling.Exceptions;
using traidr.Domain.Helper;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;
using traidr.Domain.Repository;

namespace traidr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] QueryObject query)
        {
            if (query != null)
            {
                var users = await _appUserRepository.GetAllAppUser(query);

                if (users != null) 
                {
                    return Ok(users);
                }
            }


            throw new ResourceNotFound404("Users not found");
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            if (id == null) return BadRequest();
            
                var user = await _appUserRepository.GetAppUserById(id);

                if (user != null) 
                {
                    return Ok(user);
                }
            


            throw new ResourceNotFound404("User not found");
        }
    }
}
