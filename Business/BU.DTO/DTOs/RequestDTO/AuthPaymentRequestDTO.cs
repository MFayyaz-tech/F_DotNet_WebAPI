using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BU.DTO.DTOs.RequestDTO
{
	public class AuthPaymentRequestDTO
	{
		public string CardNumber { get; set; }
		public long UserId { get; set; }
		public string ExpireDate { get; set; }
		public string Cvv { get; set; }
		public decimal TotalAmount { get; set; }
		public string? AccountType { get; set; }
		public string? TransacationId { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

	}
    public class CreateTransactionResponse
    {
        public string TransactionStatus { get; set; }
        public string TransactionId { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public List<string> SuccessMessages { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }
    }
}
