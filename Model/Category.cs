using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Description { get; set; } = null!;

        public List<Product>? Product { get; set; }
    }
}
