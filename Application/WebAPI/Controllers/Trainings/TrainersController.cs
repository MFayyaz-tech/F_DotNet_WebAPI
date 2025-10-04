using BU.DTO.DTOs.Trainings;
using BU.Services.IServices.Trainings;
using Common.Helper;
using Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebRAPI.Base;
using Microsoft.Extensions.Configuration;
using DTO.DTOs.User;
using FH.Services.IServices.User;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.RequestDTO.Trainings;

namespace WebAPI.Controllers.Trainings
{
    [Route("api/trainers")]
    [ApiController]
    //[Authorize]
    public class TrainersController : BaseController
    {
        private readonly ITrainersService _TrainersService;
        private readonly IUserService _UserService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public TrainersController(ITrainersService feTrainersService, IUserService UserService,ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _TrainersService = feTrainersService;
            _UserService = UserService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpPost("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<TrainersDTO> data = _TrainersService.loadGrid(new string[] { });
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
        public IActionResult Add([FromBody] TrainersDTO obj)
        {
            Result result = new Result(true);
            try
            {
                UserDTO existingUser = _UserService.CheckIfDuplicateUserExists(new UserDTO() { EmailAddress = obj.EmailAddress });
                if (existingUser == null)
                {
                    TrainersDTO pd = _TrainersService.Add(obj);
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
        [HttpPost("get")]
        public IActionResult Get([FromBody] long id)
        {
            Result result = new Result(true);
            try
            {
                TrainersDTO data = _TrainersService.Get(id);
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainers -> Get -> Error -> {exc.Message}");
            }
            return Json(result);

        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] TrainersDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_TrainersService.Update(obj))
                    result.Data = _TrainersService.Get(obj.TrainerId);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainers -> Update -> Error -> {exc.Message}");
            }
            return Json(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] TrainersDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _TrainersService.Delete(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainers -> Delete -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("getTrainersByAgencyId")]
        public IActionResult GetTrainersByAgencyId([FromBody] TrainerRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _TrainersService.GetTrainersByAgencyId(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainers -> GetTrainersByAgencyId -> Error -> {exc.Message}");
            }
            return Json(result);
        }
    }
}
