
using Belek.Frontends.Web.Models.Catalogs;
using Belek.Frontends.Web.Models.Orders;
using Belek.Frontends.Web.Services.Interfaces;
using Belek.Shared.Dtos;
using Belek.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public Task<List<OrderViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetAllByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}