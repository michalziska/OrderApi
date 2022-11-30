using SystemOrder.Domain.Models;

namespace SystemOrder.Persistence.Context
{
	public static class SeedData
	{
		public static async Task Seed(OrderDbContext context)
		{
			var orders = new List<Order>()
			{
				new Order
				{
					OrderId = 100,
					DateOfCreation = DateTime.Now,
				}
			};

			var products = new List<Product>()
			{
				new Product
				{
					ProductId = 1,
					Name = "Chair",
					Price = 20M,
					Category = ECategory.Furniture,
				}
			};

			context.Order.AddRange(orders);
			context.Products.AddRange(products);

			await context.SaveChangesAsync();
		}
	}
}
