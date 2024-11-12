using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using traidr.Application.Dtos.ProductDto;
using traidr.Application.Dtos.ResponseObjects;
using traidr.Application.Dtos.ShopDto;
using traidr.Application.IServices;
using traidr.Domain.ExceptionHandling.Exceptions;
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
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, 
            IPhotoService photoService, IProductElementRepository productElementRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _photoService = photoService;
            _productElementRepository = productElementRepository;
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductListingDto productDto)
        {
            
            if (productDto == null) return BadRequest(ApiResponse.Failed("Invalid data"));

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null) return BadRequest("User is null");

                var category = await _categoryRepository.GetCategoryByNameAsync(productDto.Category);                                    

                if (category == null)
                {
                    category = new ProductCategory { CategoryName = productDto.Category };
                    await _categoryRepository.AddProductCategoryAsync(category);
                }

                var product = new Product()
                { 
                    SellerId = userId,
                    ProductName = productDto.ProductName,
                    ProductDescription = productDto.Description,
                    Price = productDto.Price,
                    ProductCategoryId = category.CategoryId,                    
                    CreationDate = DateTime.UtcNow,
                };

                await _productRepository.AddProduct(product);
                var productImages = productDto.ProductImages;

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

        [Authorize]
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


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {

            var product = await _productRepository.FindProductByIdAsync(id);
            if (product == null)
            {
                throw new ResourceNotFound404("Product not found");
            }

            var productDto = new ProductDetailsDto
            {
                Id = product.ProductId,
                Name = product.ProductName,
                Description = product.ProductDescription,
                Price = product.Price,
                CategoryId = product.ProductCategoryId,
                Images = product.ProductImages.Select(img => img.ImageUrl).ToList(),
                Seller = new SellerDto()
                {
                    UserName = product.Seller.UserName,
                    Email = product.Seller.Email,
                    ShopName = product.Seller.Shop?.ShopName ?? $"{product.Seller.UserName} Shop"
                },
                Reviews = product.Reviews.Select(review => new ReviewDto
                {
                    Comment = review.Comment,
                    Rating = review.Rating,
                    CommentedAt = review.Date,
                    Reviewer = review.User?.UserName ?? "Anonymous",
                }).ToList() ?? new List<ReviewDto>(),
                ProductElements = product.ProductElements.Select(pe => new ProductElementDto
                {
                    Quantity = pe.QuantityInStock,
                    Color = pe.Color,
                    Sku = pe.Sku,
                }).ToList() ?? new List<ProductElementDto>(),
            };

            return Ok(ApiDataResponse<ProductDetailsDto>.Success(productDto, "Product retrieved successfully"));
        }


        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<ApiDataResponse<List<ProductDto>>>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindProductByCategoryIdAsync(categoryId);
            if (products == null || products.Count == 0)
            {
                throw new ResourceNotFound404("No products found in this category");

            }

            var productDto = products.Select(p => new ProductDto
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Description = p.ProductDescription,
                Price = p.Price,
                Images = p.ProductImages.Select(img => img.ImageUrl).ToList(),
            }).ToList();
            return Ok(ApiDataResponse<List<ProductDto>>.Success(productDto, "Products retrieved successfully"));

        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<ApiDataResponse<List<Product>>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductAsync();
            if (products == null || products.Count == 0)
            {
                throw new ResourceNotFound404("No products available");
            }

            var productDto = products.Select(p => new ProductDto
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Description = p.ProductDescription,
                CategoryId = p.ProductCategoryId,
                Price = p.Price,
                Images = p.ProductImages.Select(img => img.ImageUrl).ToList(),
            }).ToList();

            return Ok(ApiDataResponse<List<ProductDto>>.Success(productDto, "All products retrieved successfully"));
        }

        [HttpGet("{productId}/product-reviews")]
        public async Task<IActionResult> FindProductAndReviewsAsync(int productId)
        {
            if (productId == null) return BadRequest();

            var reviews = await _productRepository.GetProductReviewsAsync(productId);
           
            if (reviews == null || reviews.Count == 0)
            { 
                return BadRequest(); 
            }

            return Ok(reviews);
        }

        [HttpGet("Productcategory/{id}")]
        public ActionResult<ProductCategory> GetCategoryById(int id)
        {
            var category = _productRepository.FindProductByCategoryIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(category);
        }

        // Endpoint to get product elements by Product ID
        [HttpGet("product/{id}/elements")]
        public ActionResult<List<ProductElement>> GetProductElementsByProductId(int id)
        {
            var elements = _productElementRepository.GetProductElementsByProductId(id);
            if (elements == null || elements.Count == 0)
            {
                return NotFound(new { message = $"Product elements for product ID {id} not found." });
            }

            return Ok(elements);
        }


        [Authorize]
        [HttpPost("{productId}/add-reviews")]
        public async Task<IActionResult> AddReview(int productId, [FromBody] ReviewDto reviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return BadRequest();

            var review = new Review()
            {
                UserId = userId,
                ProductId = productId,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating,
            };

            return Ok("Review Added successfully");
        }
    }
}

