using BU.DTO.DTOs.Jobs;
using BU.Services.IServices.Jobs;
using Common.Helper;
using Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebRAPI.Base;
using Microsoft.Extensions.Configuration;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.Services.Services.Jobs;

namespace WebAPI.Controllers.Jobs
{
    [Route("api/fejobcontract")]
    [ApiController]
    //[Authorize]
    public class JobContractController : BaseController
    {
        private readonly IFeJobContractService _feJobContractService;
        private readonly IFeJobContractProgressService _feJobContractProgressService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public JobContractController(IFeJobContractService feJobContractService,IFeJobContractProgressService feJobContractProgressService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _feJobContractService = feJobContractService;
            _feJobContractProgressService = feJobContractProgressService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpPost("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<FeJobContractDTO> data = _feJobContractService.loadGrid(new string[] { });
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
        public IActionResult Add([FromBody] FeJobContractDTO obj)
        {
            Result result = new Result(true);
            try
            {
                FeJobContractDTO pd = _feJobContractService.Add(obj);
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
                FeJobContractProgressDTO data = _feJobContractProgressService.Get(id);
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
        public IActionResult Update([FromBody] FeJobContractProgressDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_feJobContractProgressService.Update(obj))
                    result.Data = _feJobContractProgressService.Get(obj.ContractProgressId);
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
        public IActionResult Delete([FromBody] FeJobContractProgressDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobContractProgressService.Delete(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Json(result);
        }

        [HttpPost("saveJobContractProgress")]
        public IActionResult UpdateJobProgress([FromBody] FeJobContractProgressDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _feJobContractProgressService.SaveJobContractProgress(obj);
                result.Message = "Job progress added successfully.";

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
