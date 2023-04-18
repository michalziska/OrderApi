using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using SystemOrder.Domain.Models;

namespace SystemOrder.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ProductId).HasColumnName("ProductId");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired();

            builder.HasMany(x => x.Orders)
                .WithMany(x => x.Products);

            builder.HasData(
                new[]
                {
                  new Product
                  {
                    ProductId = 100,
                    Name = "New1",
                    Price = 20M,
                    Category = ECategory.Furniture,
                    Description = "Test1"
                  },
                  new Product
                  {
                    ProductId = 101,
                    Name = "New2",
                    Price = 20M,
                    Category = ECategory.Furniture,
                    Description = "Test2"
                  },
                  new Product
                  {
                    ProductId = 102,
                    Name = "New3",
                    Price = 20M,
                    Category = ECategory.Furniture,
                    Description = "Test3"
                  },
                  new Product
                  {
                    ProductId = 103,
                    Name = "New4",
                    Price = 20M,
                    Category = ECategory.Furniture,
                    Description = "Test4"
                  },
                });
        }
    }
}
