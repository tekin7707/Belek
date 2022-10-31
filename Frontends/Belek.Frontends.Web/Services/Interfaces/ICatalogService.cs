using Belek.Frontends.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CatalogViewModel>> GetAllCatalogsAsync();

        Task<List<CatalogViewModel>> GetAllCatalogsByCategoryIdAsync(int id);

        Task<CatalogViewModel> GetCatalogByIdAsync(int id);

        Task<bool> CreateCatalogAsync(CatalogCreateInput catalogCreateInput);

        Task<bool> UpdateCatalogAsync(CatalogUpdateInput catalogUpdateInput);

        Task<bool> DeleteCatalogAsync(int id);
    }
}