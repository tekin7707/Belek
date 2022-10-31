using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Models.Baskets
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;

        public int CatalogId { get; set; }
        public string CatalogName { get; set; }

        public double Price { get; set; }

        private double? DiscountAppliedPrice;

        public double GetCurrentPrice
        {
            get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
        }

        public void AppliedDiscount(double discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}