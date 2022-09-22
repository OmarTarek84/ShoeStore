
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Orders
{
    public class Order: BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
    }
}
