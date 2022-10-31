using Belek.Shared.Services;
using Belek.Frontends.Web.Models.Catalogs;
using Belek.Frontends.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Drawing.Imaging;

namespace Belek.Frontends.Web.Controllers
{
    [Authorize]
    public class CatalogsController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ICategoryService _categoryService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CatalogsController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService, ICategoryService categoryService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _catalogService.GetAllCatalogsAsync();
            if (items == null)
                items = new List<CatalogViewModel>();
            return View(items);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogCreateInput catalogCreateInput)
        {
            catalogCreateInput.UserId = _sharedIdentityService.GetUserId;

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.CreateCatalogAsync(catalogCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var catalog = await _catalogService.GetCatalogByIdAsync(id);
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (catalog == null)
            {
                //mesaj göster
                RedirectToAction(nameof(Index));
            }
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", catalog.Id);
            CatalogUpdateInput catalogUpdateInput = new()
            {
                Id = catalog.Id,
                Name = catalog.Name,
                Description = catalog.Description,
                Price = catalog.Price,
                CategoryId = catalog.CategoryId ?? 0,
                UserId = catalog.UserId,
                Picture = catalog.Picture
            };

            return View(catalogUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CatalogUpdateInput catalogUpdateInput)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", catalogUpdateInput.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }

            var err = "";

            var photo = catalogUpdateInput.PhotoFormFile;
            var randomFileName = $"{catalogUpdateInput.Id}_{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";
            var ms = new MemoryStream();
            await photo.CopyToAsync(ms);

            using var thumbMs = await BelekTools.MakeThumbnailAsync(ms);

            err = await BelekTools.saveBlobAsync("tekin001", $"thumbs/{randomFileName}", thumbMs);

                if (string.IsNullOrEmpty(err))
                    BelekTools.saveBlobAsync("tekin001", randomFileName, ms);

            if (string.IsNullOrEmpty(err))
                catalogUpdateInput.Picture = randomFileName;

            await _catalogService.UpdateCatalogAsync(catalogUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        //}

        public async Task<IActionResult> Delete(int id)
        {
            await _catalogService.DeleteCatalogAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}