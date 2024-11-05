using App.Domain.Entities;

namespace App.Infrastructure
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrderAsync(Order order);
    }
}
