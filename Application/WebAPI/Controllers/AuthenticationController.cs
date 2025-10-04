using Common.Helper;
using Logging;
using Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using DTO.DTOs.Users;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using BU.DTO.DTOs.Common.Account;
using Microsoft.AspNetCore.Authorization;
using DTO.Core;

namespace WebAPI.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogging _logging;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IConfiguration configuration, ILogging logging, IAuthenticationService authenticationService)
        {
            _configuration = configuration;
            _logging = logging;
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthDTO model)
        {
            Result result = new Result(true);
            try
            {
                UserContext authenticated = null;

                // Check if login is via Google
                if (model.LoginType == "Google")
                {
                    // Authenticate via GoogleId (Google Login)
                    authenticated = _authenticationService.AuthenticateViaGoogle(model);
                }
                else
                {
                   
                    authenticated = _authenticationService.Authenticate(model);
                }


                if (authenticated != null && authenticated.User != null)
                {
                    _logging.Info($"Authenticated======> @Email: {model.EmailAddress}");

                    var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", authenticated.User.UserId.ToString()),
                new Claim("UserName", authenticated.User.UserName),
                new Claim("UserType", authenticated.User.UserType),
                new Claim("UserEmail", authenticated.User.EmailAddress)
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    authenticated.AuthToken = new JwtSecurityTokenHandler().WriteToken(token);
                    authenticated.User.EncUserId = CryptoEngine.Encrypt(authenticated.User.UserId);
                }
			
				else
				{
					result.Success = false;
					result.Message = "The provided credentials are incorrect.";
					_logging.Error($"Authentication Failed======> @Email: {model.EmailAddress}  @Error: {result.Message}");
				}

                result.Data = authenticated;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = "Failed to login, please contact the Administrator.";
                _logging.Fatal($"Authentication Failed======> @Email: {model.EmailAddress}  @Error: {exc.ToString()}");
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


        [HttpPost("forgotPassword")]
		public IActionResult ForgotPassword(ForgotPasswordRequest model)
		{
			Result result = new Result(true);
			try
			{
				bool isSuccess = _authenticationService.ForgotPassword(model, "FiggersEnterprise");
				if (isSuccess)
					result.Message = "Please check your email for password reset instructions";
				else
				{
					result.Success = false;
					result.Message = "Email does not exist";
					_logging.Error("Email does not exist");
				}

			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
				_logging.Fatal($"Method : ForgotPassword -> Error -> {exc.Message}");
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

		[HttpPost("validateResetToken")]
		public IActionResult ValidateResetToken(ValidateResetTokenRequest model)
		{
			Result result = new Result(true);
			try
			{
				string token = _authenticationService.ValidateResetToken(model);

				if (token != null)
					result.Data = token;
				else
				{
					result.Success = false;
					result.Message = "The token has been expired.";
					_logging.Error($"Method: ValidateResetToken Error -> The token has been expired.");
				}

			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
				_logging.Fatal($"Method : ValidateResetToken Error -> {exc.Message}");

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

		[HttpPost("validateResetOTP")]
		public IActionResult ValidateResetOTP(ValidateResetOTPRequest model)
		{
			Result result = new Result(true);
			try
			{
				string otp = _authenticationService.ValidateResetOTP(model);

				if (otp != null)
					result.Data = otp;
				else
				{
					result.Success = false;
					result.Message = "The token has been expired.";
					_logging.Error("The token has been expired.");
				}

			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
				_logging.Fatal($"Method :ValidateResetOTP -> Error -> {exc.Message}");

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

		[HttpPost("resetPassword")]
		public IActionResult ResetPassword(ResetPasswordRequest model)
		{
			Result result = new Result(true);
			try
			{
				bool isSuccess = _authenticationService.ResetPassword(model);
				if (isSuccess)
					result.Message = "Password reset successfully.";
				else
				{
					result.Success = false;
					result.Message = "The token has been expired.";
					_logging.Error("The token has been expired.");
				}
			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
				_logging.Fatal($"Method : ResetPassword -> Error -> {exc.Message}");

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

		[HttpPost("resetPasswordByOTP")]
		public IActionResult ResetPasswordByOTP(ResetPasswordByOPTRequest model)
		{
			Result result = new Result(true);
			try
			{
				bool isSuccess = _authenticationService.ResetPasswordByOTP(model);
				if (isSuccess)
				{
					result.Message = "Password reset successfully.";
				}
				else
				{
					result.Success = false;
					result.Message = "The OTP has been expired.";
					_logging.Error("The OTP has been expired.");
				}
			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
                _logging.Fatal($"Method : ResetPasswordByOTP -> Error -> {exc.Message}");
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

		//[Authorize]
		[HttpPost("changePassword")]
		public IActionResult ChangePassword([FromBody] ChangePasswordRequest model)
		{
			Result result = new Result(true);
			try
			{
				bool isSuccess = _authenticationService.ChangePassword(model);
				if (isSuccess)
					result.Message = "Password has been changed successfully.";
				else
				{
					result.Success = false;
					result.Message = "Invalid old password.";
					_logging.Error("Invalid old password.");
				}
			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
                _logging.Fatal($"Method : ChangePassword -> Error -> {exc.Message}");
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

		[HttpPost("resendOTP")]
		public IActionResult ResendOTP(ResendOTPRequestDTO model)
		{
			Result result = new Result(true);
			try
			{
				bool isSuccess = _authenticationService.ResendOTP(model, "FiggersEnterprise");
				if (isSuccess)
					result.Message = "OTP has been to your email, please check your email.";
				else
				{
					result.Success = false;
					result.Message = "Account does not exist";
					_logging.Error("Account does not exist");
				}

			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
                _logging.Fatal($"Method : ResendOTP -> Error -> {exc.Message}");
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


        //[Authorize]
        [HttpPost("de_activate_account")]
        public IActionResult DeActivateAccount([FromBody] DeActivateAccountDTO model)
        {
            Result result = new Result(true);
            try
            {
                bool isSuccess = _authenticationService.DeActivateAccount(model);
                if (isSuccess)
                    result.Message = "Account has been De-Activated successfully.";
                else
                {
                    result.Success = false;
                    result.Message = "Invalid old password.";
                    _logging.Error("Invalid old password.");
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : ChangePassword -> Error -> {exc.Message}");
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
