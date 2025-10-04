using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Logging;
using Microsoft.Extensions.Configuration;
using DTO.DTOs.Users;
using Common.Helper;
using Common;
using System.Reflection;

namespace WebRAPI.Base
{
    public class BaseController : Controller
    {
        private readonly ILogging _logging;
        private readonly  IConfiguration _configuration;
        private ILogging logging;
        private AutoMapper.Configuration.IConfiguration configuration;

        // private readonly IUserAuditLogService _userAuditLogService;
        public BaseController(ILogging logging, IConfiguration configuration)//,IUserAuditLogService userAuditLogService)
        {
            _logging = logging;
            _configuration = configuration;
           // _userAuditLogService = userAuditLogService;
        }

        public BaseController(ILogging logging, AutoMapper.Configuration.IConfiguration configuration)
        {
            this.logging = logging;
            this.configuration = configuration;
        }


        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    base.OnActionExecuted(context);
        //}

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var isErrorLog = _configuration["ErrorLogging:isErrorLog"];
        //    bool isLog = Convert.ToBoolean(isErrorLog);
        //    if (isLog)
        //    {
        //        try
        //        {
        //            var claimsList = ((System.Security.Claims.ClaimsIdentity)((Microsoft.AspNetCore.Http.DefaultHttpContext)context.HttpContext).User.Identity).Claims.ToList();
        //            //string IP =context.HttpContext.Connection.RemoteIpAddress?.ToString();
        //            var loggedInUser = "User";
        //            var encUserId = "";
        //            var sessionId = "";
        //            if (claimsList.Count > 0)
        //            {
        //                encUserId = claimsList[3].Value;
        //                loggedInUser = claimsList[4].Value + " " + claimsList[5].Value;
        //                sessionId = claimsList[7].Value;
        //            }

        //            string controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
        //            string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
        //            //string id = context.ActionArguments["id"].ToString();
        //           // ParameterInfo[] methodInfo =  ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetParameters();
        //            string logKey = controllerName + "_" + actionName;

        //            string logMessage = loggedInUser + ", Screen:  " + controllerName + ", Action: " + actionName;

        //            _logging.Info(logMessage);
        //            if(Utility.LogDic.ContainsKey(controllerName+"_"+actionName))
        //            {
        //                _userAuditLogService.Add(new UserAuditLogDTO
        //                {
        //                    SessionId = sessionId,
        //                    Area = controllerName,
        //                    Activity = actionName,
        //                    EncUserID = encUserId,
        //                    Details = Utility.LogDic[controllerName + "_" + actionName]
        //                });
        //            }

        //        }

        //        catch (Exception exc)
        //        {
        //            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
        //            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
        //            _logging.Info($"Screen: {controllerName}, Action:  {actionName } ");
        //        }
        //    }

        //    return;
        //}
    }
}
