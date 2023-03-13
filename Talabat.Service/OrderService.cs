using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.specification;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenaricRepository<Product> _productRepo;
        //private readonly IGenaricRepository<DeliveryMethod> _deliveryMethodRepo;
        //private readonly IGenaricRepository<Order> _ordersRepo;

        //Constructor
        public OrderService(
             IBasketRepository basketRepository,
             //IGenaricRepository<Product> productRepo,
             //IGenaricRepository<DeliveryMethod> deliveryMethodRepo,
             //IGenaricRepository<Order> ordersRepo
             IUnitOfWork unitOfWork,
             IPaymentService paymentService
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepo = productRepo;
            //_deliveryMethodRepo = deliveryMethodRepo;
            //_ordersRepo = ordersRepo;
        }

        //
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var productItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            //3.Calculate SubTotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);


            //4. Get Delivery Method From deliveryMethod Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //5. Create Order
            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var ExistingOrder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (ExistingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(ExistingOrder);
                await _paymentService.CreateOrUpdatePaymentIntentAsync(basket.Id);
            }

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal, basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().CreateAsync(order);


            //6. Save To Database [TODO]
            var Result = await _unitOfWork.Compelet();
            if (Result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var Spec = new OrderWithItemsAndDeliveryMethodSpecification(orderId, buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(Spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var Spec = new OrderWithItemsAndDeliveryMethodSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);

            return orders;
        }
    }
}
