namespace WeiaCraftsApplication.DTOs;
public class ExchangeRateResponse
{
    public string Base_code { get; set; }
    public Dictionary<string, decimal> Conversion_rates { get; set; }
}
