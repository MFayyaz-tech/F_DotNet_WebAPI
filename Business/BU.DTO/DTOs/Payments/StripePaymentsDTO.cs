using System;
namespace BU.DTO.DTOs.Payments
{
    public class CreatePaymentIntentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
    }
}

