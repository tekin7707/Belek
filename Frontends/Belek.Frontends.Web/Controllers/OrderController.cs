using Microsoft.AspNetCore.Mvc;

namespace Belek.Frontends.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
