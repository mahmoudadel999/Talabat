using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Domain.Entities.Orders;

namespace Talabat.Core.Application.Mapping
{
    internal class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.Product.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
