using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Interfaces;
using asp_net_ecommerce_web_api.Models;
using asp_net_ecommerce_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("v1/api/categories/")]
    public class CategoryController : ControllerBase
    {
        
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /v1/api/categories ==> Read categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            var categoryList = _categoryService.GetAllCategories();
            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, 200, "Categories returned successfully")); // 200
        }

        // GET: /v1/api/categories/{categoryId} ==> Read a category by Id
        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {
            var category =  _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 404, "Validation failed"));
            } 
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(category, 200, "Category is returned successfully")); // 200
        }

        // POST: /v1/api/categories ==> Create category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            var categoryReadDto = _categoryService.CreateCategory(categoryData);
            return Created(nameof(GetCategoryById), ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, 201, "Categories created successfully")); // 200
        }

        // PUT: /v1/api/categories/{categoryId} ==> Update a category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {

            var updateCategory = _categoryService.UpdateCategoryById(categoryId, categoryData);
            if (updateCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 400, "Validation failed"));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(updateCategory, 200, "Category updated successfully"));
        }

        // // DELETE: /v1/api/categories/{categoryId} ==> Delete a category by Id
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
        {
            
            var foundCategory = _categoryService.DeleteCategoryById(categoryId);
            if (!foundCategory)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 404, "Validation failed"));
            }
            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category deleted successfully"));
        }
    }
}

// 1. Descriptive name
// 2. plurals
// 3. plurals/{singularNoun} ==> categories/{categoryId}
// 4. use hypens for multiple words for improving the readability ==> /product-categories
// 5. versioning 
// 6. avoid verbs in url path ==> not --> /createCategory , instead --> POST  /category

// GET /api/categories ==> Request ==> Controller ==> Services ==> Controller ==> Response