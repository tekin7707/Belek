using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belek.Services.Order.App.Dtos
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int CatalogId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
