using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using SystemOrder.Domain.Models;

namespace SystemOrder.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderId).HasColumnName("OrderId");

            builder.HasMany(x => x.Products)
            .WithMany(x => x.Orders);

            builder.HasData(
                            new Order
                            {
                                OrderId = 100,
                                DateOfCreation = DateTime.Now,
                            });
        }
    }
}
