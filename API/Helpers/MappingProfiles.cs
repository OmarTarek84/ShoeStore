using AutoMapper;
using Core.Dtos;
using Core.Entities.Cart;
using Core.Entities.Identity;
using Core.Entities.Orders;
using Core.Entities.Products;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Brand, BrandOutDto>();
            CreateMap<Address, AddressOutDto>();
            CreateMap<Product, ProductOutDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));
            CreateMap<Review, ReviewOutDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<OrderItem, OrderItemOutDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.PriceAfterDiscount));
            CreateMap<Order, OrderOutDto>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.CreateDate));
            CreateMap<CartItem, CartItemOutDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.PriceAfterDiscount))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
                .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.Product.Id))
                .ReverseMap();
        }
    }
}
