using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.specification
{
    public class OrderWithItemsAndDeliveryMethodSpecification:BaseSpecification<Order>
    {

        //the Constructor used for get all the order
        public OrderWithItemsAndDeliveryMethodSpecification(string buyerEmail)
            :base(O=>O.BuyerEmail==buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O=>O.Items);
            AddOrderByDescending(O=>O.OrderDate);
        }


        //This Constructor is used for get a specific orders by id for a specific user
        public OrderWithItemsAndDeliveryMethodSpecification(int id ,string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail &&O.Id==id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
