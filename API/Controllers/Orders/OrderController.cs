using Core.Dtos;
using Core.Helpers;
using Core.Interfaces;
using InfraStructure.Data.Specifications.Orders;
using InfraStructure.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.Orders
{
    [Authorize]
    public class OrderController: BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrderController(IUnitOfWork uow, IHttpContextAccessor contextAccessor)
        {
            _uow = uow;
            _contextAccessor = contextAccessor;
        }

        //[HttpPost]
        //public async Task<ActionResult<OrderOutDto>> PlaceOrder()
        //{
        //    var orderoutdto = await _uow.OrderRepository.PlaceOrder();
        //    if (await _uow.Complete()) return Ok(orderoutdto);
        //    return Ok(new ApiException(500));
        //}

        [HttpGet]
        public async Task<ActionResult<Pagination<OrderOutDto>>> GetOrders([FromQuery] OrderSpecParams orderSpecParams)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _uow.OrderRepository.GetListAsync(
                new OrderSpecification(orderSpecParams, userId),
                new OrderCountSpecification(orderSpecParams, userId),
                new PaginationParams { PageNumber = orderSpecParams.PageNumber, PageSize = orderSpecParams.PageSize }
            ));
        }
    }
}
