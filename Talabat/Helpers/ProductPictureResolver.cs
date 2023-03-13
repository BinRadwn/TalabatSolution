using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Talabat.Core.Entities;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class ProductPictureResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;
         
        public ProductPictureResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["BaseURL"]}{source.PictureUrl}";
            return null;
        }
    }
}
