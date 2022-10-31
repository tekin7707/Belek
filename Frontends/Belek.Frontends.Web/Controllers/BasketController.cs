using Belek.Frontends.Web.Models.Baskets;
using Belek.Frontends.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(int catalogId)
        {
            var catalog = await _catalogService.GetCatalogByIdAsync(catalogId);

            var basketItem = new BasketItemViewModel { CatalogId = catalog.Id, CatalogName = catalog.Name, Price = catalog.Price };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(int catalogId)
        {
            var result = await _basketService.RemoveBasketItem(catalogId);

            return RedirectToAction(nameof(Index));
        }
    }
}