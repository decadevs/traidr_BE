using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using traidr.Application.Dtos.ShopDto;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;

        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        // POST: api/Shop
        [HttpPost("add-shop")]
        public async Task<IActionResult> AddShop([FromBody] CreateShopDto shopDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Unauthorized();

            var contact = new Shop
            {
                ShopName = shopDto.ShopName,
                SellerId = userId,    
                Description = shopDto.Category,
            };

            await _shopRepository.AddShopAsync(contact);
            return Ok(contact);
        }

        // GET: api/Shop
        [HttpGet]
        public async Task<ActionResult<List<Shop>>> GetAllShops()
        {
            var shops = await _shopRepository.GetAllShopsAsync();
            if (shops.Count == 0)
            {
                return BadRequest("No Shop Available");    
            }

            return Ok(shops);
        }

        // GET: api/Shop/{sellerId}
        [HttpGet("shop-owner")]
        public async Task<ActionResult<Shop>> GetShopBySellerId()
        {
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shop = await _shopRepository.GetShopBySellerIdAsync(sellerId);

            if (shop == null)
            {
                return NotFound($"No shop found for SellerId: {sellerId}");
            }

            return Ok(shop);
        }
        
        [HttpGet("shop-inventories")]
        public async Task<ActionResult<Shop>> GetShopAndProductsBySellerId()
        {

            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shopInventories = await _shopRepository.GetShopAndProductAsync(sellerId);

            if (shopInventories == null)
            {
                return NotFound($"No shop found for SellerId: {sellerId}");
            }

            return Ok(shopInventories);
        }

        // PUT: api/Shop
        [HttpPut("update-shop")]
        public async Task<IActionResult> UpdateShop([FromBody] Shop shop)
        {
            if (shop == null)
            {
                return BadRequest("Shop data is null.");
            }

            await _shopRepository.UpdateShopAsync(shop);
            return NoContent();
        }
    }
}
