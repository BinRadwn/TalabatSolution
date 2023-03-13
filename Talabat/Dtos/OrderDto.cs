using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int deliveryMethodId { get; set; }
        public AddressDto shippingAddress { get; set; }
    }
}
