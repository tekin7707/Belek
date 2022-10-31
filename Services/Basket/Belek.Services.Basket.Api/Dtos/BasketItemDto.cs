using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Services.Basket.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }

        public int CatalogId { get; set; }
        public string CatalogName { get; set; }

        public decimal Price { get; set; }
    }
}