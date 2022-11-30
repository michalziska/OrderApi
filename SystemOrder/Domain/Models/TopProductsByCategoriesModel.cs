namespace SystemOrder.Domain.Models
{
	public class TopProductsByCategoriesModel
	{
		public ECategory Category { get; set; }
		public int Products { get; set; }
		public IEnumerable<Product> TopProducts { get; set; }
	}
}
