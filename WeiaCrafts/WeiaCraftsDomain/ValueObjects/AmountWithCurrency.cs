namespace WeiaCraftsDomain.ValueObjects;

public class AmountWithCurrency
{
    public decimal Amount { get;  set; }
    public string Currency { get;  set; }

    public AmountWithCurrency() { }

    public AmountWithCurrency(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.");
        if (currency.Length != 3) throw new ArgumentException("Currency must be a 3-letter code.");

        Amount = amount;
        Currency = currency.ToUpper();
    }

    public override string ToString() => $"{Amount} {Currency}";

  
    public static AmountWithCurrency Zero(string currency) => new AmountWithCurrency(0, currency);
}
