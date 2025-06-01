using System.Security.Claims;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;

namespace WeiaCraftsApplication.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<PaymentResultDto> ProcessPaymentAsync(CheckoutDto checkoutDto)
        {
            // TODO: Connect to payment gateway
            return new PaymentResultDto { Success = true, Message = "Payment processed" };
        }

        public async Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistoryAsync(ClaimsPrincipal user)
        {
            // TODO: Fetch payment history from database
            return new List<PaymentHistoryDto>();
        }
    }
}
