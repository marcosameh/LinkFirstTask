using App.BL.Common;
using App.BL.DTOs;
using App.BL.IRepository;
using App.DAL.Context;
using App.DAL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.BL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly List<string> _errors = new List<string>();

        public OrderRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                order.TotalPrice = await CalculateTotalPriceAsync(order.OrderItems);

                if (_errors.Count > 0)
                {
                    return new ApiResponse<OrderDto>(false, _errors, null);
                }

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                var savedOrder = await _context.Orders
                                       .Include(o => o.OrderItems)
                                       .FirstOrDefaultAsync(o => o.Id == order.Id);

                var responseDto = _mapper.Map<OrderDto>(savedOrder);
                return new ApiResponse<OrderDto>(true, new List<string>(), responseDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderDto>(false, new List<string> { ex.Message }, null);
            }
        }

        private async Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderItem> orderItems)
        {
            decimal totalPrice = 0;

            foreach (var item in orderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    totalPrice += product.Price * item.Quantity;
                }
                else
                {
                    _errors.Add($"Product ID: {item.ProductId} Not Found");
                }
            }

            return totalPrice;
        }
    }
}
