using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.RequestDTO.Customer;
using BU.DTO.DTOs.Users;
using BU.Services.IServices.Agency;
using BU.Services.Services.Customer;
using Common.Helper;
using DTO.DTOs.User;
using DTO.DTOs.Users;
using FH.Services.IServices.User;
using FH.Services.Services.User;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using WebRAPI.Base;

namespace WebAPI.Controllers.Agency
{
	[Route("api/agency")]
	[ApiController]
	//[Authorize]
	public class AgencyController : BaseController
	{
		private readonly IAgencyService _agencyService;
        private readonly IUserService _userService;
        private readonly ILogging _logging;
        private readonly IAuthenticationService _AuthanticationServices;

        IConfiguration _configuration;

		public AgencyController(IUserService userService, IAuthenticationService authenticationController , IAgencyService agencyService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
		{
			_userService = userService;
            _AuthanticationServices = authenticationController;
            _agencyService = agencyService;
			_logging = logging;
			_configuration = configuration;
		}

        //[HttpPost("registerAgency")]
        //public IActionResult RegisterUser([FromBody] RegisterAgencyRequestDTO obj)
        //{
        //	Result result = new Result(true);
        //	try
        //	{
        //		UserDTO existingUser = _userService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress, UserId = 0 });
        //		if (existingUser == null)
        //		{
        //			RegisterAgencyRequestDTO pd = _agencyService.RegisterAgency(obj);
        //			result.Data = "Agency registered successfully, please check your email for more information";
        //			result.Message = "Agency registered successfully, please check your email for more information";
        //		}
        //		else
        //		{
        //			result.Message = "Email already exists";
        //			result.Success = false;
        //			//email already exists.
        //		}


        //	}
        //	catch (Exception exc)
        //	{
        //		result.Success = false;
        //		result.Message = exc.Message;
        //		_logging.Fatal(exc.Message);
        //	}
        //	if (result.Success)
        //	{
        //		return Json(result);
        //	}
        //	else
        //	{
        //		return Unauthorized(new { message = result.Message });
        //	}
        //}



        [HttpPost("registerAgency")]
        public IActionResult RegisterUser([FromBody] RegisterAgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (obj.LoginType == "Google" || obj.LoginType == "Facebook" || obj.LoginType == "Apple")
                {
                    // Check if the user exists based on EmailAddress
                    UserDTO existingUser = _userService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
                    if (existingUser == null)
                    {
                        // Register new agency with Google Login
                        RegisterAgencyRequestDTO pd = _agencyService.RegisterAgencyViaGoogle(obj);
                        var loginModel = new UserAuthDTO
                        {
                            EmailAddress = obj.EmailAddress,
                            LoginType = obj.LoginType,
                            GoogleId = obj.GoogleId
                        };

                        // Manually call the Authenticate method and capture the result
                        var authenticationResult = _AuthanticationServices.AuthenticateViaGoogle(loginModel);

                        if (authenticationResult != null) {
                            result.Success = true;
                            result.Data = authenticationResult;
                            result.Message = $"Agency registered successfully via {obj.LoginType}, please check your email for more information";
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
                            result.Message = "User successfully logged in via Google.";
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Failed to log in the user via Google.";
                        }
                    }
                }
                else
                {
                    // Normal registration flow
                    UserDTO existingUser = _userService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
                    if (existingUser == null)
                    {
                        RegisterAgencyRequestDTO pd = _agencyService.RegisterAgency(obj);
                        result.Data = "Agency registered successfully, please check your email for more information";
                        result.Message = "Agency registered successfully, please check your email for more information";
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










        [HttpGet("loadAgencies")]
		public IActionResult LoadAgencies()
		{
			Result result = new Result(true);
			try
			{
				IEnumerable<AgenciesListReponseDTO> data = _agencyService.LoadAgenciesList(new string[] { });
				result.Data = data;
			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
				_logging.Fatal(exc.Message);
			}
			return Json(result);
		}

		[HttpPost("deleteAgency")]
		public IActionResult DeleteAgency([FromBody] RegisterAgencyRequestDTO obj)
		{
			Result result = new Result(true);
			try
			{
                result.Success = _agencyService.DeleteAgency(obj);
				result.Data = "Agency deleted successfully";
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

        [HttpPost("updateAgencyProfile")]
        public IActionResult UpdateCustomerProfile([FromBody] UpdateAgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_agencyService.UpdateAgency(obj) == true)
                {
                    AgencyDTO updatedAgency = _agencyService.GetFeAgencyById(new AgencyDTO { AgencyId = obj.AgencyId });



                    result.Data = updatedAgency;

                }
                else
                {
                    result.Message = "Failed to update agency information, please try again.";
                    result.Success = false;
                }


            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method: updateAgencyError  @Error: {exc.Message}");
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

        [HttpPost("addAgencyBankCard")]
        public IActionResult AddAgencyBankCard([FromBody] AgencyBankDetailsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = true;
                result.Data = _agencyService.AddBankDetail(obj);
                result.Message = "Bank details added successfully";
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

        [HttpPost("getAgencyBankCard")]
        public IActionResult GetAgencyBankCard([FromBody] AgencyBankDetailsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = true;
                result.Data = _agencyService.GetAgencyCard(obj);
                result.Message = "Bank details added successfully";
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



        [HttpPost("getAgencyJobDetails")]
        public IActionResult GetAgencyJobDetails([FromBody] RegisterAgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = true;
                result.Data = _agencyService.GetAgencyJobsDetail(obj);
                result.Message = "Agency detail fetch successfully";
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


        [HttpPost("getAgencyEarning")]
        public IActionResult GetAgencyEarning([FromBody] AgencyEarningDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = true;
                result.Data = _agencyService.GetAgencyEarning(obj);
                result.Message = "Agency Earning fetch successfully";
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
