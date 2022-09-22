using Core.Entities.Orders;
using Core.Helpers;

namespace InfraStructure.Data.Specifications.Orders
{
    public class OrderCountSpecification: BaseSpecification<Order>
    {
        public OrderCountSpecification(OrderSpecParams orderSpecParams, string userId) : base(x => x.UserId == userId)
        {

        }
    }
}
