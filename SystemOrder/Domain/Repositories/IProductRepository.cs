using SystemOrder.Domain.Models;
using SystemOrder.Helpers;
using SystemOrder.Resources;

namespace SystemOrder.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> ListAsync(ProductResourceParameters productResourceParameters);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product> FindByIdAsync(int id);
        void Update(Product product);
        void Delete(Product product);

        Task<IEnumerable<TopProductsByCategoriesModel>> TheMostSellProductsByCategories();
    }
}
