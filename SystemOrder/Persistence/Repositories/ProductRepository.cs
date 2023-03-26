using Microsoft.EntityFrameworkCore;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Repositories;
using SystemOrder.Helpers;
using SystemOrder.Persistence.Context;
using SystemOrder.Resources;

namespace SystemOrder.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(OrderDbContext context) : base(context) { }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<PagedList<Product>> ListAsync(ProductResourceParameters productResourceParameters)
        {
            if (productResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(productResourceParameters));
            }

            var collection = _context.Products.Include(p => p.Orders).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(productResourceParameters.MainCategory))
            {
                var mainCategory = productResourceParameters.MainCategory.Trim();
                Enum.TryParse<ECategory>(mainCategory, true, out var eCategory);
                collection = collection.Where(x => x.Category == eCategory);
            }

            if (!string.IsNullOrWhiteSpace(productResourceParameters.SearchQuery))
            {
                var searchQuery = productResourceParameters.SearchQuery.Trim();
                collection = collection.Where(x => x.Name.Contains(searchQuery));
            }

            return await PagedList<Product>.CreateAsync(collection,
                productResourceParameters.PageNumber,
                productResourceParameters.PageSize);
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
