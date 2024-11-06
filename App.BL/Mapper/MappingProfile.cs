using App.BL.DTOs;
using App.Domain.Entities;
using AutoMapper;

namespace App.BL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            })));
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Name=item.Product.Name,
                })));
            CreateMap<SubmitOrderDto, Order>();
            CreateMap<Product, ProductDto>();
        }
    }
}
