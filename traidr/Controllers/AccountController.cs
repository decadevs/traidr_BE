using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmailSendingService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IEmailSendingService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
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
            var clientId = "754362326099-1jchbuf84tuq745ddk4k2cijhe91n3e9.apps.googleusercontent.com";

            
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.Token, 
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });

            
            var appUser = await _userManager.FindByEmailAsync(payload.Email);
            if (appUser == null)
            {
                appUser = new AppUser
                {
                    UserName = payload.FamilyName,
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

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UpdatePasswordDto model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            // Attempt to change the password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to change password", errors = result.Errors });
            }

            return Ok(new { message = "Password changed successfully" });
        }


        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Return a success response to prevent email enumeration attacks
                return BadRequest(new { message = "Invalid user email" });
            }

            // Generate the password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (token == null) return BadRequest(new { message = "Failed to generate reset Link" });
            // Generate reset link
            var resetLink = $"http://localhost:5173/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";

            // Send email
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Please reset your password by clicking here: {resetLink}");

            return Ok(new { message = "If your email is registered, you will receive a reset link." });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user email." });
            }

            // Attempt to reset the password using the token
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to reset password", errors = result.Errors });
            }

            return Ok(new { message = "Password has been reset successfully" });
        }
    }
}
