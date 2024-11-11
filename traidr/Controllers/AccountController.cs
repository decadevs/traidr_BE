using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using traidr.Application.Dtos.ResponseObjects;
using traidr.Application.Dtos.UserDto;
using traidr.Application.IServices;
using traidr.Domain.Context.PreSeeding;
using traidr.Domain.ExceptionHandling.Exceptions;
using traidr.Domain.Models;

namespace traidr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

   
        [HttpPost("sign-up")]
        public async Task<IActionResult> RegisterUser([FromBody] UserSignupDto signupDto)
        {
          
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(equals => equals.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse.Failed(errors));
            }
          
            var userExist = await _userManager.FindByEmailAsync(signupDto.Email);

            if (userExist != null)
            {
                throw new ConflictError409("Email already exists.");
            }

            var newUser = new AppUser()
            {
                UserName = signupDto.UserName,
                FirstName = signupDto.UserName,
                Email = signupDto.Email,                
                ReferralSource = signupDto.ReferralSource,
            };

            var userResult = await _userManager.CreateAsync(newUser, signupDto.Password);


            if (!userResult.Succeeded)
            {
                throw new DatabaseUpdateError("An error occurred while registering the user.");
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            if (!roleResult.Succeeded)
            {
                throw new DatabaseUpdateError("An error occurred while registering the user.");
            }

            return Ok(ApiDataResponse<AppUser>.Success(signupDto));
            
        }

        [HttpPost("GoogleAuth")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        {
            var clientId = "YOUR_GOOGLE_CLIENT_ID.apps.googleusercontent.com";

            
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.Token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { clientId }
            });

            
            var appUser = await _userManager.FindByEmailAsync(payload.Email);
            if (appUser == null)
            {
                appUser = new AppUser
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    EmailConfirmed = true 
                };

                var result = await _userManager.CreateAsync(appUser);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                // Optionally, add other claims if needed
                var roleResult = await _userManager.AddToRoleAsync(appUser, UserRoles.User);

                if (!roleResult.Succeeded)
                {
                    throw new DatabaseUpdateError("An error occurred while registering the user.");
                }
                await _userManager.AddClaimAsync(appUser, new System.Security.Claims.Claim("GoogleId", payload.Subject));
            }

            await _signInManager.SignInAsync(appUser, isPersistent: false);

            var token = _tokenService.CreateToken(appUser);

            return Ok(new { appUser, token });
        }
    


    [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null) return Unauthorized("Invalid email address");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!isValidPassword)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new 
            {
                user.Id,
                Message = "Login successful",
                user.Email,
                Token = _tokenService.CreateToken(user),
            });
                     
        }
    }
}
