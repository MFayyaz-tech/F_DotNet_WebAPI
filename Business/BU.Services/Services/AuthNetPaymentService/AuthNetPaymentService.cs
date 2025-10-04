using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers;
using BU.DTO.DTOs.RequestDTO;
using BU.Services.IServices.AuthNetPaymentService;
using Common.Helper;
using DAO;
using IN.Common.Model;
using Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IN.Common.Utilities;
using Common;
using BU.DTO.DTOs.Payments;
using BU.DTO.DTOs.Jobs;
using DA.Entities.Jobs;
using DA.Entities.Billing;
using AutoMapper;

namespace BU.Services.Services.AuthNetPaymentService
{
	public class AuthNetPaymentService : IAuthNetPaymentService
	{
		private readonly AuthorizeNetConfig _authorizeNetConfig;
		private readonly ILogging _logging;
        private readonly IRepository<Fe_payment> _FePaymentRepository;
        private readonly IMapper _mapper;




        public AuthNetPaymentService(IOptions<AuthorizeNetConfig> authorizeNetConfig,
            IRepository<Fe_payment> fePaymentRepository,
             IMapper mapper,

        ILogging logging)
		{
			_authorizeNetConfig = authorizeNetConfig.Value;
			_logging = logging;
            _FePaymentRepository = fePaymentRepository;
            _mapper = mapper;
		}

        //public CreateTransactionResponse ProcessPayment(AuthPaymentRequestDTO data)
        //{
        //    string environment = _authorizeNetConfig.Environment;
        //    var responseResult = new CreateTransactionResponse();
        //    try
        //    {
        //        AuthorizeNet.Environment aNetEnvironment = AuthorizeNet.Environment.SANDBOX;
        //        if (environment?.ToLower() == "production")
        //        {
        //            aNetEnvironment = AuthorizeNet.Environment.PRODUCTION;
        //        }

        //        _logging.Info($"Method: AuthNetPaymentService.ProcessPayment Message: Starting payment processing with Environment: {environment}, CardNumber: {data.CardNumber.Substring(data.CardNumber.Length - 4)}, Amount: {data.TotalAmount}");

        //        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = aNetEnvironment;
        //        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //        {
        //            name = CryptoEngine.Decrypt(_authorizeNetConfig.ApiLoginKey),
        //            ItemElementName = ItemChoiceType.transactionKey,
        //            Item = CryptoEngine.Decrypt(_authorizeNetConfig.TransactionKey),
        //        };

        //        data.CardNumber = data.CardNumber.Replace(" ", "");

        //        var creditCard = new creditCardType
        //        {
        //            cardNumber = Utils.TruncateString(data.CardNumber, 16),
        //            expirationDate = data.ExpireDate,
        //            cardCode = data.Cvv,
        //        };

        //        var billingAddress = new customerAddressType
        //        {
        //            firstName = Utils.TruncateString(data.FirstName, 50),
        //            lastName = Utils.TruncateString(data.LastName, 50),
        //            address = Utils.TruncateString(data.Address, 60),
        //            phoneNumber = Utils.CleanPhoneNumber(data.Phone),
        //            email = Utils.TruncateString(data.Email, 255),
        //            city = Utils.TruncateString(data.City, 40),
        //            state = data.State,
        //            zip = Utils.TruncateString(data.Zip, 20)
        //        };

        //        decimal price = data.TotalAmount;

        //        if (environment?.ToLower() != "production")
        //        {
        //            price = 1;
        //        }

        //        var paymentType = new paymentType { Item = creditCard };
        //        var transactionRequest = new transactionRequestType
        //        {
        //            transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
        //            amount = price,
        //            payment = paymentType,
        //            billTo = billingAddress
        //        };

        //        var request = new createTransactionRequest { transactionRequest = transactionRequest };
        //        var controller = new createTransactionController(request);
        //        controller.Execute();
        //        var response = controller.GetApiResponse();

        //        if (response != null)
        //        {
        //            responseResult.TransactionId = response.transactionResponse?.transId;
        //            responseResult.TransactionStatus = GetTransactionStatus(response.transactionResponse?.responseCode);

        //            if (response.transactionResponse?.errors != null)
        //            {
        //                foreach (var errorItem in response.transactionResponse.errors)
        //                {
        //                    responseResult.ErrorMessages.Add($"{errorItem.errorCode}: {errorItem.errorText}");
        //                }
        //                responseResult.IsSuccess = false;
        //            }

        //            if (response.messages != null)
        //            {
        //                foreach (var messageItem in response.messages.message)
        //                {
        //                    responseResult.SuccessMessages.Add($"{messageItem.code}: {messageItem.text}");
        //                }
        //            }

        //            if (response.transactionResponse?.messages != null)
        //            {
        //                foreach (var messageItem in response.transactionResponse.messages)
        //                {
        //                    responseResult.SuccessMessages.Add($"{messageItem.code}: {messageItem.description}");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            responseResult.TransactionStatus = "Unknown Error";
        //            responseResult.ErrorMessages.Add("Null response from Authorize.NET");
        //            responseResult.IsSuccess = false;
        //            _logging.Fatal("Method: AuthNetPaymentService.ProcessPayment Message: Null response from Authorize.NET");
        //        }

