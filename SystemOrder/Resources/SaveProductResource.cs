using System.ComponentModel.DataAnnotations;
using SystemOrder.Attributes;

namespace SystemOrder.Resources
{
    public class SaveProductResource
    {
        [Required]
        [Validate<NameValidator>]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Validate<DescValidator>]
        public string Description { get; set; }

        //[Required]
        //public int ProductId { get; set; }
    }
}
