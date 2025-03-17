using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Email { get; set; } = null!;

        public int Cellular { get; set; }

        public List<Order>? Order { get; set; }
    }
}
