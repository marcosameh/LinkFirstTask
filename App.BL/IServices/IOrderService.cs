using App.BL;
using App.Domain.Entities;
using App.Domain.Models;

namespace App.Infrastructure.IServices
{
    public interface IOrderService
    {
        public Task<ApiResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto);
        public Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderItem> orderItems);
    }
}
