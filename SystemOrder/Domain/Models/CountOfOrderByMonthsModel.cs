namespace SystemOrder.Domain.Models
{
	public class CountOfOrderByMonthsModel
	{
		public int Month { get; set; }
		public int OrdersCount { get; set; }
		public IEnumerable<Order> Orders { get; set; }
	}
}
