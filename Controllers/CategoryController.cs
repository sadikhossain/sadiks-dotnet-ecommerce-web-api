using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        // GET: /api/categories ==> Read categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            // if (!string.IsNullOrEmpty(searchValue))
            // {
            //     Console.WriteLine($"{searchValue}");
            //     var searchCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            //     return Ok(searchCategories);
            // }

            var categoryList = categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt

            }).ToList();
            return Ok(categoryList); // 200
        }

        // POST: /api/categories ==> Create category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category Name is required and can not be empty");
            }
            if (categoryData.Name.Length < 2)
            {
                return BadRequest("Category name must be at least 2 characters.");
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };
            categories.Add(newCategory);

            var categoryReadDto = new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };
            return Created($"/api/categories/{newCategory.CategoryId}", categoryReadDto); // 200
        }

        // POST: /api/categories ==> Create category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            if (categoryData == null)
            {
                return BadRequest("Category data is missing");
            }

            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return NotFound("Category with this id does not exist");
            }

            // foundCategory.Name = categoryData.Name ?? foundCategory.Name;
            // foundCategory.Description = categoryData.Description ?? foundCategory.Description;
            if (!string.IsNullOrEmpty(categoryData.Name))
            {
                if (categoryData.Name.Length > 2)
                {
                    foundCategory.Name = categoryData.Name;
                }
                else
                {
                    return BadRequest("Category name must be at least 2 characters.");
                }
            }
            if (!string.IsNullOrWhiteSpace(categoryData.Description))
            {
                foundCategory.Description = categoryData.Description;
            }
            // categories.Remove(foundCategory);
            return NoContent();
        }


        // DELETE: /api/categories/{categoryId} ==> Delete a category by Id
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return NotFound("Category with this id does not exist");
            }
            categories.Remove(foundCategory);
            return NoContent();
        }
    }
}