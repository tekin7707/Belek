using AutoMapper;
using AutoMapper.Internal.Mappers;
using Belek.Services.Catalog.App.Mapping;
using Belek.Services.Catalog.App.Dtos;
using Belek.Shared.Dtos;
using FreeCourse.Services.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Belek.Services.Catalog.Domain.Models;

namespace Belek.Services.Catalog.App.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;
        private readonly CatalogDbContext _catalogDbContext;

        public CatalogService(IMapper mapper, CatalogDbContext catalogDbContext)
        {
            _mapper = mapper;
            _catalogDbContext = catalogDbContext; 
        }

        public async Task<Response<List<CatalogDto>>> GetAllAsync()
        {
            var catalogs = await _catalogDbContext.Catalogs.ToListAsync();

            return Response<List<CatalogDto>>.Success(ObjectMapper.Mapper.Map<List<CatalogDto>>(catalogs), 200);
        }

        public async Task<Response<CatalogDto>> GetByIdAsync(int id)
        {
            var catalog = await _catalogDbContext.Catalogs.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (catalog == null)
            {
                return Response<CatalogDto>.Fail("Catalog not found", 404);
            }
            catalog.Category = await _catalogDbContext.Categories.FirstOrDefaultAsync(x => x.Id == catalog.CategoryId);
            CatalogDto catalogDto = ObjectMapper.Mapper.Map<CatalogDto>(catalog);
            return Response<CatalogDto>.Success(catalogDto, 200);
        }

        public async Task<Response<List<CatalogDto>>> GetByCatagoryIdAsync(int categoryId)
        {
            var catalogs = await _catalogDbContext.Catalogs.Where(x=>x.CategoryId==categoryId).ToListAsync();

            return Response<List<CatalogDto>>.Success(ObjectMapper.Mapper.Map<List<CatalogDto>>(catalogs), 200);
        }

        public async Task<Response<CatalogDto>> CreateAsync(CatalogCreateDto catalogDto)
        {
            catalogDto.CreatedDate = DateTime.Now;
            catalogDto.Status = Domain.Enums.StatusEnum.Active;
            var catalog = ObjectMapper.Mapper.Map<Domain.Models.Catalog>(catalogDto);
            await _catalogDbContext.AddAsync<Domain.Models.Catalog> (catalog);
            await _catalogDbContext.SaveChangesAsync();

            return Response<CatalogDto>.Success(ObjectMapper.Mapper.Map<CatalogDto>(catalog), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CatalogUpdateDto catalogUpdateDto)
        {
            var updateCatalog = ObjectMapper.Mapper.Map<Domain.Models.Catalog>(catalogUpdateDto);

            var catalog = await _catalogDbContext.Catalogs.Where(x => x.Id == catalogUpdateDto.Id).FirstOrDefaultAsync();

            if (catalog == null)
            {
                return Response<NoContent>.Fail("Catalog not found", 404);
            }
            try
            {
                catalog.CategoryId = updateCatalog.CategoryId;
                catalog.Name = updateCatalog.Name;
                catalog.Description = updateCatalog.Description;
                catalog.Price = updateCatalog.Price;
                catalog.Picture = updateCatalog.Picture;
                catalog.CreatedDate = DateTime.Now;
                catalog.UpdateDate = DateTime.Now;

                _catalogDbContext.Catalogs.Update(catalog);
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message, 500);
            }


            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var catalog = await _catalogDbContext.Catalogs.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (catalog == null)
            {
                return Response<NoContent>.Fail("Catalog not found", 404);
            }
            _catalogDbContext.Remove<Domain.Models.Catalog>(catalog);
            await _catalogDbContext.SaveChangesAsync();
            return Response<NoContent>.Fail("Course not found", 404);
        }
    }
}