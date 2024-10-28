using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using traidr.Application.Dtos.ProductDto;
using traidr.Application.Dtos.ResponseObjects;
using traidr.Application.IServices;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;
        private readonly IProductElementRepository _productElementRepository;

        public ProductController(IProductRepository productRepository, 
            IPhotoService photoService, IProductElementRepository productElementRepository)
        {
            _productRepository = productRepository;
            _photoService = photoService;
            _productElementRepository = productElementRepository;
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductListingDto productDto, [FromForm] List<IFormFile> productImages)
        {
            if (ModelState.IsValid)
            {
                //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);         

                var product = new Product()
                {
                    SellerId = "df31d370-0ce9-47de-ba63-c56670e991dd",
                    ProductName = productDto.Title,
                    ProductDescription = productDto.Description,
                    Price = productDto.Price,
                    ProductCategoryId = productDto.CategoryId,                    
                    CreationDate = DateTime.UtcNow,
                };

                await _productRepository.AddProduct(product);


                if (productImages != null)
                {
                    var uploadedProductImages = new List<ProductImage>();

                    foreach (var image in productImages)
                    {
                        var uploadResult = await _photoService.AddPhotoAsync(image);

                        if (uploadResult != null)
                        {
                            var productImage = new ProductImage
                            {
                                ProductId = product.ProductId,
                                ImageUrl = uploadResult.Uri.ToString(),
                                publicId = uploadResult.PublicId,
                            };
                            uploadedProductImages.Add(productImage);
                        }
                    }

                    await _productRepository.AddProductImagesAsync(uploadedProductImages);
                }

                return Ok(ApiResponse.Success(product));
            }
            return BadRequest(ApiResponse.Failed("Invalid data"));
        }


        [HttpPost("{productId}/elements")]
        public async Task<IActionResult> CreateProductElement(int productId, [FromBody] List<ProductElementDto> elementDto)
        {
            //var product = await _productRepository.GetByProductIdAsync(productId);

            if (productId == 0 || productId == null)
            {
                return NotFound(ApiResponse.Failed($"Product with ID {productId} not found"));
            }

            var elements = elementDto.Select(dto => new ProductElement
            {
                ProductId = productId,
                QuantityInStock = dto.Quantity,
                Sku = dto.Sku,
            }).ToList();

            await _productElementRepository.AddProductElementsAsync(elements);

            return Ok(ApiResponse.Success(elements));
        }

    }

}
