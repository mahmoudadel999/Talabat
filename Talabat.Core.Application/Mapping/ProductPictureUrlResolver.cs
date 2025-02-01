using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Core.Application.Mapping
{
    internal class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string>
    {
        public string Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
