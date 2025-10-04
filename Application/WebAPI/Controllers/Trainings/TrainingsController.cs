using BU.DTO.DTOs.Trainings;
using BU.Services.IServices.Trainings;
using Common.Helper;
using Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebRAPI.Base;
using Microsoft.Extensions.Configuration;
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Jobs;

namespace WebAPI.Controllers.Trainings
{
    [Route("api/trainings")]
    [ApiController]
    //[Authorize]
    public class TrainingsController : BaseController
    {
        private readonly ITrainingsService _feTrainingsService;
        private readonly ITrainingFeedBackService _feTrainingFeedBackService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public TrainingsController(ITrainingsService feTrainingsService, ITrainingFeedBackService feTrainingFeedBackService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _feTrainingsService = feTrainingsService;
            _feTrainingFeedBackService = feTrainingFeedBackService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpPost("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<TrainingsDTO> data = _feTrainingsService.loadGrid(new string[] { });
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
        public IActionResult Add([FromBody] TrainingsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                TrainingsDTO pd = _feTrainingsService.Add(obj);
                if(pd != null)
                {
                    result.Data = pd.TrainingId;
                    result.Success = true;
                    result.Message = "Training saved successfully.";
                }
                else
                {
                    result.Success = true;
                    result.Message = "Something went wrong, please try again.";
                }

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> Add trainings -> Error -> {exc.Message}");
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
                TrainingsDTO data = _feTrainingsService.Get(id);
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
        public IActionResult Update([FromBody] TrainingsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_feTrainingsService.Update(obj))
                    result.Data = _feTrainingsService.Get(obj.TrainingId);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> Update -> Error -> {exc.Message}");
            }
            return Json(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] TrainingsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.Delete(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Json(result);
        }

        [HttpPost("getTrainingDetails")]
        public IActionResult GetTrainingDetails([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetTrainingDetails(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingDetails -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("upload")]
        public IActionResult Upload([FromForm] UploadTrainingFileDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.Upload(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> Upload File -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("publishTraining")]
        public IActionResult PublishTraining([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.PublishTraining(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> PublishTraining -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        

        [HttpPost("unPublishTraining")]
        public IActionResult UnPublishTraining([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.UnPublishTraining(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> UnPublishTraining -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("completeTraining")]
        public IActionResult CompleteTraining([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.CompleteTraining(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> CompleteTraining -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("getTrainingsByStatus")]
        public IActionResult GetTrainingsByStatus([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetTrainingsByStatus(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingsByStatus -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("getTrainingsByAgencyId")]
        public IActionResult GetTrainingsByAgencyId([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetTrainingsByAgencyId(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingsByAgencyId -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("updateTrainingProgress")]
        public IActionResult UpdateTrainingProgress([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.UpdateTrainingProgress(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> UpdateTrainingProgress -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("getTrainingsCustomerNotEnrolled")]
        public IActionResult GetTrainingsCustomerNotEnrolled([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetTrainingsCustomerNotEnrolled(obj);
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingsCustomerNotEnrolled -> Error -> {ex.Message}");
            }
            return Json(result);
        }

        [HttpPost("getFeaturedTraining")]
        public IActionResult GetFeaturedTraining([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetFeaturedTrainings(obj);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingsCustomerNotEnrolled -> Error -> {ex.Message}");
            }
            return Json(result);
        }


        [HttpPost("getCustomerEnrolledTrainings")]
        public IActionResult GetCustomerEnrolledTrainings([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetCustomerEnrolledTrainings(obj);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logging.Fatal($"Method : api/trainings -> GetCustomerEnrolledTrainings -> Error -> {ex.Message}");
            }
            return Json(result);
        }

        [HttpPost("submitFeedback")]
        public IActionResult submitFeedback([FromBody] TrainingFeedBackDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingFeedBackService.Add(obj);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logging.Fatal($"Method : api/trainings -> submitFeedback -> Error -> {ex.Message}");
            }
            return Json(result);
        }

        [HttpPost("getTrainingFeedBacks")]
        public IActionResult GetTrainingFeedBacks([FromBody] TrainingFeedBackRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingFeedBackService.GetTrainingFeedBacks(obj);
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingFeedBacks -> Error -> {ex.Message}");
            }
            return Json(result);
        }

        [HttpPost("updateTraining")]
        public IActionResult UpdateTraining([FromBody] TrainingUpdateRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                bool updateSuccess = _feTrainingsService.UpdateTraining(obj);
                if (updateSuccess)
                {
                    result.Data = obj;
                    result.Success = true;
                    result.Message = "Training updated successfully.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Something went wrong, please try again.";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> Update trainings -> Error -> {exc.Message}");
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

        [HttpPost("getCustomerCompletedTrainingsDetail")]
        public IActionResult GetCustomerCompletedTrainingDetail([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingsService.GetCustomerCompletedTrainingDetail(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingDetails -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("getCustomerTrainingTestimonials")]
        public IActionResult GetCustomerTrainingTestimonials([FromBody] TrainingFeedBackRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feTrainingFeedBackService.GetCustomerFeedBack(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingDetails -> Error -> {exc.Message}");
            }
            return Json(result);
        }
        [HttpPost("getTestimonials")]
        public IActionResult GetTestimonials([FromBody] AgencyDTO agency)
        {
            Result result = new Result(true);
            try
            {
                result.Message = "Testimonials fetch Successfully";
                result.Data = _feTrainingsService.GetTestimonials(agencyId:agency);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> GetTestimonials -> Error -> {exc.Message}");
            }
            return Ok(result);
        }

        [HttpPost("getTestimonialDetail")]
        public IActionResult GetTestimonialDetails([FromBody] TrainingRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Message = "Testimonials fetch successfully";
                result.Data = _feTrainingsService.GetTestimonialsDetail(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;

                _logging.Fatal($"Method : api/trainings -> GetTestimonialsDetail -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("addReply")]
        public IActionResult AddReply([FromBody] FeedbackReplyDTO obj)
        {
            Result result = new Result(true);
            try
            {
                FeedbackReplyDTO pd = _feTrainingsService.AddReply(obj);
                if (pd != null)
                {
                  
                    result.Success = true;
                    result.Message = "Feedback Reply saved successfully.";
                }
                else
                {
                    result.Success = true;
                    result.Message = "Something went wrong, please try again.";
                }

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> Feedback Reply -> Error -> {exc.Message}");
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

        [HttpPost("deleteFeedback")]
        public IActionResult DeleteFeedback([FromBody] FeedbackReplyDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = _feTrainingFeedBackService.DeleteFeedback(obj);

                if (result.Success)
                {
                    result.Message = "Comment deleted successfully";
                    return Ok(result);  // Return 200 (OK) with the result
                }
                else
                {
                    result.Message = "Failed to delete comment";  // Custom failure message
                    return BadRequest(new { message = result.Message });  // Return 400 (Bad Request)
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/Comment -> Delete -> Error -> {exc.Message}");
                return StatusCode(500, new { message = "Internal Server Error", details = exc.Message });  // Return 500 for unexpected errors
            }
        }

    
}


}
