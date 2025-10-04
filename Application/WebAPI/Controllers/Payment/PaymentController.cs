using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO;
using BU.Services.IServices.AuthNetPaymentService;
using Common.Helper;
using FH.Services.IServices.User;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using WebRAPI.Base;
using FH.Services.Services.User;
using BU.DTO.DTOs.Payments;

namespace WebAPI.Controllers.Payment
{
	[ApiController]
	[Route("api/paymentauth")]
	public class PaymentController : BaseController
	{
		private readonly IConfiguration _configuration;
		private readonly IUserService _UserServics;
		private readonly IAuthNetPaymentService _paymentService;
		private readonly ILogging _logging;

		public PaymentController(IConfiguration configuration, IUserService userService,ILogging logging, IAuthNetPaymentService paymentService) : base(logging, configuration)
		{
			_configuration = configuration;
            _UserServics = userService;
			logging = _logging;
			_paymentService = paymentService;
		}

        [HttpPost("makepayment")]
        public IActionResult CreatePaymentIntent([FromBody] AuthPaymentRequestDTO request)
        {
            Result result = new Result(true);
            try
            {
                // Process the payment
                CreateTransactionResponse data = _paymentService.ProcessPayment(request);
                if (data.IsSuccess)
                {
                    // Update the user's isActive status to true
                    bool updateSuccess = _UserServics.ActivateUser(request);

                    if (updateSuccess)
                    {
                        result.Data = data;
                        result.Success = true;
                        result.Message = "Payment successfully done, user activated.";
                    }
                    else
                    {
                        result.Data = data;
                        result.Success = true;
                        result.Message = "Payment successfully done, but failed to activate user.";
                    }
                }
                else
                {
                    result.Data = data;
                    result.Message = "Payment Failed";
                    result.Success = false;
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }

            if (result.Success)
            {
                return Json(result);
            }
            else
            {
                return Unauthorized(new { message = result.Message, data = result.Data });
            }
        }



        [HttpPost("refund_payment")]
        public IActionResult RefundPayment([FromBody] RefundPaymentDTO request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.TransactionId) || request.RefundAmount <= 0)
                {
                    return BadRequest("Invalid refund request.");
                }

                var result = _paymentService.RefundPayment(request);

                if (!result.IsSuccess)
                {
                    return StatusCode(500, new { Message = "Refund failed.", Data = result });
                }

                return Ok(new { Message = "Refund successful.", Data = result });
            }
            catch (Exception ex)
            {
                _logging.Error($"Method: PaymentController.RefundPayment Message: An error occurred while processing the refund. Exception: {ex.Message}");
                return StatusCode(500, "An internal error occurred.");
            }
        }



        [HttpPost("save_payment")]
        public IActionResult SavePayment(PaymentDTO obj)
        {
            try
            {
                if (obj == null)
                {
                    return BadRequest("Payment data is required.");
                }

                // Assuming a service method that saves the payment
                var result = _paymentService.SavePayment(obj);

                if (result == null)
                {
                    return StatusCode(500, "An error occurred while saving the payment.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred." + ex);
            }
        }

    }
}
