using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using traidr.Application.IServices;
using traidr.Domain.Context;
using traidr.Domain.Models;

namespace traidr.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly ApplicationDbContext _context;
        public TokenService(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
            _securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["JWT:SigningKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var roleId = _context.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).FirstOrDefault();
            var role = _context.Roles.Where(r => r.Id == roleId).Select(r => r.Name).FirstOrDefault();

            var claim = new List<Claim>
         {
             new Claim(JwtRegisteredClaimNames.Email, user.Email),
             new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
             new Claim(JwtRegisteredClaimNames.NameId, user.Id),
             new Claim(ClaimTypes.Role, role),
         };

            var credential = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credential,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
