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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly CatalogDbContext _catalogDbContext;

        public CategoryService(IMapper mapper, CatalogDbContext catalogDbContext)
        {
            _mapper = mapper;
            _catalogDbContext = catalogDbContext; 
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _catalogDbContext.Categories.ToListAsync();

            return Response<List<CategoryDto>>.Success(ObjectMapper.Mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = ObjectMapper.Mapper.Map<Domain.Models.Category>(categoryDto);
            category.CreatedDate = DateTime.Now;
            await _catalogDbContext.AddAsync<Domain.Models.Category>(category);
            await _catalogDbContext.SaveChangesAsync();
            return Response<CategoryDto>.Success(ObjectMapper.Mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _catalogDbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }

            return Response<CategoryDto>.Success(ObjectMapper.Mapper.Map<CategoryDto>(category), 200);
        }
    }
}