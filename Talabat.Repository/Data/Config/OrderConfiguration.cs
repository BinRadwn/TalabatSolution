using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, NP => NP.WithOwner());

            builder.Property(o => o.Status)
                .HasConversion(
                 status => status.ToString(),
                 status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status)
                );

            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(item => item.SubTotal)
                .HasColumnType("decimal(18,2)");


        }
    }
}
