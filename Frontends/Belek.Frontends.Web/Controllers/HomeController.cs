using Belek.Frontends.Web.Models;
using Belek.Frontends.Web.Models.Catalogs;
using Belek.Frontends.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Belek.Frontends.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ICatalogService catalogService)
        {
            _logger = logger;
            _configuration = configuration;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _catalogService.GetAllCatalogsAsync();
            if (items == null)
                items = new List<CatalogViewModel>();
            return View(items);

            return View();

        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _catalogService.GetCatalogByIdAsync(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}