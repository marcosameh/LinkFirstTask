using App.BL.DTOs;
using App.BL.Models;
using App.DAL;
using App.DAL.IServices;
using App.Domain.Entities;
using App.Domain.Enum;
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
        public async Task<ApiResponse<int>> CreateOrderAsync(CreateOrderDto orderDto)
        {
            try
            {
                var order = mapper.Map<Order>(orderDto);                
                foreach (var item in order.OrderItems)
                {
                    var product = await productRepository.Get(item.ProductId);
                    if (product == null)
                    {
                        _errors.Add($"Product ID: {item.ProductId} Not Found");
                    }
                   
                }
                if (_errors.Any())
                {
                    return new ApiResponse<int>(false, _errors, default(int));
                }
                order.OrderDate = DateTime.Now;
                order.StatusId = (int)OrderStatusEnum.Pending;
                var orderId = await orderRepository.CreateOrderAsync(order);
                return new ApiResponse<int>(true, new List<string>(), orderId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, new List<string> { ex.Message }, default(int));
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

        public async Task<ApiResponse<OrderDto>> GetOrder(int orderId)
        {
            try
            {
                var order= await orderRepository.GetOrder(orderId);
                if (order == null)
                {
                    return new ApiResponse<OrderDto>(false, new List<string> { "order not found" }, null);
                }
                var orderDto = mapper.Map<OrderDto>(order);
                orderDto.TotalPrice = await CalculateTotalPriceAsync(order.OrderItems);
                return new ApiResponse<OrderDto>(true, new List<string>() , orderDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderDto> (false, new List<string>() { ex.Message },null );
            }
        }

        public async Task<ApiResponse<OrderDto>> SubmitOrderAsync(SubmitOrderDto submitOrderDto)
        {
            try
            {
                var order = await orderRepository.GetOrder(submitOrderDto.OrderId);
                if (order == null)
                {
                    return new ApiResponse<OrderDto>(false, new List<string> { "Order Not Found" }, null);
                }
                if (order.StatusId == (int)OrderStatusEnum.Submitted)
                {
                    return new ApiResponse<OrderDto>(false, new List<string> { "order Already Submitted" }, null);
                }
                order.TotalPrice = await CalculateTotalPriceAsync(order.OrderItems);
                order.StatusId = (int)OrderStatusEnum.Submitted;
                order.CustomerAddress = submitOrderDto.CustomerAddress;
                order.CustomerName = submitOrderDto.CustomerName;
                order.CustomerPhone = submitOrderDto.CustomerPhone;
                foreach (var orderItem in order.OrderItems)
                {
                    orderItem.UnitPrice = orderItem.Product.Price;
                }
                await orderRepository.SaveChangesAsync();
                var orderDto = mapper.Map<OrderDto>(order);
                return new ApiResponse<OrderDto>(true, new List<string>(), orderDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderDto>(false, new List<string>() { ex.Message }, null);
            }
        }
    }
}
