
using AutoMapper;
using Core.Dtos;
using Core.Entities.Cart;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data.Repositories.Carts
{
    public class CartRepository: ICartRepository
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public CartRepository(StoreContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<CartItemOutDto>> AddCartItems(List<CartItemInDto> cartitems)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cartItemsToBeAdded = new List<CartItem>();
            var cartFound = new CartItem();

            foreach (var item in cartitems)
            {
                cartFound = await _context.CartItems.Include(s => s.Product).FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == item.ProductId);
                if (cartFound is not null)
                {
                    cartFound.Quantity += item.Quantity;
                    _context.CartItems.Update(cartFound);
                }
                else
                {
                    var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == item.ProductId);
                    if (item.Quantity > product.CountInStock) throw new Exception($"Only {product.CountInStock} of this product occurs in stock");
                    cartItemsToBeAdded.Add(new CartItem
                    {
                        ProductId = item.ProductId,
                        Product = product,
                        UserId = userId,
                        Quantity = item.Quantity
                    });
                }
            }

            if (cartItemsToBeAdded.Count > 0)
                await _context.CartItems.AddRangeAsync(cartItemsToBeAdded);

            var cartsoutDto = new List<CartItemOutDto>();
            if (cartItemsToBeAdded.Count > 0)
            {
                foreach (var item in cartItemsToBeAdded)
                {
                    cartsoutDto.Add(_mapper.Map<CartItemOutDto>(item));
                }
            }
            else cartsoutDto.Add(_mapper.Map<CartItemOutDto>(cartFound));
            return cartsoutDto;
        }

        public async Task<List<CartItemOutDto>> GetCartProducts(List<CartItemInDto>? inDto = null)
        {
            var user = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var cartProductsOutDto = new List<CartItemOutDto>();
            if (user == null)
            {
                if (inDto == null || inDto.Count <= 0) throw new Exception("No Cart Items Specified");
                foreach (var cartItemInDto in inDto)
                {
                    var prod = await _context.Products.FindAsync(cartItemInDto.ProductId);
                    cartProductsOutDto.Add(new CartItemOutDto
                    {
                        productId = cartItemInDto.ProductId,
                        ProductImage = prod.Image,
                        ProductName = prod.Name,
                        Quantity = cartItemInDto.Quantity,
                        Subtotal = cartItemInDto.Quantity * prod.PriceAfterDiscount,
                        ProductPrice = prod.PriceAfterDiscount,
                    });
                }
                return cartProductsOutDto;
            }
            var userId = user.Value;
            var cartItems = await _context.CartItems.Include(s => s.Product).Where(s => s.UserId == userId).AsNoTracking().ToListAsync();

            var cartsOutDto = new List<CartItemOutDto>();
            foreach (var item in cartItems)
            {
                cartsOutDto.Add(_mapper.Map<CartItemOutDto>(item));
            }
            return cartsOutDto;
        }

        public async Task<bool> RemoveCart(int productId)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartitem = await _context.CartItems.FindAsync(productId, userId);
            if (cartitem == null) throw new Exception("Cart Not Found");

            _context.CartItems.Remove(cartitem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CartItemOutDto> UpdateQuantity(CartItemInDto cartItem)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = await _context.CartItems.Include(s => s.Product).FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == cartItem.ProductId);
            if (item == null) throw new Exception("Cart Not Found");

            item.Quantity = cartItem.Quantity;
            _context.CartItems.Update(item);
            if (await _context.SaveChangesAsync() > 0)
                return _mapper.Map<CartItemOutDto>(item);
            throw new Exception("Unexpected Error occurred");
        }
    }
}
