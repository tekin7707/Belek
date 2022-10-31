using Belek.Services.Order.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belek.Services.Order.App.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto>? Items { get; set; }
    }
}
