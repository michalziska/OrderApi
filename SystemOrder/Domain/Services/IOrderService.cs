using SystemOrder.Domain.Models;
using SystemOrder.Domain.Services.Communication;

namespace SystemOrder.Domain.Services
{
	public interface IOrderService
	{
		Task<ProductResponse> FindProductAsync(int id);
		Task<IEnumerable<Product>> ListProductsAsync();

		Task<ProductResponse> SaveProductAsync(Product product, CancellationToken cancellationToken);
		Task<ProductResponse> UpdateProductAsync(int id, Product product);
		Task<ProductResponse> DeleteProductAsync(int id);

		Task<IEnumerable<TopProductsByCategoriesModel>> TheMostSellProductsByCategories();

		Task<OrderResponse> FindOrderAsync(int id);
		Task<IEnumerable<Order>> ListOrdersAsync();

		Task<OrderResponse> SaveOrderAsync(Order order, CancellationToken cancellationToken);
		Task<OrderResponse> UpdateOrderAsync(int id, Order order);
		Task<OrderResponse> DeleteOrderAsync(int id);

		Task<IEnumerable<CountOfOrderByMonthsModel>> CountOfOrderByMonths();

	}
}
