using AutoMapper;
using Belek.Services.Catalog.App.Dtos;
using Belek.Services.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belek.Services.Catalog.App.Mapping
{
    internal class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Belek.Services.Catalog.Domain.Models.Catalog, CatalogDto>().ReverseMap();
            CreateMap<Belek.Services.Catalog.Domain.Models.Catalog, CatalogCreateDto>().ReverseMap();
            CreateMap<Belek.Services.Catalog.Domain.Models.Catalog, CatalogUpdateDto>().ReverseMap();
        }
    }
}