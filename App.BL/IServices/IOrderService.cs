using App.BL;
using App.BL.DTOs;
using App.BL.Models;
using App.Domain.Entities;

namespace App.DAL.IServices
{
    public interface IOrderService
    {
        public Task<ApiResponse<int>> CreateOrderAsync(CreateOrderDto orderDto);
        public Task<ApiResponse<OrderDto>> SubmitOrderAsync(SubmitOrderDto orderDto);
        public Task<ApiResponse<OrderDto>> GetOrder(int orderId);
        public Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderItem> orderItems);
    }
}
