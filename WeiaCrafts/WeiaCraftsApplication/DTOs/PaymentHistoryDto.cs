using System;

namespace WeiaCraftsApplication.DTOs;

public class PaymentHistoryDto
{
    public string TransactionId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}