        //        if (responseResult.ErrorMessages?.Count > 0)
        //        {
        //            _logging.Fatal($"Method: AuthNetPaymentService.ProcessPayment Message: Error(s) processing Authorize.NET payment Errors: {string.Join(" | ", responseResult.ErrorMessages)}");
        //        }
        //        else
        //        {
        //            responseResult.IsSuccess = true;
        //            _logging.Info($"Method: AuthNetPaymentService.ProcessPayment Message: Payment processing succeeded Environment: {environment}, CardNumber: {data.CardNumber.Substring(data.CardNumber.Length - 4)},  Amount: {data.TotalAmount}");
        //        }

        //        return responseResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logging.Info($"Method: AuthNetPaymentService.ProcessPayment Message: Payment failed with Environment: {environment}, CardNumber: {data.CardNumber.Substring(data.CardNumber.Length - 4)},  Amount: {data.TotalAmount} Exception: {ex.Message}");
        //        responseResult.ErrorMessages.Add($"Exception: {ex.Message}");
        //        responseResult.IsSuccess = false;
        //        return responseResult;
        //    }
        //}


        public CreateTransactionResponse ProcessPayment(AuthPaymentRequestDTO data)
        {
            string environment = _authorizeNetConfig.Environment;
            var responseResult = new CreateTransactionResponse();
            try
            {
                AuthorizeNet.Environment aNetEnvironment = AuthorizeNet.Environment.SANDBOX;
                if (environment?.ToLower() == "production")
                {
                    aNetEnvironment = AuthorizeNet.Environment.PRODUCTION;
                }

                _logging.Info($"Method: AuthNetPaymentService.ProcessPayment Message: Starting payment processing with Environment: {environment}, CardNumber: {data.CardNumber.Substring(data.CardNumber.Length - 4)}, Amount: {data.TotalAmount}");

                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = aNetEnvironment;
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = CryptoEngine.Decrypt(_authorizeNetConfig.ApiLoginKey),
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = CryptoEngine.Decrypt(_authorizeNetConfig.TransactionKey),
                };

                data.CardNumber = data.CardNumber.Replace(" ", "");

                var creditCard = new creditCardType
                {
                    cardNumber = Utils.TruncateString(data.CardNumber, 16),
                    expirationDate = data.ExpireDate,
                    cardCode = data.Cvv,
                };

                var billingAddress = new customerAddressType
                {
                    firstName = Utils.TruncateString(data.FirstName, 50),
                    lastName = Utils.TruncateString(data.LastName, 50),
                    address = Utils.TruncateString(data.Address, 60),
                    phoneNumber = Utils.CleanPhoneNumber(data.Phone),
                    email = Utils.TruncateString(data.Email, 255),
                    city = Utils.TruncateString(data.City, 40),
                    state = data.State,
                    zip = Utils.TruncateString(data.Zip, 20)
                };

                decimal price = data.TotalAmount;

                if (environment?.ToLower() != "production")
                {
                    price = 1;
                }

                var paymentType = new paymentType { Item = creditCard };
                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                    amount = price,
                    payment = paymentType,
                    billTo = billingAddress
                };

                var request = new createTransactionRequest { transactionRequest = transactionRequest };
                var controller = new createTransactionController(request);
                controller.Execute();
                var response = controller.GetApiResponse();

                if (response != null)
                {
                    responseResult.TransactionId = response.transactionResponse?.transId;
                    responseResult.TransactionStatus = GetTransactionStatus(response.transactionResponse?.responseCode);

                    if (response.transactionResponse?.errors != null)
                    {
                        foreach (var errorItem in response.transactionResponse.errors)
                        {
                            if (errorItem.errorCode == "11")
                            {
                                responseResult.ErrorMessages.Add("Duplicate transaction detected. Please wait for two minutes before trying again.");
                                //_logging.Warn("Duplicate transaction detected.");
                            }
                            else
                            {
                                responseResult.ErrorMessages.Add($"{errorItem.errorCode}: {errorItem.errorText}");
                            }
                        }
                        responseResult.IsSuccess = false;
                    }

                    if (response.messages != null)
                    {
                        foreach (var messageItem in response.messages.message)
                        {
                            responseResult.SuccessMessages.Add($"{messageItem.code}: {messageItem.text}");
                        }
                    }

                    if (response.transactionResponse?.messages != null)
                    {
                        foreach (var messageItem in response.transactionResponse.messages)
                        {
                            responseResult.SuccessMessages.Add($"{messageItem.code}: {messageItem.description}");
                        }
                    }
                }
                else
                {
                    responseResult.TransactionStatus = "Unknown Error";
                    responseResult.ErrorMessages.Add("Null response from Authorize.NET");
                    responseResult.IsSuccess = false;
                    _logging.Fatal("Method: AuthNetPaymentService.ProcessPayment Message: Null response from Authorize.NET");
                }

