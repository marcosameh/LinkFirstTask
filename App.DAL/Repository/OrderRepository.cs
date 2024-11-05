using App.Domain.Entities;
using App.Infrastructure;
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

        public async Task<Order> CreateOrderAsync(Order order)
        {
            try
            {               
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                var savedOrder = await _context.Orders
                                       .Include(o => o.OrderItems)
                                       .FirstOrDefaultAsync(o => o.Id == order.Id);


                return savedOrder;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      
    }
}
