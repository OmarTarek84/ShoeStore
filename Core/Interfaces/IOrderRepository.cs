
using Core.Dtos;
using Core.Entities.Orders;
using Core.Helpers;

namespace Core.Interfaces
{
    public interface IOrderRepository: IBaseRepository<Order, OrderOutDto>
    {
        Task<OrderOutDto> PlaceOrder(string userId);
    }
}
