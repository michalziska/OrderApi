using SystemOrder.Domain.Models;
using SystemOrder.Domain.Repositories;
using SystemOrder.Domain.Services;
using SystemOrder.Domain.Services.Communication;

namespace SystemOrder.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IProductRepository _productRepository;
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
		{
			_orderRepository = orderRepository;
			_productRepository = productRepository;
			_unitOfWork = unitOfWork;
		}


		public async Task<OrderResponse> DeleteOrderAsync(int id)
		{
			try
			{
				var orderFound = await _orderRepository.FindByIdAsync(id);

				if (orderFound == null)
					return new OrderResponse("Order not found");

				_orderRepository.Delete(orderFound);
				await _unitOfWork.CompleteAsync();

				return new OrderResponse(orderFound);
			}
			catch (Exception ex)
			{
				return new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<ProductResponse> DeleteProductAsync(int id)
		{
			try
			{
				var productFound = await _productRepository.FindByIdAsync(id);

				if (productFound == null)
					return new ProductResponse("Product not found.");

				_productRepository.Delete(productFound);
				await _unitOfWork.CompleteAsync();

				return new ProductResponse(productFound);
			}
			catch (Exception ex)
			{
				return new ProductResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<OrderResponse> FindOrderAsync(int id)
		{
			try
			{
				var order = await _orderRepository.FindByIdAsync(id);

				return new OrderResponse(order);
			}
			catch (Exception ex)
			{

				return new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<ProductResponse> FindProductAsync(int id)
		{
			try
			{
				var product = await _productRepository.FindByIdAsync(id);

				return new ProductResponse(product);

			}
			catch (Exception ex)
			{

				return new ProductResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<IEnumerable<Order>> ListOrdersAsync()
		{
			try
			{
				var query = await _orderRepository.ListAsync();

				return query;
			}
			catch (Exception ex)
			{
				return new List<Order>() { }; //new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<IEnumerable<Product>> ListProductsAsync()
		{
			try
			{
				var query = await _productRepository.ListAsync();

				return query;
			}
			catch (Exception ex)
			{
				return new List<Product>() { }; //new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<IEnumerable<TopProductsByCategoriesModel>> TheMostSellProductsByCategories()
		{
			try
			{
				var query = await _productRepository.TheMostSellProductsByCategories();

				return query;
			}
			catch (Exception ex)
			{
				return new List<TopProductsByCategoriesModel>() { }; //new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<OrderResponse> SaveOrderAsync(Order order, CancellationToken cancellationToken)
		{
			try
			{
				await _orderRepository.AddAsync(order, cancellationToken);
				await _unitOfWork.CompleteAsync();

				return new OrderResponse(order);
			}
			catch (Exception ex)
			{

				return new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<ProductResponse> SaveProductAsync(Product product, CancellationToken cancellationToken)
		{
			try
			{
				await _productRepository.AddAsync(product, cancellationToken);
				await _unitOfWork.CompleteAsync();

				return new ProductResponse(product);
			}
			catch (Exception ex)
			{

				return new ProductResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<OrderResponse> UpdateOrderAsync(int id, Order order)
		{
			try
			{
				var orderFound = await _orderRepository.FindByIdAsync(id);

				if (orderFound == null)
					return new OrderResponse("Order not found");

				orderFound.DateOfCreation = order.DateOfCreation;
				orderFound.Products = order.Products;

				_orderRepository.Update(orderFound);
				await _unitOfWork.CompleteAsync();

				return new OrderResponse(orderFound);
			}
			catch (Exception ex)
			{
				return new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<ProductResponse> UpdateProductAsync(int id, Product product)
		{
			try
			{
				var productFound = await _productRepository.FindByIdAsync(id);

				if (productFound == null)
					return new ProductResponse("Product not found.");

				productFound.Name = product.Name;
				productFound.Category = product.Category;
				productFound.Price = product.Price;

				_productRepository.Update(productFound);
				await _unitOfWork.CompleteAsync();

				return new ProductResponse(productFound);
			}
			catch (Exception ex)
			{

				return new ProductResponse($"An error occurred when ...: {ex.Message}");
			}
		}

		public async Task<IEnumerable<CountOfOrderByMonthsModel>> CountOfOrderByMonths()
		{
			try
			{
				var query = await _orderRepository.CountOfOrderByMonths();

				return query;
			}
			catch (Exception ex)
			{
				return new List<CountOfOrderByMonthsModel>() { }; //new OrderResponse($"An error occurred when ...: {ex.Message}");
			}
		}
	}
}
