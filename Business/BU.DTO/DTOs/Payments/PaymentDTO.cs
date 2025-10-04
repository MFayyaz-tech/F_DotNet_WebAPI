using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.Payments
{
	public class PaymentDTO : BaseDTO
	{
        public long PaymentId { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public long? CardId { get; set; }
        public long? JobId { get; set; }
        public long? BidId { get; set; }
        public long? TrainingId { get; set; }
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
  
    }

    public class RefundPaymentDTO
    {
        public string TransactionId { get; set; }
        public decimal RefundAmount { get; set; }
    }

}

