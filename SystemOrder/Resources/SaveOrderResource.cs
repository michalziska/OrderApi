using System.ComponentModel.DataAnnotations;

namespace SystemOrder.Resources
{
	public class SaveOrderResource
	{
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfCreation { get; set; }

		[Required]
		public IEnumerable<SaveProductResource> Products { get; set; } = new List<SaveProductResource>();


	}
}
