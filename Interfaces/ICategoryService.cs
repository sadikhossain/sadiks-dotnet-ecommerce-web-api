using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;

namespace asp_net_ecommerce_web_api.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();
        CategoryReadDto? GetCategoryById(Guid categoryId);
        CategoryReadDto CreateCategory(CategoryCreateDto categoryData);
        CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData);
        bool DeleteCategoryById(Guid categoryId);
    }
}