using Core.Dtos;
using Core.Interfaces;
using InfraStructure.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Cart
{
    public class CartController: BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public CartController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<CartItemOutDto>>> AddCart(List<CartItemInDto> inDto)
        {
            var cartitems = await _uow.CartRepository.AddCartItems(inDto);
            if (await _uow.Complete()) return Ok(cartitems);
            return Ok(new ApiException(400));
        }

        [HttpPost("cartProducts")]
        public async Task<ActionResult<List<CartItemOutDto>>> GetCart(List<CartItemInDto>? inDto = null)
        {
            return Ok(await _uow.CartRepository.GetCartProducts(inDto));
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<CartItemOutDto>> UpdateQuantity(CartItemInDto inDto)
        {
            return Ok(await _uow.CartRepository.UpdateQuantity(inDto));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteCart([FromQuery] int productId)
        {
            return Ok(await _uow.CartRepository.RemoveCart(productId));
        }
    }
}
