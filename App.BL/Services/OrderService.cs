using App.Domain.Entities;
using App.Domain.Enum;
using App.Domain.Models;
using App.Infrastructure;
using App.Infrastructure.IServices;
using AutoMapper;

namespace App.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly List<string> _errors = new List<string>();
        public OrderService(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto)
        {
            try
            {
                var order = mapper.Map<Order>(orderDto);
                order.TotalPrice = await CalculateTotalPriceAsync(order.OrderItems);
                if (_errors.Any())
                {
                    return new ApiResponse<OrderDto>(false, _errors, null);
                }
                order.OrderDate = DateTime.Now;
                order.StatusId = (int)OrderStatusEnum.Submitted;
                await orderRepository.CreateOrderAsync(order);
                return new ApiResponse<OrderDto>(true, new List<string>(), orderDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderDto>(false, new List<string> { ex.Message }, null);
            }

        }
        public async Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderItem> orderItems)
        {
            decimal totalPrice = 0;

            foreach (var item in orderItems)
            {
                var product = await productRepository.Get(item.ProductId);
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
