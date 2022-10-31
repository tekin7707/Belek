using Belek.Frontends.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<CategoryViewModel> GetCategoryByIdAsync(int id);

        //Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput);

        //Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput);

        Task<bool> DeleteCategoryAsync(int id);
    }
}