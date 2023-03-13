using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.product.ProductUrl))
                return $"{configuration["BaseURL"]}{source.product.ProductUrl}";
            return null;
        }
    }
}
