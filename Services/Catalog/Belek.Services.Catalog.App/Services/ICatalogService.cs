using Belek.Services.Catalog.App.Dtos;
using Belek.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Services.Catalog.App.Services
{
    public interface ICatalogService
    {
        Task<Response<List<CatalogDto>>> GetAllAsync();

        Task<Response<CatalogDto>> GetByIdAsync(int id);
      
        Task<Response<List<CatalogDto>>> GetByCatagoryIdAsync(int categoryId);

        Task<Response<CatalogDto>> CreateAsync(CatalogCreateDto catalogDto);
     
        Task<Response<NoContent>> UpdateAsync(CatalogUpdateDto catalogUpdateDto);

        Task<Response<NoContent>> DeleteAsync(int id);
    }
}