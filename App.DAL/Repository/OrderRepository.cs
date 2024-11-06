using App.Domain.Entities;
using App.DAL;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;


        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return order.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> GetOrder(int orderId)
        {
            try
            {
                return await _context.Orders.Where(x=>x.Id==orderId)
                    .Include(x=>x.OrderItems)
                    .ThenInclude(x=>x.Product)
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
