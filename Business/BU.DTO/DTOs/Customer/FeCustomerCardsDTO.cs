using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Customer
{
    public class FeCustomerCardsDTO : BaseDTO
    {
        public long CustomerCardId { get; set; }
        public long CustomerId { get; set; }
        public string CardId { get; set; }
        public string Brand { get; set; }
        public string ExpireDate { get; set; }
        public string CvvNumber { get; set; }
        public string Country { get; set; }
        public string CreditCardNumber { get; set; } 
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
