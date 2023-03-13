using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.productBrand, O => O.MapFrom(S => S.productBrand.Name))
                .ForMember(D => D.productType, O => O.MapFrom(S => S.productType.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureResolver>());


            CreateMap<AddressDto, Core.Entities.Identity.Address>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(d => d.ProductId, o => o.MapFrom(s => s.product.ProductId))
               .ForMember(d => d.ProductName, o => o.MapFrom(s => s.product.ProductName))
               .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.product.ProductUrl))
               .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());



        }
    }
}
