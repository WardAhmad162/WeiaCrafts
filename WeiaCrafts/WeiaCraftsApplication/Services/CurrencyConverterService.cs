using System.Net.Http.Json;
using WeiaCraftsApplication.DTOs;

using WeiaCraftsApplication.Interfaces;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.Services;

public class CurrencyConverterService : ICurrencyConverterService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "1dd32788a3b013df53832901"; 
    private const string BaseUrl = "https://v6.exchangerate-api.com/v6/";

    public CurrencyConverterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AmountWithCurrency> ConvertAsync(AmountWithCurrency source, string targetCurrency)
    {
        if (source.Currency.ToUpper() == targetCurrency.ToUpper())
            return source;

        var url = $"{BaseUrl}{ApiKey}/latest/{source.Currency}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to fetch exchange rates");

        var result = await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();
        if (result == null || !result.Conversion_rates.ContainsKey(targetCurrency.ToUpper()))
            throw new Exception("Target currency not supported");

        var rate = result.Conversion_rates[targetCurrency.ToUpper()];
        return new AmountWithCurrency(source.Amount * rate, targetCurrency);
    }
}
