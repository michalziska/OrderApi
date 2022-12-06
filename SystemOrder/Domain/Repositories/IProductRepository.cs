using SystemOrder.Domain.Models;

namespace SystemOrder.Domain.Repositories
{
	public interface IProductRepository
	{
		Task<IQueryable<Product>> ListAsync();
		Task AddAsync(Product product, CancellationToken cancellationToken);
		Task<Product> FindByIdAsync(int id);
		void Update(Product product);
		void Delete(Product product);

		Task<IEnumerable<TopProductsByCategoriesModel>> TheMostSellProductsByCategories();
	}
}
