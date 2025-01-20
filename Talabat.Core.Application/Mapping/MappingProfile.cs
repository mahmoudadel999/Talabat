using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entities.Basket;
using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Core.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand!.Name))
                .ForMember(D => D.Category, O => O.MapFrom(S => S.Category!.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            CreateMap<ProductBrand, ProductToReturnDto>();
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
