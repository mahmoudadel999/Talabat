using AutoMapper;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Models.Common;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entities.Basket;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Core.Domain.Entities.Product;
using userAddress = Talabat.Core.Domain.Entities.Identity.Address;
using orderAddress = Talabat.Core.Domain.Entities.Orders.Address;

namespace Talabat.Core.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());
            CreateMap<ProductBrand, ProductToReturnDto>();
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
               .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod!.ShortName))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<orderAddress, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliverMethodDto>();

            CreateMap<userAddress, AddressDto>().ReverseMap();
        }
    }
}
