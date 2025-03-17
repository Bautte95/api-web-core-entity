using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Model
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [MaxLength(50)]
        public string NameProduct { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public List<OrderProduct> OrderProduct { get; set; } = [];
    }
}
