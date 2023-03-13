using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.specification
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        //Constructor
        public OrderByPaymentIntentIdSpecification(string PIntentId)
            : base(O => O.PaymentIntentId == PIntentId)
        {

        }
    }
}
