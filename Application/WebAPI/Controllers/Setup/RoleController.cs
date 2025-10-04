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
    [Route("api/role")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public RoleController(IRoleService roleService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _roleService = roleService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpGet("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<RoleDTO> data = _roleService.loadGrid(new string[] { });
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
        public IActionResult Add([FromBody] RoleDTO obj)
        {
            Result result = new Result(true);
            try
            {
                RoleDTO pd = _roleService.Add(obj);
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
                RoleDTO data = _roleService.Get(id);
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
        public IActionResult Update([FromBody] RoleDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_roleService.Update(obj))
                    result.Data = _roleService.Get(obj.RoleID);
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
        public IActionResult Delete([FromBody] RoleDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _roleService.Delete(obj);
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


