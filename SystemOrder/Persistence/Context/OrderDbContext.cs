using Microsoft.EntityFrameworkCore;
using SystemOrder.Domain.Models;

namespace SystemOrder.Persistence.Context
{
	public class OrderDbContext : DbContext
	{
		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
		{
			Database.Migrate();
		}

		public DbSet<Order> Order => Set<Order>();
		public DbSet<Product> Products => Set<Product>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Product>()
				.HasMany(p => p.Orders)
				.WithMany(p => p.Products);

			modelBuilder.Entity<Order>().HasData(
							new Order
							{
								OrderId = 100,
								DateOfCreation = DateTime.Now,
							});

			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					Name = "Chair",
					Price = 20M,
					Category = ECategory.Furniture,
				});
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
		}
	}
}
