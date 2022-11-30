using Microsoft.EntityFrameworkCore;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Repositories;
using SystemOrder.Persistence.Context;

namespace SystemOrder.Persistence.Repositories
{
	public class ProductRepository : BaseRepository, IProductRepository
	{
		public ProductRepository(OrderDbContext context) : base(context) { }

		public async Task AddAsync(Product product)
		{
			await _context.Products.AddAsync(product);
		}

		public void Delete(Product product)
		{
			_context.Products.Remove(product);
		}

		public async Task<Product> FindByIdAsync(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task<IQueryable<Product>> ListAsync()
		{
			return _context.Products.Include(p => p.Orders).AsNoTracking();
		}

		public void Update(Product product)
		{
			_context.Products.Update(product);
		}

		public async Task<IEnumerable<TopProductsByCategoriesModel>> TheMostSellProductsByCategories()
		{
			var query = _context.Products
				.Include(p => p.Orders).ToList()
				.GroupBy(p => p.Category, (category, products) => new TopProductsByCategoriesModel
				{
					Category = category,
					Products = products.Select(prod => prod.Name).Count(),
					TopProducts = products.Select(prod => prod)
				})
				.OrderByDescending(prod => prod.Products);

			return query;
		}
	}
}
