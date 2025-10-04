

using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.RequestDTO.Customer;
using BU.Services.IServices.Customer;
using BU.Services.Services.Customer;
using Common.Helper;
using DA.DAO.DAO.Customer;
using DTO.DTOs.User;
using DTO.DTOs.Users;
using FH.Services.IServices.User;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using WebRAPI.Base;

namespace WebAPI.Controllers.Customer
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly IFeCustomerService _CustomerService;
        private readonly IFeCustomerCardsService _customerCardsService;
        private readonly IUserService _UserService;
        private readonly ILogging _logging;
        private readonly IAuthenticationService _AuthanticationServices;


        IConfiguration _configuration;
        public CustomerController(IUserService UserService, IAuthenticationService AuthanticationServices, IFeCustomerService CustomerService,
            IFeCustomerCardsService customerCardsService,
            ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _CustomerService = CustomerService;
            _AuthanticationServices = AuthanticationServices;
            _UserService = UserService;
            _customerCardsService = customerCardsService;
            _logging = logging;
            _configuration = configuration;
        }

        //[HttpPost("registerCustomer")]
        //public IActionResult RegisterUser([FromBody] CustomerRegistrationRequestDTO obj)
        //{
        //    Result result = new Result(true);
        //    try
        //    {
        //        UserDTO existingUser = _UserService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
        //        if (existingUser == null)
        //        {
        //            CustomerRegistrationRequestDTO pd = _CustomerService.RegisterCustomer(obj);
        //            result.Data = "Customer registered successfully, please check your email for more information";
        //            result.Message = "Customer registered successfully, please check your email for more information";
        //        }
        //        else
        //        {
        //            result.Message = "Email already exists";
        //            result.Success = false;
        //        }


        //    }
        //    catch (Exception exc)
        //    {
        //        result.Success = false;
        //        result.Message = exc.Message;
        //        _logging.Fatal(exc.Message);
        //    }
        //    if (result.Success)
        //    {
        //        return Json(result);
        //    }
        //    else
        //    {
        //        return Unauthorized(new { message = result.Message });
        //    }
        //}


        [HttpPost("registerCustomer")]
        public IActionResult RegisterCustomer([FromBody] CustomerRegistrationRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (obj.LoginType == "Google" || obj.LoginType == "Facebook" || obj.LoginType == "Apple")
                {
                    // Check if the user exists based on EmailAddress
                    UserDTO existingUser = _UserService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
                    if (existingUser == null)
                    {
                        // Register new customer with Google Login
                        var registerResult = _CustomerService.RegisterCustomerViaGoogle(obj);

                        var loginModel = new UserAuthDTO
                        {
                            EmailAddress = obj.EmailAddress,
                            LoginType = obj.LoginType,
                            GoogleId = obj.GoogleId
                        };

                        // Authenticate the user after registration
                        var authenticationResult = _AuthanticationServices.AuthenticateViaGoogle(loginModel);

                        if (authenticationResult != null && authenticationResult.User != null)
                        {
                            result.Success = true;
                            result.Data = authenticationResult;
                            result.Message = $"Customer registered and logged in successfully via {obj.LoginType}";
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Failed to log in the customer via Google.";
                        }
                    }
                    else
                    {
                        // If user already exists, call Authenticate API to log in the user
                        var loginModel = new UserAuthDTO
                        {
                            EmailAddress = obj.EmailAddress,
                            LoginType = obj.LoginType,
                            GoogleId = obj.GoogleId
                        };

                        // Manually call the Authenticate method and capture the result
                        var authenticationResult = _AuthanticationServices.AuthenticateViaGoogle(loginModel);

                        if (authenticationResult != null && authenticationResult.User != null)
                        {
                            result.Success = true;
                            result.Data = authenticationResult;
                            result.Message = "Customer successfully logged in via Google.";
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Failed to log in the customer via Google.";
                        }
                    }
                }
                else
                {
                    // Normal registration flow
                    UserDTO existingUser = _UserService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
                    if (existingUser == null)
                    {
                        var registerResult = _CustomerService.RegisterCustomer(obj);
                        result.Data = registerResult;
                        result.Message = "Customer registered successfully, please check your email for more information";
                    }
                    else
                    {

                        result.Message = "Email already exists";
                        result.Success = false;
                    }
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
                return Unauthorized(new { message = result.Message });
            }
        }




        [HttpPost("updateCustomerProfile")]
        public IActionResult UpdateCustomerProfile([FromBody] UpdateCustomerRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_CustomerService.UpdateCustomer(obj) == true)
                {
                    FeCustomerDTO updatedCustomer = _CustomerService.GetFeCustomerById(new FeCustomerDTO { CustomerId = obj.CustomerId });



                    result.Data = updatedCustomer; 

                }
                else
                {
                    result.Message = "Failed to update customer information, please try again.";
                    result.Success = false;
                }


            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method: updateCustomerProfile  @Error: {exc.Message}");
            }
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return Unauthorized(new { message = result.Message });
            }
        }



        [HttpPost("addCustomerCard")]
        public IActionResult AddCustomerCard([FromBody] FeCustomerCardsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                FeCustomerCardsDTO response =  _customerCardsService.Add(obj);
                if(response !=null)
                {
                    result.Data = response;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Some thing went wrong, please try again";
                }
                

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method: addCustomerCard  @Error: {exc.Message}");
            }
            if (result.Success)
            {
                return Json(result);
            }
            else
            {
                return Unauthorized(new { message = result.Message });
            }
        }

        [HttpPost("deleteCustomerCard")]
        public IActionResult DeleteCustomerCard([FromBody] FeCustomerCardsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                bool response = _customerCardsService.Delete(obj);
                if (response)
                {
                    result.Data = response;
                    result.Message = "Customer card deleted successfully.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Some thing went wrong, please try again";
                }


            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method: DeleteCustomerCard  @Error: {exc.Message}");
            }
            if (result.Success)
            {
                return Json(result);
            }
            else
            {
                return Unauthorized(new { message = result.Message });
            }
        }

        [HttpPost("getCustomerCards")]
        public IActionResult GetCustomerCards([FromBody] FeCustomerCardsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _customerCardsService.GetCustomerCards(obj.CustomerId);
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
                return Unauthorized(new { message = result.Message });
            }
        }
    }
}
