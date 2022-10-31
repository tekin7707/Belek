
using Belek.Frontends.Web.Models.Catalogs;
using Belek.Frontends.Web.Services.Interfaces;
using Belek.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        //private readonly IPhotoStockService _photoStockService;
        //private readonly PhotoHelper _photoHelper;

        //public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        public CatalogService(HttpClient client)
        {
            _client = client;
            //_photoStockService = photoStockService;
            //_photoHelper = photoHelper;
        }

        public async Task<List<CatalogViewModel>> GetAllCatalogsAsync()
        {
            var response = await _client.GetAsync("catalogs");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CatalogViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CatalogViewModel>> GetAllCatalogsByCategoryIdAsync(int id)
        {
            var response = await _client.GetAsync($"catalogs/GetCatalogsByCatagoryId/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CatalogViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<CatalogViewModel> GetCatalogByIdAsync(int id)
        {
            var response = await _client.GetAsync($"catalogs/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CatalogViewModel>>();

            return responseSuccess.Data;
        }

        public async Task<bool> CreateCatalogAsync(CatalogCreateInput catalogCreateInput)
        {
            var response = await _client.PostAsJsonAsync<CatalogCreateInput>("catalogs", catalogCreateInput);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCatalogAsync(CatalogUpdateInput catalogUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<CatalogUpdateInput>("catalogs", catalogUpdateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCatalogAsync(int id)
        {
            var response = await _client.DeleteAsync($"catalogs/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}