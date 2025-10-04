using Common.Helper;
using DTO.DTOs.Setup;
using Logging;
using Services.IServices.Setup;
using WebRAPI.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace WebRAPI.Controllers.Setup
{
    [Route("api/rolepermission")]
    [ApiController]
    [Authorize]
    public class RolePermissionController : BaseController
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public RolePermissionController(IRolePermissionService rolePermissionService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _rolePermissionService = rolePermissionService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpGet("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<RolePermissionDTO> data = _rolePermissionService.loadGrid(new string[] { });
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
        [HttpPost("add")]
        public IActionResult Add([FromBody] RolePermissionDTO obj)
        {
            Result result = new Result(true);
            try
            {
                RolePermissionDTO pd = _rolePermissionService.Add(obj);
                result.Data = pd;

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Json(result);
        }
        [HttpPost("get")]
        public IActionResult Get([FromBody] long id)
        {
            Result result = new Result(true);
            try
            {
                RolePermissionDTO data = _rolePermissionService.Get(id);
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
        [HttpPost("update")]
        public IActionResult Update([FromBody] RolePermissionDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_rolePermissionService.Update(obj))
                    result.Data = _rolePermissionService.Get(obj.RolePermissionID);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Json(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] RolePermissionDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _rolePermissionService.Delete(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Json(result);
        }
    }
}
