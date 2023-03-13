using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.IO;
using System;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Services;
using Talabat.Error;
using Talabat.Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Talabat.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string _whSecret = "whsec_9e98ef9da06c4d1852bc2a64b377a315d1d2fc12e013c46f74e3b9c25675890a";

        //Constructor
        public PaymentsController(IPaymentService paymentService , ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }


        //End Points

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "A Problem With Your Basket"));
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _whSecret);


            PaymentIntent intent;
            Order order;

            // Handle the event
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await _paymentService.UpdatePaymentIntentSucceededOrFailed(intent.Id, true);
                    _logger.LogInformation("Payment Succeeded" , order.Id ,intent.Id);
                    break;
                case Events.PaymentIntentPaymentFailed:
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await _paymentService.UpdatePaymentIntentSucceededOrFailed(intent.Id, false);
                    _logger.LogInformation("Payment Failed", order.Id, intent.Id);
                    break;

            }


            return new EmptyResult();


        }
    }
}
