using App.Domain.Entities;

namespace App.DAL
{
    public interface IOrderRepository
    {
        public Task<int> CreateOrderAsync(Order order);
        public Task<Order> GetOrder(int orderId);
        Task SaveChangesAsync();
    }
}