                if (responseResult.ErrorMessages?.Count > 0)
                {
                    _logging.Fatal($"Method: AuthNetPaymentService.ProcessPayment Message: Error(s) processing Authorize.NET payment Errors: {string.Join(" | ", responseResult.ErrorMessages)}");
                }
                else
                {
                    responseResult.IsSuccess = true;
                    _logging.Info($"Method: AuthNetPaymentService.ProcessPayment Message: Payment processing succeeded Environment: {environment}, CardNumber: {data.CardNumber.Substring(data.CardNumber.Length - 4)},  Amount: {data.TotalAmount}");
                }

                return responseResult;
            }
            catch (Exception ex)
            {
                _logging.Info($"Method: AuthNetPaymentService.ProcessPayment Message: Payment failed with Environment: {environment}, CardNumber: {data.CardNumber.Substring(data.CardNumber.Length - 4)},  Amount: {data.TotalAmount} Exception: {ex.Message}");
                responseResult.ErrorMessages.Add($"Exception: {ex.Message}");
                responseResult.IsSuccess = false;
                return responseResult;
            }
        }



        public CreateTransactionResponse RefundPayment(RefundPaymentDTO obj)
        {
            string environment = _authorizeNetConfig.Environment;
            var responseResult = new CreateTransactionResponse();

            try
            {
                AuthorizeNet.Environment aNetEnvironment = AuthorizeNet.Environment.SANDBOX;
                if (environment?.ToLower() == "production")
                {
                    aNetEnvironment = AuthorizeNet.Environment.PRODUCTION;
                }

                _logging.Info($"Method: AuthNetPaymentService.RefundPayment Message: Starting refund with Environment: {environment}, TransactionID: {obj.TransactionId}, RefundAmount: {obj.RefundAmount}");

                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = aNetEnvironment;
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = CryptoEngine.Decrypt(_authorizeNetConfig.ApiLoginKey),
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = CryptoEngine.Decrypt(_authorizeNetConfig.TransactionKey),
                };

                var refundTransactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.refundTransaction.ToString(),
                    amount = obj.RefundAmount,
                    refTransId = obj.TransactionId,
                };

                var request = new createTransactionRequest { transactionRequest = refundTransactionRequest };
                var controller = new createTransactionController(request);
                controller.Execute();
                var response = controller.GetApiResponse();

                if (response != null && response.transactionResponse != null)
                {
                    responseResult.TransactionId = response.transactionResponse.transId;
                    responseResult.TransactionStatus = GetTransactionStatus(response.transactionResponse.responseCode);

                    if (response.transactionResponse.errors != null)
                    {
                        foreach (var errorItem in response.transactionResponse.errors)
                        {
                            responseResult.ErrorMessages.Add($"{errorItem.errorCode}: {errorItem.errorText}");
                        }
                        responseResult.IsSuccess = false;
                    }
                    else
                    {
                        responseResult.IsSuccess = true;
                        _logging.Info($"Method: AuthNetPaymentService.RefundPayment Message: Refund succeeded Environment: {environment}, TransactionID: {obj.TransactionId}, RefundAmount: {obj.RefundAmount}");
                    }
                }
                else
                {
                    responseResult.TransactionStatus = "Unknown Error";
                    responseResult.ErrorMessages.Add("Null response from Authorize.NET");
                    responseResult.IsSuccess = false;
                    _logging.Fatal("Method: AuthNetPaymentService.RefundPayment Message: Null response from Authorize.NET");
                }

                return responseResult;
            }
            catch (Exception ex)
            {
                _logging.Info($"Method: AuthNetPaymentService.RefundPayment Message: Refund failed with Environment: {environment}, TransactionID: {obj.TransactionId}, RefundAmount: {obj.RefundAmount} Exception: {ex.Message}");
                responseResult.ErrorMessages.Add($"Exception: {ex.Message}");
                responseResult.IsSuccess = false;
                return responseResult;
            }
        }



        public string GetTransactionStatus(string code)
		{
			if (Enum.TryParse(code, out TransactionResponseCode responseCode))
			{
				switch (responseCode)
				{
					case TransactionResponseCode.Approved:
						return "Approved";
					case TransactionResponseCode.Declined:
						return "Declined";
					case TransactionResponseCode.Error:
						return "Error";
					case TransactionResponseCode.HeldForReview:
						return "Held For Review";
					default:
						return "Unknown Error";
				}
			}
			else
			{
				return "Null response Code";
			}
		}

        public PaymentDTO SavePayment(PaymentDTO obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "PaymentDTO object cannot be null.");
            }

            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            // Ensure that the _mapper and _FePaymentRepository are not null
            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized.");
            }

            if (_FePaymentRepository == null)
            {
                throw new InvalidOperationException("Payment repository is not initialized.");
            }

            // Map the PaymentDTO to Fe_payment entity
            Fe_payment ent = _mapper.Map<PaymentDTO, Fe_payment>(obj);

            // Check if mapping was successful
            if (ent == null)
            {
                throw new InvalidOperationException("Mapping from PaymentDTO to Fe_payment entity failed.");
            }

            // Insert the payment entity and return the updated DTO
            obj.PaymentId = _FePaymentRepository.Insert(ent);

            return obj;
        }

       
    }
}
