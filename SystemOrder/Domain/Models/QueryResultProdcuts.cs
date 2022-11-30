namespace SystemOrder.Domain.Models
{
	public class QueryResultProdcuts<T>
	{
		public List<T> Products { get; set; } = new List<T>();
	}
}
