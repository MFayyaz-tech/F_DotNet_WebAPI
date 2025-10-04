using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace WebRAPI.Controllers.Payment

{
   

    [ApiController]
    [Route("api/payment")]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static ConcurrentDictionary<string, string> _paymentMethods = new ConcurrentDictionary<string, string>();

        public PaymentsController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }




        [HttpPost("save-payment-method")]
        public async Task<IActionResult> SavePaymentMethod([FromBody] SavePaymentMethodRequest request)
        {
            try
            {
                // Store the PaymentMethodId in memory
                _paymentMethods[request.UserId] = request.PaymentMethodId;

                return Ok(new { message = "Payment method saved successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get-payment-method")]
        public IActionResult GetPaymentMethod([FromQuery] string userId)
        {
            return _paymentMethods.TryGetValue(userId, out var paymentMethodId)
                ? Ok(new { paymentMethodId })
                : NotFound(new { error = "Payment method not found" });
        }


        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount, // Amount in cents
                    Currency = "usd",
                    PaymentMethod = request.PaymentMethodId,
                    Confirm = true,
                    ReturnUrl = "",
                };

                var service = new PaymentIntentService();
                PaymentIntent paymentIntent = await service.CreateAsync(options);

                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }

    public class SavePaymentMethodRequest
    {
        public string UserId { get; set; }
        public string PaymentMethodId { get; set; }
    }
    public class CreatePaymentIntentRequest
    {
        public long Amount { get; set; } // Amount in cents
        public string PaymentMethodId { get; set; }
    }


}