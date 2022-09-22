
namespace Core.Dtos
{
    public class OrderOutDto: OutDto
    {
        public DateTime OrderDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemOutDto> OrderItems { get; set; } = new List<OrderItemOutDto>();
    }
}
