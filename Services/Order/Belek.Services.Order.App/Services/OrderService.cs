using Belek.Services.Order.App.Dtos;
using Belek.Services.Order.Db;
using Belek.Services.Order.Domain.Models;
using Belek.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Belek.Services.Order.App.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderService(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<OrderDto>> CreateAsync(OrderCreateDto orderCreateDto)
        {
            orderCreateDto.CreatedDate = DateTime.Now;
            //mapping
            await _orderDbContext.AddAsync<OrderModel>(new OrderModel());
            await _orderDbContext.SaveChangesAsync();

            return Response<OrderDto>.Success(new OrderDto(), 201);
        }

        public Task<Response<NoContent>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<OrderDto>>> GetAllAsync()
        {
            var items = await _orderDbContext.Orders.Select(x => new OrderDto
            {
                Id = x.Id,
                Status = x.Status,
                UserId = x.UserId,
                Items = _orderDbContext.OrderItems.Where(p => p.OrderId == x.Id).Select(p => new OrderItemDto
                {
                    OrderId = p.OrderId,
                    CatalogId = p.CatalogId,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()

            }).ToListAsync();

            return Response<List<OrderDto>>.Success(items, 200);
        }

        public async Task<Response<List<OrderDto>>> GetAllByUserIdAsync(string userId)
        {
            var items = await _orderDbContext.Orders.Where(x=>x.UserId==userId).Select(x => new OrderDto
            {
                Id = x.Id,
                Status = x.Status,
                UserId = x.UserId,
                Items = _orderDbContext.OrderItems.Where(p => p.OrderId == x.Id).Select(p => new OrderItemDto
                {
                    OrderId = p.OrderId,
                    CatalogId = p.CatalogId,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()

            }).ToListAsync();

            return Response<List<OrderDto>>.Success(items, 200);
        }

        public async Task<Response<OrderDto>> GetBeyIdAsync(int id)
        {
            var item = await _orderDbContext.Orders.Where(x=>x.Id==id).Select(x => new OrderDto
            {
                Id = x.Id,
                Status = x.Status,
                UserId = x.UserId,
                Items = _orderDbContext.OrderItems.Where(p => p.OrderId == x.Id).Select(p => new OrderItemDto
                {
                    OrderId = p.OrderId,
                    CatalogId = p.CatalogId,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()

            }).FirstOrDefaultAsync();

            return Response<OrderDto>.Success(item, 200);
        }

        public Task<Response<NoContent>> UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
