using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public List<OrderProduct> OrderProduct { get; set; } = [];
    }
}
