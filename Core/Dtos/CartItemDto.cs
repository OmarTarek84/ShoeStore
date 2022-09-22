
namespace Core.Dtos
{
    public class CartItemOutDto
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public int productId { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class CartItemInDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
