using App.BL.Common;
using App.BL.DTOs;

namespace App.BL.IRepository
{
    public interface IOrderRepository
    {
        public Task<ApiResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto);
    }
}
