
using Core.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Orders
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
