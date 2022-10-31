using Belek.Services.Order.App.Dtos;
using Belek.Shared.Dtos;

namespace Belek.Services.Order.App.Services
{
    public interface IOrderService
    {
        Task<Response<List<OrderDto>>> GetAllAsync();

        Task<Response<OrderDto>> GetBeyIdAsync(int id);

        Task<Response<List<OrderDto>>> GetAllByUserIdAsync(string userId);

        Task<Response<OrderDto>> CreateAsync(OrderCreateDto orderCreateDto);

        Task<Response<NoContent>> UpdateAsync(OrderUpdateDto orderUpdateDto);

        Task<Response<NoContent>> DeleteAsync(int id);
    }
}
