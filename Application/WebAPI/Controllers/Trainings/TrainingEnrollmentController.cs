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
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;

namespace WebAPI.Controllers.Trainings
{
    [Route("api/trainingenrollment")]
    [ApiController]
    //[Authorize]
    public class TrainingEnrollmentController : BaseController
    {

        private readonly ITrainingEnrollmentService _TrainingEnrollmentService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public TrainingEnrollmentController(ITrainingEnrollmentService TrainingEnrollmentService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _TrainingEnrollmentService = TrainingEnrollmentService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpPost("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<TrainingEnrollmentDTO> data = _TrainingEnrollmentService.loadGrid(new string[] { });
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

        [HttpPost("enrollCustomer")]
        public IActionResult EnrollCustomer([FromForm] TrainingEnrolRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if(_TrainingEnrollmentService.CheckAlreadyEnrolled(obj))
                {
                    result.Success = false;
                    result.Message = "Customer already enrolled in this training";

                }
                else
                {
                    TrainingEnrollmentDTO pd = _TrainingEnrollmentService.EnrolCustomer(obj);
                    result.Data = pd;
                    result.Message = "Customer enrolled in training successfully.";
                }
              

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/enrollCustomer -> EnrollCustomer -> Error -> {exc.Message}");
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

        [HttpPost("agencyTrainingEnrollmentRequests")]
        public IActionResult AgencyTrainingEnrollmentRequests([FromBody] TrainingEnrolRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                var data = _TrainingEnrollmentService.AgencyTrainingEnrollmentRequests(obj);
                result.Data = data;

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> AgencyTrainingEnrollmentRequests -> Error -> {exc.Message}");
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

        [HttpPost("customerTrainingEnrollmentRequests")]
        public IActionResult CustomerTrainingEnrollmentRequests([FromBody] TrainingEnrolRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                var data = _TrainingEnrollmentService.CustomerEnrollmentRequests(obj);
                result.Data = data;

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> CustomerTrainingEnrollmentRequests -> Error -> {exc.Message}");
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

        

        [HttpPost("add")]
        public IActionResult Add([FromBody] TrainingEnrollmentDTO obj)
        {
            Result result = new Result(true);
            try
            {
                TrainingEnrollmentDTO pd = _TrainingEnrollmentService.Add(obj);
                result.Data = pd;

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> Add -> Error -> {exc.Message}");
            }
            return Json(result);
        }
        [HttpPost("get")]
        public IActionResult Get([FromBody] long id)
        {
            Result result = new Result(true);
            try
            {
                TrainingEnrollmentDTO data = _TrainingEnrollmentService.Get(id);
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> Get -> Error -> {exc.Message}");
            }
            return Json(result);

        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] TrainingEnrollmentDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_TrainingEnrollmentService.Update(obj))
                    result.Data = _TrainingEnrollmentService.Get(obj.EnrollmentId);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> Update -> Error -> {exc.Message}");
            }
            return Json(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] TrainingEnrollmentDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _TrainingEnrollmentService.Delete(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> Delete -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("completeTrainingEnrollment")]
        public IActionResult CompleteTrainingEnrollment([FromBody] TrainingEnrolRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                var response = _TrainingEnrollmentService.CompleteTrainingEnrollment(obj);
                if (response)
                {
                    result.Data = "Training completed successfully";
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Training enrollment completion failed, please try again.";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> CompleteTrainingEnrollment -> Error -> {exc.Message}");
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

        [HttpPost("agencyApproveRejectEnrollmentRequest")]
        public IActionResult AgencyApproveRejectEnrollmentRequest([FromBody] TrainingEnrolRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                var response  = _TrainingEnrollmentService.AgencyApproveRejectEnrollmentRequest(obj);
                if(response)
                {
                    result.Data = "Customer enrolled in training successfully";
                    result.Success = true;
                }else
                {
                    result.Success = false;
                    result.Message = "Customer not enroll, please try again.";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> agencyApproveRejectEnrollmentRequest -> Error -> {exc.Message}");
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

        [HttpPost("getCustomerCompletedTrainings")]
        public IActionResult GetCustomerCompletedTrainings([FromBody] TrainingEnrolRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                var data = _TrainingEnrollmentService.GetCustomerCompletedTrainings(obj);
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                }
                else {

                    result.Success = false;
                    result.Message = "No completed trainings found.";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> getCustomerCompletedTrainings -> Error -> {exc.Message}");
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


        [HttpPost("getEnrollmentMedia")]
        public IActionResult GetEnrollmentMedia([FromBody] MediaRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                var data = _TrainingEnrollmentService.GetEnrollmentMedia(obj);
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                }
                else
                {

                    result.Success = false;
                    result.Message = "No Data found.";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainingenrollment -> GetEnrollmentMedia -> Error -> {exc.Message}");
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
