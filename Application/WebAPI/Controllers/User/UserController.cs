using Common.Helper;
using Logging;
using FH.Services.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using DTO.DTOs.User;
using WebRAPI.Base;
using Entities.Users;
using BU.DTO.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BU.DTO.DTOs.RequestDTO.Authantication;
using BU.DTO.DTOs.RequestDTO.FCM;
using IN.Common.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
namespace FH.WebRAPI.Controllers.User
{
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public UserController(IUserService userService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _userService = userService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpGet("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
				IEnumerable<UserDTO> data = _userService.loadGrid(new string[] { });
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> loadGrid -> Error -> {exc.Message}");
            }
            return Json(result);
        }

		

		[HttpPost("add")]
        public IActionResult Add([FromBody] UserDTO obj)
        {
            Result result = new Result(true);
            try
            {
                UserDTO pd = _userService.Add(obj);
                result.Data = pd;

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> Add -> Error -> {exc.Message}");
            }

			if (result.Data != null)
			{
				return Json(result);
			}
			else
			{
				return Unauthorized(new { message = "Failed to save record, please try again." });
			}
		}

        [HttpPost("get")]
        public IActionResult Get([FromBody] long id)
        {
            Result result = new Result(true);
            try
            {
                UserDTO data = _userService.Get(id);
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
			if (result.Data != null)
			{
				return Json(result);
			}
			else
			{
				return Unauthorized(new { message = "User not exists." });
			}

		}
        [HttpPost("update")]
        public IActionResult Update([FromBody] UserDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_userService.Update(obj))
                    result.Data = _userService.Get(obj.UserId);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> update -> Error -> {exc.Message}");
            }
			if (result.Data != null)
			{
				return Json(result);
			}
			else
			{
				return Unauthorized(new { message = "User update failed, please try again." });
			}
		}
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] UserDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _userService.Delete(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> delete -> Error -> {exc.Message}");
            }
			if (result.Data != null)
			{
				return Json(result);
			}
			else
			{
				return Unauthorized(new { message = "Failed to delete record, please try again." });
			}
        }
		[HttpPost("reject")]
		public IActionResult Reject([FromBody] UserDTO obj)
		{
			Result result = new Result(true);
			try
			{
				result.Data = _userService.Delete(obj);
			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> Reject -> Error -> {exc.Message}");
            }
			return Json(result);
		}
		[HttpPost("approve")]
		public IActionResult Approve([FromBody] UserDTO obj)
		{
			Result result = new Result(true);
			try
			{
				result.Data = _userService.Delete(obj);
			}
			catch (Exception exc)
			{
				result.Success = false;
				result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> Approve -> Error -> {exc.Message}");
            }
			return Json(result);
		}

        [HttpPost("getAgencyUserId")]
        public IActionResult GetAgencyUserId([FromBody] GetUserIdDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _userService.GetAgencyUserId(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> Approve -> Error -> {exc.Message}");
            }
            return Json(result);
        }





        [HttpPost("getCustomerUserId")]
        public IActionResult GetCustomerUserId([FromBody] GetUserIdDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _userService.GetCustomerUserId(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> Approve -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("ChangePassword")]
        public  IActionResult ChangePassword([FromBody] ChangePasswordDTO model)
        {
            Result result = new Result(true);

            try
            {
                result.Data = _userService.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);

            }
            catch (Exception exc) {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> Approve -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("saveFcmToken")]
        public IActionResult SaveFcmToken([FromBody] SaveFcmTokenRequestDTO model)
        {
            Result result = new Result(true);

            try
            {
                result.Data = _userService.SaveFcmToken(model);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/user -> saveFcmToken -> Error -> {exc.Message}");
            }
            return Json(result);
        }
    }
}



