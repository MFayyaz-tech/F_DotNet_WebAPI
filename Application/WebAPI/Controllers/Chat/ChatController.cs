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
using BU.DTO.DTOs.Users;
using BU.Services.IServices.Chat;
using BU.DTO.DTOs.RequestDTO.Trainings;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace FH.WebRAPI.Controllers.User
{
    [Route("api/chat")]
    [ApiController]
    //[Authorize]
    public class ChatController : BaseController
    {
        private readonly IFeChatService _chatServices;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public ChatController(IFeChatService userService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _chatServices = userService;
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
                result.Data = _chatServices.GetChatList(UserId);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : chat -> getChatList -> Error -> {exc.Message}");
            }
            return Json(result);
        }
      
        [HttpGet("getChatsBetweenUsers")]
        public IActionResult GetChatsBetweenUsers([FromQuery] long senderId, [FromQuery] long receiverId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            Result result = new Result(true);
            try
            {
                var chats = _chatServices.GetChatsBetweenUsers(senderId, receiverId, pageNumber, pageSize);
                result.Data = chats;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : chat -> GetChatsBetweenUsers -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("uploadImages")]
        public IActionResult UploadImages(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            var uploadedFiles = new List<string>();

            try
            {
                // Fetch the root path from configuration
                string rootPath = _configuration["Web:DocumentPath"];
                if (string.IsNullOrWhiteSpace(rootPath))
                {
                    throw new ArgumentException("DocumentPath is not configured correctly.");
                }

                // Log the root path
                _logging.Info($"rootPath: {rootPath}");

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        // Generate the file path with a timestamp
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        string relativePath = Path.Combine("Documents", "UploadedFiles", $"{timestamp}_{file.FileName}");
                        string fullPath = Path.Combine(rootPath, relativePath);

                        // Log the constructed paths
                        _logging.Info($"relativePath: {relativePath}");
                        _logging.Info($"fullPath: {fullPath}");

                        // Ensure directory exists
                        string directoryPath = Path.GetDirectoryName(fullPath);
                        if (string.IsNullOrWhiteSpace(directoryPath))
                        {
                            throw new ArgumentException("Directory path is not valid.");
                        }

                        // Log the directory path
                        _logging.Info($"directoryPath: {directoryPath}");

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Upload the file
                        using (Stream stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        // Add the relative path to the list of uploaded files
                        uploadedFiles.Add(relativePath);
                    }
                }

                return Ok(new { Paths = uploadedFiles });
            }
            catch (Exception exc)
            {
                _logging.Fatal($"Method : chat -> UploadImages -> Error -> {exc.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("download")]
        public IActionResult Download(string filePath)
        {
            try
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string fullPath = Path.Combine(rootPath, filePath);

                if (!System.IO.File.Exists(fullPath))
                {
                    return NotFound("File not found");
                }

                var fileBytes = System.IO.File.ReadAllBytes(fullPath);
                var contentType = "application/octet-stream";

                return File(fileBytes, contentType, Path.GetFileName(fullPath));
            }
            catch (Exception exc)
            {
                _logging.Fatal($"Method: Download -> Error: {exc.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}



