using App.BL.Common;
using App.BL.DTOs;
using App.BL.IRepository;
using App.DAL.Context;
using App.DAL.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace App.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        List<string> Errors = new List<string>();
        public OrderRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApiResponse<Order> CreateOrder(OrderDto orderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                order.TotalPrice = CalculateTotalPrice(order.OrderItems);
                if (Errors.Count > 0)
                {
                    return new ApiResponse<Order>(false, string.Join("\n", Errors), null);
                }
                _context.Orders.Add(order);
                _context.SaveChanges();

                return new ApiResponse<Order>(true, "Order created successfully", order);
            }

            catch (Exception ex)
            {
                return new ApiResponse<Order>(false, $"{ex.Message}", null);
            }
        }

        private decimal CalculateTotalPrice(IEnumerable<OrderItem> orderItems)
        {
            decimal totalPrice = 0;

            foreach (var item in orderItems)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    totalPrice += product.Price * item.Quantity;
                }
                else
                {
                    Errors.Add($"Product Not Found {item.ProductId}");
                }
            }

            return totalPrice;
        }
    }
}
