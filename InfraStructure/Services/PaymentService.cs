
using Core.Entities.Identity;
using Core.Interfaces;
using InfraStructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace InfraStructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        string secret = "";

        public PaymentService(IConfiguration config, UserManager<AppUser> userManager, StoreContext context, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            StripeConfiguration.ApiKey = config.GetSection("Stripe:SecretKey").Value;
            _config = config;
            _userManager = userManager;
            _context = context;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            secret = config.GetSection("WebhookSecret").Value;
        }
        public async Task<Session> CreateCheckoutSession()
        {
            var lineItems = new List<SessionLineItemOptions>();

            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _context.CartItems.Include(s => s.Product).Where(s => s.UserId == userId).AsNoTracking().ToListAsync();

            foreach (var cartitem in cartItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = cartitem.Product.PriceAfterDiscount * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartitem.Product.Name,
                            Images = new List<string> { cartitem.Product.Image }
                        }
                    },
                    Quantity = cartitem.Quantity,
                });
            }
            var options = new SessionCreateOptions
            {
                CustomerEmail = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://shoe-store-demo.azurewebsites.net/orders/success",
                CancelUrl = "https://shoe-store-demo.azurewebsites.net/cart"
            };
            var service = new SessionService();
            var session = service.Create(options);
            return session;
        }

        public async Task<bool> FullfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        request.Headers["Stripe-Signature"],
                        secret,
                        300,
                        false
                    );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _userManager.FindByEmailAsync(session.CustomerEmail);
                    await _unitOfWork.OrderRepository.PlaceOrder(user.Id);
                }

                return true;
            }
            catch (StripeException e)
            {
                return false;
            }
        }
    }
}
