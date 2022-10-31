using Belek.Services.Catalog.App.Dtos;
using Belek.Services.Catalog.App.Services;
using Belek.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : CustomBaseController
    {
        private readonly ICatalogService _catalogService;

        public CatalogsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _catalogService.GetAllAsync();

            return CreateActionResultInstance(items);
        }

        ////catalog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _catalogService.GetByIdAsync(id);

            return CreateActionResultInstance(item);
        }


        [HttpGet]
        [Route("/api/[controller]/GetAllByCategoryId/{id}")]
        public async Task<IActionResult> GetCatalogsByCatagoryId(int id)
        {//http://localhost:5000/services/Catalog/catalogs/GetAllByCategoryId/1
            var response = await _catalogService.GetByCatagoryIdAsync(id);

            return CreateActionResultInstance(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CatalogCreateDto catalogDto)
        {
            var response = await _catalogService.CreateAsync(catalogDto);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CatalogUpdateDto catalogUpdateDto)
        {
            var response = await _catalogService.UpdateAsync(catalogUpdateDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _catalogService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}