using Belek.Services.Order.App.Services;
using Belek.Shared.ControllerBases;
using Belek.Shared.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Belek.Services.Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IOrderService orderService, ISharedIdentityService sharedIdentityService)
        {
            _orderService = orderService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _orderService.GetAllAsync();

            return CreateActionResultInstance(items);
        }

        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Create([FromBody] string value)
        {
        }

        public void Update(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
