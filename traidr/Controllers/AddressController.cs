using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using traidr.Application.Dtos.ResponseObjects;
using traidr.Application.Dtos.AddressDtos;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace traidr.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        // POST: api/Address
        [HttpPost("add-address")]
        public async Task<ActionResult> AddAddress([FromBody] AddressDto addressDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return BadRequest("User cannot be null");

            var address = new Address
            {
                UserId = userId,
                Street = addressDto.Street,
                City = addressDto.City,
                State = addressDto.State
            };

            var success = await _addressRepository.AddAddressAsync(address);
            if (!success)
            {
                return StatusCode(500, ApiDataResponse<Address>.Failed(null, "Failed to add address"));
            }

            return CreatedAtAction(nameof(AddAddress), new { id = address.AddressId },
                ApiDataResponse<Address>.Success(address, "Address added successfully"));
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiDataResponse<Address>>> GetAddressByUserId(string userId)
        {
            var address = await _addressRepository.GetAddressByUserIdAsync(userId);
            if (address == null)
            {
                return NotFound(ApiDataResponse<Address>.Failed(null, "Address not found for this user"));
            }
            return Ok(ApiDataResponse<Address>.Success(address, "Address retrieved successfully"));
        }
    }
}

