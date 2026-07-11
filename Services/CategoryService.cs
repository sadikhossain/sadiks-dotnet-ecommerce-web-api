using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.Controllers;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace asp_net_ecommerce_web_api.Services
{
    public class CategoryService
    {
        private static readonly List<Category> _categories = new List<Category>();
        public List<CategoryReadDto> GetAllCategories()
        {
            return _categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt

            }).ToList();
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return null;
            }
            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };
            _categories.Add(newCategory);

            return new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };
        }

        // public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        // {
            
        // }
    }
}