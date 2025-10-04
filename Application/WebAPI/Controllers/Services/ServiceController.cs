using System;
using BU.DTO.DTOs.Services;
using BU.Services.IServices.Services;
using Common.Helper;
using Logging;
using Microsoft.AspNetCore.Mvc;
using WebRAPI.Base;
using Microsoft.Extensions.Configuration;


namespace WebAPI.Controllers.Service
{
    [Route("api/services")]
    [ApiController]
    public class ServiceController : BaseController
    {
        private readonly ILogging _logging;
        private readonly IFeServices _IFeServices;
        private readonly IConfiguration _configuration;

        public ServiceController(IFeServices iFeService, ILogging logging, IConfiguration configuration)
            : base(logging, configuration)
        {
            _IFeServices = iFeService;
            _logging = logging;
            _configuration = configuration;
        }

        [HttpPost("addServices")]
        public IActionResult Add([FromBody] FeServicesDTO obj)
        {
            Result result = new Result(true);
            try
            {
                FeServicesDTO pd = _IFeServices.addService(obj);
                if (pd != null)
                {
                    result.Data = pd;
                    result.Success = true;
                    result.Message = "Services Add Succesfully";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Failed to add services";
                }


            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> Add -> Error -> {exc.Message}");
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

        [HttpGet("GetCustomerServices")]
        public IActionResult GetCustomerServices()
        {
            Result result = new Result(true);
            try
            {

                result.Data = _IFeServices.getCustomerServices();
                result.Message = "Services fetch succesfully";
                result.Success = true;

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/agents -> getAgents -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("GetAgencyServices")]
        public IActionResult GetAgencyServices([FromBody] FeServicesDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _IFeServices.GetAgencyServices(obj);
                result.Success = true;
                result.Message = "Agency services successfully ";

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/agents -> getAgents -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("GetServiceById")]
        public IActionResult GetServiceById([FromBody] FeServicesDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _IFeServices.GetServiceById(obj);
                result.Success = true;
                result.Message = "Get services by Id successfully ";
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/agents -> getAgents -> Error -> {exc.Message}");
            }
            return Json(result);
        }



        [HttpPost("update")]
        public IActionResult Update([FromBody] FeServicesDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = _IFeServices.Update(obj);
                if (result.Success)
                {
                    result.Data = null;
                    result.Message = "Job updated successfully";
                }
                else
                {
                    result.Message = "Failed to update job";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/update -> Update -> Error -> {exc.Message}");
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



        [HttpPost("markObsulate")]
        public IActionResult MarkObsulate([FromBody] FeServicesDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = _IFeServices.MarkObsulate(obj);
                if (result.Success)
                {
                    result.Data = null;
                    result.Message = "Service obsulate successfully";
                }
                else
                {
                    result.Message = "Failed to obsulate service";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/update -> Update -> Error -> {exc.Message}");
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


        [HttpPost("GetServiceByCategories")]
        public IActionResult GetServiceByCategories([FromBody] FeServicesDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _IFeServices.GetServiceByCatergoies(obj);
                result.Success = true;
                result.Message = "Get services by Id successfully ";
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/agents -> getAgents -> Error -> {exc.Message}");
            }
            return Json(result);
        }


    }
}

