using System.ComponentModel.DataAnnotations;

namespace SystemOrder.Resources
{
	public class SaveProductResource
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[Range(1, 5)]
		public int Category { get; set; }

		[Required]
		public decimal Price { get; set; }

		//[Required]
		//public int ProductId { get; set; }
	}
}
