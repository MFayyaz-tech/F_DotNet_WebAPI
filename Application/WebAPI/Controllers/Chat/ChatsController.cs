using BU.Services.IServices.Chat;
using Common.Helper;
using DTO.DTOs.User;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using WebRAPI.Base;

namespace WebAPI.Controllers.Chat
{
    [Route("api/chats")]
    [ApiController]
    public class ChatsController : BaseController
    {
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public ChatsController(ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _logging = logging;
            _configuration = configuration;
        }

        [HttpPost("getChatList")]
        public IActionResult GetChatList([FromBody] UserDTO UserId)
        {
            _logging.Fatal($"Method : GetChatList");
            Result result = new Result(true);
            try
            {
                result.Data = "I am here";
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : chat -> getChatList -> Error -> {exc.Message}");
            }
            return Json(result);
        }
    }
}
