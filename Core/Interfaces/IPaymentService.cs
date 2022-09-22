
using Microsoft.AspNetCore.Http;
using Stripe.Checkout;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckoutSession();
        Task<bool> FullfillOrder(HttpRequest request);
    }
}
