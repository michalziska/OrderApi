using SystemOrder.Attributes;

namespace SystemOrder.Domain.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public ECategory Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
