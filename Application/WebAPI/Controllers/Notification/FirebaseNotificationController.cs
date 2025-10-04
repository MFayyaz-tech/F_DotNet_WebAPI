using AutoMapper;
using BU.DTO.DTOs.RequestDTO.FCM;
using BU.Services.IServices.Notification;
using Common.Helper;
using DA.DAO.DAO.Notifications;
using DA.Entities.Notifications;
using DAO;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRAPI.Base;
using static BU.DTO.DTOs.RequestDTO.FCM.NotificationRequestDTO;

[Route("api/notifications")]
[ApiController]
public class FCMController : BaseController
{
    private readonly INotificationService _notificationService;
    private readonly ILogging _logging;
    private readonly IRepository<Fe_notifications_tokens> _FeNotificationRepository;

    private readonly IConfiguration _configuration;

    public FCMController(INotificationService notificationService, IRepository<Fe_notifications_tokens> FeNotificationRepository, ILogging logging, IConfiguration configuration)
        : base(logging, configuration)
    {
        _notificationService = notificationService;
        _logging = logging;
        _FeNotificationRepository = FeNotificationRepository;
        _configuration = configuration;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {

        Result result = new Result(true);


        if (string.IsNullOrEmpty(request.ReciverId.ToString()) || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Body))
        {
            result.Message = "Invalid request parameters.";
            result.Success = false;
            return BadRequest(result);
        }


        try
        {
            var user = _FeNotificationRepository.GetList(Common.Database.MAIN, FeNotificationsDAO.GetUserToken, new { UserId = request.ReciverId }).FirstOrDefault();

            if (user == null || string.IsNullOrEmpty(user.token))
            {
                result.Message = "User token not found.";
                result.Success = false;
                return NotFound(result);
            }

            var token = user.token;

         

            await FirebaseMessagingService.SendMessageAsync(token, request.Title, request.Body, request.Data);

            result.Message = "Notification sent successfully.";
            result.Success = true;
            return Json(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    [HttpPost("saveFcmToken")]
    public IActionResult SaveFcmToken([FromBody] SaveFcmTokenRequestDTO obj)
    {
        Result result = new Result(true);
        try
        {
            SaveFcmTokenRequestDTO data = _notificationService.Add(obj);
            result.Data = data;
        }
        catch (Exception exc)
        {
            result.Success = false;
            result.Message = exc.Message;
            _logging.Fatal($"Method : api/Notifications -> saveFcmToken -> Error -> {exc.Message}");
        }
        return Ok(result);
    }
}
