using Belek.Frontends.Web.Models.Catalogs;
using Belek.Frontends.Web.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetAllAsync();

        Task<List<OrderViewModel>> GetAllByUserIdAsync(string userId);

        Task<OrderViewModel> GetByIdAsync(int id);


    }
}