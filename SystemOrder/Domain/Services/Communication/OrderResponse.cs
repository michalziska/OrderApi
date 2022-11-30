using SystemOrder.Domain.Models;

namespace SystemOrder.Domain.Services.Communication
{
	public class OrderResponse : BaseResponse<Order>
	{
		/// <summary>
		/// Creates a success response.
		/// </summary>
		public OrderResponse(Order order) : base(order) { }

		/// <summary>
		/// Creates am error response.
		/// </summary>
		public OrderResponse(string message) : base(message) { }
	}
}
