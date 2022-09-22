
using Core.Entities.Identity;
using Core.Entities.Products;

namespace Core.Entities.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int Quantity { get; set; } = 1;

        public decimal GetSubtotal()
        {
            return Product.PriceAfterDiscount * Quantity;
        }
    }
}
