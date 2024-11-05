using App.BL.Common;
using App.BL.DTOs;
using App.DAL.Entities;

namespace App.BL.IRepository
{
    public interface IOrderRepository
    {
        public ApiResponse<OrderDto> CreateOrder(OrderDto orderDto);
    }
}
