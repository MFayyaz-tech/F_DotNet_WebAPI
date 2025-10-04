using BU.DTO.DTOs.Jobs;
using BU.Services.IServices.Jobs;
using Common.Helper;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using WebRAPI.Base;
using Microsoft.AspNetCore.Authorization;
using DTO.DTOs.User;
using BU.DTO.DTOs.Customer;
using BU.Services.Services.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.DTO.DTOs.ResponseDTO.Job;
using BU.DTO.DTOs.RequestDTO.Trainings;

namespace WebAPI.Controllers.Jobs
{
    [Route("api/jobs")]
    [ApiController]
    //[Authorize]
    public class JobsController : BaseController
    {
        private readonly IFeJobsService _feJobsService;
        private readonly IFeJobContractService _feJobsContractsService;
        private readonly ILogging _logging;
        IConfiguration _configuration;
        public JobsController(IFeJobsService feJobsService, IFeJobContractService feJobsContractsService, ILogging logging, IConfiguration configuration) : base(logging, configuration)
        {
            _feJobsService = feJobsService;
            _feJobsContractsService = feJobsContractsService;
            _logging = logging;
            _configuration = configuration;
        }
        [HttpPost("loadGrid")]
        public IActionResult LoadGrid()
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<FeJobsDTO> data = _feJobsService.loadGrid(new string[] { });
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
        public IActionResult Add([FromBody] FeJobsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                FeJobsDTO pd = _feJobsService.Add(obj);
                if(pd != null)
                {
                    result.Data = pd;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Failed to add job";
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
        [HttpPost("get")]
        public IActionResult Get([FromBody] long id)
        {
            Result result = new Result(true);
            try
            {
                FeJobsDTO data = _feJobsService.Get(id);
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> Get -> Error -> {exc.Message}");
            }
            return Json(result);

        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] FeJobsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = _feJobsService.Update(obj);
                if(result.Success)
                {
                    result.Data = _feJobsService.Get(obj.JobId);
                    result.Message = "Job updated successfully";
                }else
                {
                    result.Message = "Failed to update job";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> Update -> Error -> {exc.Message}");
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
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] FeJobsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Success = _feJobsService.Delete(obj);
                result.Message = "Job deleted successfully";
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> Delete -> Error -> {exc.Message}");
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

        [HttpPost("bidJob")]
        public IActionResult BidJob([FromBody] FeJobBidDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if(_feJobsService.CheckIfAgencyAlreadyBidOnJob(obj) != null)
                {
                    result.Message = "You have already bid on this job.";
                    result.Success = false;
                }
                else
                {
                    if (_feJobsService.BidAJob(obj) != null)
                    {
                        result.Message = "Bid placed successfully.";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "Failed to placed bid on job, please try again.";
                        result.Success = false;
                    }
                }
               
                
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> BidJob -> Error -> {exc.Message}");
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

        [HttpPost("deleteBidOnJob")]
        public IActionResult DeleteBidOnJob([FromBody] FeJobBidDTO obj)
        {
            Result result = new Result(true);
            try
            {
                if (_feJobsService.DeleteBidOnJob(obj) == true)
                {
                    result.Message = "Bid deleted successfully.";
                    result.Success = true;
                }
                else
                {
                    result.Message = "Failed to delete bid, please try again.";
                    result.Success = false;
                }

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> DeleteBidOnJob -> Error -> {exc.Message}");
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

        

        [HttpGet("loadOpenJobs")]
        public IActionResult LoadOpenJobs()
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.LoadOpenJobs();

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> LoadOpenJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("loadCustomerOpenJobs")]
        public IActionResult LoadCustomerOpenJobs([FromBody] FeJobsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.LoadCustomerOpenJobs(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> LoadCustomerOpenJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("loadCustomerActiveJobs")]
        public IActionResult LoadCustomerActiveJobs([FromBody] JobRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.LoadCustomerActiveJobs(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> LoadCustomerActiveJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("loadCustomerActiveJobsDetails")]
        public IActionResult LoadCustomerActiveJobsDetails([FromBody] JobRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.LoadCustomerActiveJobsDetails(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> LoadCustomerActiveJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("loadCustomerAllJobs")]
        public IActionResult LoadCustomerAllJobs([FromBody] FeJobsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.LoadCustomerAllJobs(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> LoadCustomerAllJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("loadJobBids")]
        public IActionResult LoadJobBids([FromBody] FeJobsDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.LoadJobBids(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> LoadJobBids -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("hireAgency")]
        public IActionResult RewardJob([FromBody] HireAgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                
                result.Data = _feJobsService.HireAgency(obj);
                if(result.Data != null)
                {
                    result.Message = "Agency hired successfully.";
                }
                else
                {
                    result.Message = "Failed to hire agency.";
                    result.Success = false;
                }

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> RewardJob -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("getAgencyJobContracts")]
        public IActionResult GetAgencyRewardedJobs([FromBody] AgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _feJobsContractsService.GetAgencyJobContracts(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> GetAgencyRewardedJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }

        [HttpPost("agencyStartJob")]
        public IActionResult AgencyStartJob([FromBody] AgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result = _feJobsService.AgencyStartJobContract(obj);
                result.Message = "Agency started the job.";

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> AgencyStartJob -> Error -> {exc.Message}");
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

        [HttpPost("agencyDeliverTheJob")]
        public IActionResult AgencyDeliverTheJob([FromBody] AgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _feJobsService.AgencyDeliverTheJob(obj);
                result.Message = "Agency delivered the job successfully.";

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> AgencyDeliverTheJob -> Error -> {exc.Message}");
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

        [HttpPost("customerCompleteTheJob")]
        public IActionResult CustomerCompleteTheJob([FromBody] AgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _feJobsService.CustomerCompleteTheJob(obj);
                result.Message = "Agency completed the job successfully.";

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> CustomerCompleteTheJob -> Error -> {exc.Message}");
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

        [HttpPost("cancelJob")]
        public IActionResult CancelJob([FromBody] CancelJobContractRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _feJobsService.CancelJob(obj);
                result.Message = "Agency canceled successfully.";

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

        [HttpPost("loadAgencyBids")]
        public IActionResult CancelJob([FromBody] AgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result.Data = _feJobsService.LoadAgencyBids(obj);

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Json(result);
        }

        [HttpPut("updateCustomerJob")]
        public IActionResult UpdateJob([FromBody] UpdateJobRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                bool updateSuccess = _feJobsService.UpdateJob(obj);
                if (updateSuccess)
                {
                    result.Data = obj;
                    result.Success = true;
                    result.Message = "Job updated successfully.";
                }
                else 
                {
                    result.Success = false;
                    result.Message = "Failed to update job";
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> Update -> Error -> {exc.Message}");
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

        [HttpPost("getJobById")]
        public IActionResult GetJobById([FromBody] UpdateJobRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _feJobsService.GetJobsById(obj);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/trainings -> GetTrainingDetails -> Error -> {exc.Message}");
            }
            return Json(result);
        }

    


        [HttpPost("getCustomerJobs")]
        public IActionResult GetCustomerJobs([FromBody] UpdateJobRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<FeJobsDTO> data = _feJobsService.GetCustomerJobs(obj);

                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> GetCustomerJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("getJobHistory")]
        public IActionResult GetJobHistory([FromBody] UpdateJobRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<JobHistoryDTO> data = _feJobsService.GetJobHistory(obj);

                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> GetCustomerJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }


        [HttpPost("getAgencyAssignJobs")]
        public IActionResult GetAgencyAssignJobs([FromBody] FeJobBidDTO obj)
        {
            Result result = new Result(true);
            try
            {
                IEnumerable<FeGetAssignJobDTO> data = _feJobsService.GetAgencyAssignJobs(obj);
                result.Message = "Agency Assign job fetch successfully";
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> getAgencyAssignJobs -> Error -> {exc.Message}");
            }
            return Json(result);
        }



        [HttpPost("unAssignJob")]
        public IActionResult UnAssignJob([FromBody] AgencyRequestDTO obj)
        {
            Result result = new Result(true);
            try
            {

                result = _feJobsService.AgencyUnAssignJob(obj);
                result.Message = "Agency unAssign the job.";

            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal($"Method : api/jobs -> UnAssignJob -> Error -> {exc.Message}");
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


        //customer can cancel the job if its is in open state
        //agency accept the job and start working
        //agency deliver the job  and add comments
        //customer completed the job and add comments
    }
}
