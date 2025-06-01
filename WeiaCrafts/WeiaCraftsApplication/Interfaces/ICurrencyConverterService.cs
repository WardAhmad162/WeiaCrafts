using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.Interfaces;

public interface ICurrencyConverterService
{
    Task<AmountWithCurrency> ConvertAsync(AmountWithCurrency source, string targetCurrency);
}
