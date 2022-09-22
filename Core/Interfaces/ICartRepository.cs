
using Core.Dtos;

namespace Core.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItemOutDto>> AddCartItems(List<CartItemInDto> cartitems);
        Task<List<CartItemOutDto>> GetCartProducts(List<CartItemInDto>? inDto = null);
        Task<CartItemOutDto> UpdateQuantity(CartItemInDto cartItem);
        Task<bool> RemoveCart(int productId);
    }
}
