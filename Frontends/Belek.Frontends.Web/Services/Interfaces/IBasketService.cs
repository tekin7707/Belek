using Belek.Frontends.Web.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);

        Task<BasketViewModel> Get();

        Task<bool> Delete();

        Task AddBasketItem(BasketItemViewModel basketItemViewModel);

        Task<bool> RemoveBasketItem(int catalogId);

        Task<bool> ApplyDiscount(string discountCode);

        Task<bool> CancelApplyDiscount();
    }
}