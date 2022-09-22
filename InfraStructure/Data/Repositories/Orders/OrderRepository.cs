
using AutoMapper;
using Core.Dtos;
using Core.Entities.Orders;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace InfraStructure.Data.Repositories.Orders
{
    public class OrderRepository : BaseRepository<Order, OrderOutDto>, IOrderRepository
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _config;

        public OrderRepository(StoreContext context, IMapper mapper, IHttpContextAccessor contextAccessor, IConfiguration config) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _config = config;
        }

        public async Task<OrderOutDto> PlaceOrder(string userId)
        {
            var cartProducts = await _context.CartItems.Where(s => s.UserId == userId).Include(s => s.Product).ToListAsync();
            if (cartProducts.Count == 0)
                throw new Exception("No Products in your cart");

            var orderItems = new List<OrderItem>();

            var orderSubtotal = 0m;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var cartProduct in cartProducts)
                {
                    var subTot = cartProduct.Product.PriceAfterDiscount * cartProduct.Quantity;

                    var prod = cartProduct.Product;
                    prod.CountInStock--;
                    _context.Update(prod);

                    orderItems.Add(new OrderItem
                    {
                        ProductId = cartProduct.ProductId,
                        Product = cartProduct.Product,
                        Quantity = cartProduct.Quantity,
                        TotalPrice = subTot
                    });
                    orderSubtotal += subTot;
                }

                var neworder = new Order
                {
                    OrderItems = orderItems,
                    UserId = userId,
                    Subtotal = orderSubtotal,
                    TotalPrice = orderSubtotal,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                _context.CartItems.RemoveRange(cartProducts);
                Add(neworder);
                _context.SaveChanges();
                if (transaction != null)
                    transaction.Commit();

                return _mapper.Map<OrderOutDto>(neworder);
            } catch(Exception e)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw e;
            }
        }
    }
}
