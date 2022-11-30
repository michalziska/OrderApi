using SystemOrder.Domain.Models;

namespace SystemOrder.Domain.Services.Communication
{
	public class ProductResponse : BaseResponse<Product>
	{
		/// <summary>
		/// Creates a success response.
		/// </summary>
		public ProductResponse(Product product) : base(product) { }

		/// <summary>
		/// Creates am error response.
		/// </summary>
		public ProductResponse(string message) : base(message) { }
	}
}
