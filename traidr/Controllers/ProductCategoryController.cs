using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using traidr.Application.Dtos.ProductDto;
using traidr.Domain.Helper;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductCategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            if (categories == null)
            {
                return NotFound();
            }
            var categoryDto = categories.Select(c => new ProductCategoryDto
            {
                Id = c.CategoryId,
                CategoryName = c.CategoryName,
            }).ToList();
            return Ok(categoryDto);
        } 
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new ProductCategoryDto
            {
                Id = category.CategoryId,
                CategoryName = category.CategoryName,
            };
            return Ok(categoryDto);
        }

        [HttpPost("/add-category")]
        public async Task<IActionResult> AddCategory([FromBody] ProductCategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(ModelState);
            }

            var category = new ProductCategory
            {
                CategoryName = categoryDto.CategoryName,
            };

            await _categoryRepository.AddProductCategoryAsync(category);


            return Ok("Product Category added successfully");

        }
    }
}
