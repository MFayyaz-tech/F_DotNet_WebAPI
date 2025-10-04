using AuthorizeNet.Api.Contracts.V1;
using BU.DTO.DTOs.Payments;
using BU.DTO.DTOs.RequestDTO;

namespace BU.Services.IServices.AuthNetPaymentService
{
	public interface IAuthNetPaymentService
	{
		public CreateTransactionResponse ProcessPayment(AuthPaymentRequestDTO data);
        PaymentDTO SavePayment(PaymentDTO obj);
        CreateTransactionResponse RefundPayment(RefundPaymentDTO obj);

    }
}
