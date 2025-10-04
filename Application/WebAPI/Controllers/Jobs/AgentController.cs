using System;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.DTO.DTOs.ResponseDTO.Job;
using BU.Services.IServices.Jobs;
using Common.Helper;
using DTO.DTOs.User;
using FH.Services.IServices.User;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebRAPI.Base;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.Jobs
{
    [Route("api/agent")]
    [ApiController]
    public class AgentsController : BaseController
    {
        private readonly IFeAgentsServices agentsServices;
        private readonly IUserService _UserService;
        private readonly ILogging _logging;
        IConfiguration _configuration;

        public AgentsController(IFeAgentsServices feAgentsServices, IUserService UserService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            agentsServices = feAgentsServices;
            _UserService = UserService;
            _logging = logging;
            _configuration = configuration;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AgentsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                UserDTO existingUser = _UserService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
                if (existingUser == null)
                {
                    AgentsDTO pd = agentsServices.Add(obj);
                    result.Data = pd;
                }
                else
                {
                    result.Message = "Email already exists";
                    result.Success = false;
                }


            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainers ->  Add Trainings -> Error -> {exc.Message}");
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
        [HttpPost("getAgents")]
        public IActionResult GetAgents([FromBody] AgentsDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = agentsServices.getAgents(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/agents -> getAgents -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("getAgentDetail")]
        public IActionResult GetAgentDetail([FromBody] AgentsDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = agentsServices.getAgentDetail(obj);

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

