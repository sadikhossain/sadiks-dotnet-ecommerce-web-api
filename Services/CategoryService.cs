using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.Controllers;
using asp_net_ecommerce_web_api.data;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Interfaces;
using asp_net_ecommerce_web_api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace asp_net_ecommerce_web_api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        
        public async Task<List<CategoryReadDto>> GetAllCategories()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            return foundCategory == null ? null : _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            // CategoryCreateDto ==> Category
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.Description = categoryData.Description;
            await _appDbContext.Categories.AddAsync(newCategory);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task<CategoryReadDto?> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return null;
            }

            // CategoryUpdateDto ==> Category
            _mapper.Map(categoryData, foundCategory);
            // foundCategory.Name = categoryData.Name;
            // foundCategory.Description = categoryData.Description;
            _appDbContext.Categories.Update(foundCategory);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<bool> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return false;
            }
            _appDbContext.Categories.Remove(foundCategory);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}