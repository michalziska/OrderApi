namespace SystemOrder.Domain.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public DateTime DateOfCreation { get; set; }

		public int CountOfProducts => Products.Count;
		public decimal SumOfPriceProducts => Products.Sum(prod => prod.Price);

		public List<Product> Products { get; set; } = new List<Product>();
	}
}
