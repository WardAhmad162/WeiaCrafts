using System.Security.Claims;
using WeiaCraftsApplication.DTOs;

namespace WeiaCraftsApplication.Interfaces;

public interface IPaymentService
{
    public interface IPaymentService
    {
        Task<PaymentResultDto> ProcessPaymentAsync(CheckoutDto checkoutDto);
        Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistoryAsync(ClaimsPrincipal user);
    }
}
