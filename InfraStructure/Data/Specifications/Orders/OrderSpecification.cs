

using Core.Entities.Orders;
using Core.Helpers;

namespace InfraStructure.Data.Specifications.Orders
{
    public class OrderSpecification: BaseSpecification<Order>
    {
        public OrderSpecification(OrderSpecParams orderSpecParams, string userId): base(x => x.UserId == userId)
        {
            AddInclude(s => s.OrderItems);
            AddInclude("OrderItems.Product");

            ApplyPaging(
                (orderSpecParams.PageNumber - 1) * orderSpecParams.PageSize,
                orderSpecParams.PageSize
            );

            if (!string.IsNullOrEmpty(orderSpecParams.SortPrice))
            {
                switch (orderSpecParams.SortPrice)
                {
                    case "DESC":
                        AddOrderByDescending(s => s.TotalPrice);
                        break;
                    default:
                        AddOrderBy(s => s.TotalPrice);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(orderSpecParams.SortLatest))
            {
                switch (orderSpecParams.SortLatest)
                {
                    case "DESC":
                        AddOrderByDescending(s => s.CreateDate);
                        break;
                    default:
                        AddOrderBy(s => s.CreateDate);
                        break;
                }
            }
        }
    }
}
