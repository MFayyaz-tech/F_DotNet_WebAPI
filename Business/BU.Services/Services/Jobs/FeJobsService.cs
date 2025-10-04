using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.DTO.DTOs.ResponseDTO.Bids;
using BU.DTO.DTOs.ResponseDTO.Job;
using BU.Services.IServices.Jobs;
using Common;
using Common.Helper;
using DA.DAO.DAO.Jobs;
using DA.Entities.Jobs;
using DAO;
using IN.Common.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BU.Services.Services.Jobs
{
    public class FeJobsService : IFeJobsService
    {
        private readonly IRepository<Fe_jobs> _FeJobsRepository;
        private readonly IRepository<Fe_job_bid> _FeJobBidRepository;
        private readonly IRepository<Fe_job_contract> _FeJobContractRepository;
        private readonly IFeJobContractService _FeJobContractService;
        private readonly IFeJobContractProgressService _FeJobContractProgressService;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FeJobsService(IRepository<Fe_jobs> FeJobsRepository, 
            IRepository<Fe_job_bid> FeJobBidRepository,
            IRepository<Fe_job_contract> FeJobContractRepository,
            IFeJobContractService FeJobContractService,
            IFeJobContractProgressService FeJobContractProgressService, 
            IMapper mapper, IConfiguration configuration)
        {
            _FeJobsRepository = FeJobsRepository;
            _FeJobBidRepository = FeJobBidRepository;
            _FeJobContractRepository = FeJobContractRepository;
            _FeJobContractService = FeJobContractService;
            _FeJobContractProgressService = FeJobContractProgressService;
            _configuration = configuration;
            _mapper = mapper;

        }
        public FeJobsDTO Add(FeJobsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            obj.JobStatus = JobStatus.Open.ToString();
            obj.IsActive = true;
            Fe_jobs ent = _mapper.Map<FeJobsDTO,Fe_jobs>(obj);
            obj.JobId = _FeJobsRepository.Insert(ent);
            return obj;
        }

        public bool Delete(FeJobsDTO obj)
        {
            long userId = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                userId = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            var job = _FeJobsRepository.Get(Database.MAIN, obj.JobId);
            job.Updated_by = userId;
            job.Is_deleted = true;
            return _FeJobsRepository.Update(job);
        }

        public FeJobsDTO Get(long id)
        {
            var job = _FeJobsRepository.Get(Database.MAIN, id);
            return _mapper.Map<Fe_jobs, FeJobsDTO>(job);
        }

        public List<FeJobsDTO> GetList()
        {
            var jobs = _FeJobsRepository.GetAll(Database.MAIN, new string[] { }).ToList();
            return _mapper.Map<List<Fe_jobs>, List<FeJobsDTO>>(jobs);
        }

        public IEnumerable<FeJobsDTO> loadGrid(string[] parameters)
        {
            var jobs = _FeJobsRepository.GetList(Common.Database.MAIN, FeJobsDAO.GetJobsListQuery);
            return _mapper.Map<IEnumerable<Fe_jobs>, IEnumerable<FeJobsDTO>>(jobs);
        }

        public bool Update(FeJobsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Fe_jobs ent = _mapper.Map<FeJobsDTO, Fe_jobs>(obj);
            return _FeJobsRepository.Update(ent);
        }

        public FeJobBidDTO BidAJob(FeJobBidDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Fe_job_bid ent = _mapper.Map<FeJobBidDTO, Fe_job_bid>(obj);
            obj.BidId =  _FeJobBidRepository.Insert(ent);
            return obj;
        }

        public bool DeleteBidOnJob(FeJobBidDTO obj)
        {
            Fe_job_bid ent = _FeJobBidRepository.GetList(Database.MAIN, FeJobBidDAO.GetBidByIdQuery, new { BidId = obj.BidId }).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                ent.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            ent.Is_deleted = true;
            _FeJobBidRepository.Update(ent);
            return true;
        }

        public FeJobBidDTO CheckIfAgencyAlreadyBidOnJob(FeJobBidDTO obj)
        {
            var jobBid = _FeJobBidRepository.GetList(Database.MAIN, FeJobBidDAO.GetIfAgencyAlreadyBidOnJobQuery, new { AgencyId = obj.AgencyId, JobId = obj.JobId }).FirstOrDefault();
            return _mapper.Map<Fe_job_bid, FeJobBidDTO>(jobBid);
        }

         public IEnumerable<FeJobsDTO> LoadOpenJobs()
        {
            var jobList = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetOpenJobsQuery);
            return _mapper.Map<IEnumerable<Fe_jobs>, IEnumerable<FeJobsDTO>>(jobList);
        }
        public IEnumerable<FeJobsDTO> LoadCustomerOpenJobs(FeJobsDTO obj)
        {
            var jobList = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetCustomerOpenJobsQuery, new { CustomerId = obj.CustomerId });
            return _mapper.Map<IEnumerable<Fe_jobs>, IEnumerable<FeJobsDTO>>(jobList);
        }

        public IEnumerable<FeJobsDTO> LoadCustomerAllJobs(FeJobsDTO obj)
        {
            var jobList = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetCustomerAllJobsQuery, new { CustomerId = obj.CustomerId });
            return _mapper.Map<IEnumerable<Fe_jobs>, IEnumerable<FeJobsDTO>>(jobList);
        }

        // load job all bids
        public IEnumerable<JobBidsListResponseDTO> LoadJobBids(FeJobsDTO obj)
        {
            var list = _FeJobBidRepository.GetList(Database.MAIN, FeJobBidDAO.GetJobBidsListQuery, new { JobId = obj.JobId });
            return _mapper.Map<IEnumerable<Fe_job_bid>, IEnumerable<JobBidsListResponseDTO>>(list);
        }

        // reward a job to agency
        public HireAgencyRequestDTO HireAgency(HireAgencyRequestDTO obj)
        {
            var job = _FeJobsRepository.Get(Database.MAIN, obj.JobId);
            job.Job_status = JobStatus.Rewarded.ToString();
            _FeJobContractService.CreateJobContract(new FeJobContractDTO { 
                JobId = obj.JobId,
                AgencyId = obj.AgencyId,
                BidId = obj.BidId
            });
            _FeJobsRepository.Update(job);
            return obj;
        }

        //agency start the job and start working on job
        //public Result AgencyStartJobContract(AgencyRequestDTO obj)
        //{
        //    Result result = new Result(false);
        //    long updatedBy = 0;
        //    if (!string.IsNullOrWhiteSpace(obj.EncUserID))
        //    {
        //        updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
        //    }
        //    var jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
        //    var job = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = jobContract.Job_id }).FirstOrDefault();

        //    job.Job_status = JobStatus.InProgress.ToString();
        //    job.Updated_by = updatedBy;
        //    _FeJobsRepository.Update(job);

        //    jobContract.Contract_status = JobStatus.InProgress.ToString();
        //    jobContract.Updated_by = updatedBy;
        //    _FeJobContractRepository.Update(jobContract);
        //    result.Success = true;
        //    result.Message = "Job started successfully.";
        //    return result;
        //}

       

        public Result AgencyStartJobContract(AgencyRequestDTO obj)
        {
            Result result = new Result(false);
            long updatedBy = 0;

            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

           
            var jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
            var job = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = jobContract.Job_id }).FirstOrDefault();

         
            job.Job_status = JobStatus.InProgress.ToString();  
            job.Updated_by = updatedBy;
          
            
            _FeJobsRepository.Update(job);

            jobContract.Contract_status = JobStatus.InProgress.ToString();
            jobContract.Updated_by = updatedBy;
            jobContract.Agent_id = obj.agentId;
            _FeJobContractRepository.Update(jobContract);
            
          
            result.Success = true;
            result.Message = "Job assigned to agent successfully.";
            return result;
        }



        public FeJobContractDTO AgencyDeliverTheJob(AgencyRequestDTO obj)
        {
            long updatedBy = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            var jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
            var job = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = jobContract.Job_id }).FirstOrDefault();

            //update job contract
            jobContract.Agency_feedback = obj.FeedBack;
            jobContract.Contract_status = JobStatus.Delivered.ToString();
            jobContract.Contract_progress = obj.ContractProgress;
            jobContract.Updated_by = updatedBy;
            _FeJobContractRepository.Update(jobContract);

            //save job contract progress with notes

            FeJobContractProgressDTO progress = new FeJobContractProgressDTO()
            {
                ContractId = obj.ContractId,
                ContractProgress = obj.ContractProgress,
                ContractStatus = JobStatus.Delivered.ToString(),    
                ContractNotes = obj.FeedBack.ToString()
            };
            _FeJobContractProgressService.SaveJobContractProgress(progress);

            //update job to delivered status
            job.Job_status = JobStatus.Delivered.ToString();
            job.Updated_by = updatedBy;
            _FeJobsRepository.Update(job);

            return _mapper.Map<Fe_job_contract, FeJobContractDTO>(jobContract);
        }

        public FeJobContractDTO CustomerCompleteTheJob(AgencyRequestDTO obj)
        {
            long updatedBy = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            var jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
            var job = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = jobContract.Job_id }).FirstOrDefault();

            //update job contract
            jobContract.Customer_feedback = obj.FeedBack;
            jobContract.Customer_rating = obj.CustomerRating;
            jobContract.Contract_status = JobStatus.Completed.ToString();
            jobContract.Contract_progress = obj.ContractProgress;
            jobContract.Updated_by = updatedBy;
            _FeJobContractRepository.Update(jobContract);

            //save job contract progress with notes

            FeJobContractProgressDTO progress = new FeJobContractProgressDTO()
            {
                ContractId = obj.ContractId,
                ContractProgress = obj.ContractProgress,
                ContractStatus = JobStatus.Completed.ToString(),
                ContractNotes = obj.FeedBack.ToString()
            };
            _FeJobContractProgressService.SaveJobContractProgress(progress);

            //update job to delivered status
            job.Job_status = JobStatus.Completed.ToString();
            job.Updated_by = updatedBy;
            _FeJobsRepository.Update(job);

            return _mapper.Map<Fe_job_contract, FeJobContractDTO>(jobContract);
        }

        public IEnumerable<JobResponseDTO> LoadCustomerActiveJobs(JobRequestDTO obj)
        {
            var jobList =  _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetCustomerActiveJobs, new { CustomerId = obj.CustomerId });
            return _mapper.Map<IEnumerable<Fe_jobs>, IEnumerable<JobResponseDTO>>(jobList);
        }

        public CancelJobContractRequestDTO CancelJob(CancelJobContractRequestDTO obj)
        {
            long updatedBy = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            Fe_job_contract jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
            var job = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = jobContract.Job_id }).FirstOrDefault();

            if (!string.IsNullOrEmpty(obj.AttachmentMediaBase64))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.AttachmentMediaBase64.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                string folderPath = $"\\Documents\\JobContact\\Media\\{jobContract.Contract_id}";
                string fullPath = $"{rootPath}\\Documents\\JobContact\\Media\\{jobContract.Contract_id}";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                jobContract.Attachment_media = Path.Combine(folderPath, fileName);

            }

            //update job contract
            jobContract.Customer_feedback = obj.FeedBack;
            jobContract.Contract_status = JobStatus.Cancelled.ToString();
            jobContract.Customer_rating = obj.CustomerRating;
            jobContract.Cancelation_reason = obj.CancelationReason;
            jobContract.Updated_by = updatedBy;
            _FeJobContractRepository.Update(jobContract);

            //save job contract progress with notes
            FeJobContractProgressDTO progress = new FeJobContractProgressDTO()
            {
                ContractId = obj.ContractId,
                ContractStatus = JobStatus.Cancelled.ToString(),
                ContractNotes = obj.FeedBack.ToString()
            };

            _FeJobContractProgressService.SaveJobContractProgress(progress);

            //update job to delivered status
            job.Job_status = JobStatus.Open.ToString();
            job.Updated_by = updatedBy;
            _FeJobsRepository.Update(job);

            return obj;
        }

        public IEnumerable<AgencyBidsResponseDTO> LoadAgencyBids(AgencyRequestDTO obj)
        {
            var list = _FeJobBidRepository.GetList(Database.MAIN, FeJobBidDAO.GetAgencyBidsQuery, new { AgencyId = obj.AgencyId });
            return _mapper.Map<IEnumerable<Fe_job_bid>, IEnumerable<AgencyBidsResponseDTO>>(list);
        }

        public bool UpdateJob(UpdateJobRequestDTO obj)
        {
            var existingJob = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = obj.JobId }).FirstOrDefault();

            if (existingJob != null)
            {
                // Decrypting EncUserID if provided
                if (!string.IsNullOrWhiteSpace(obj.EncUserID))
                {
                    existingJob.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
                }

                // Assigning correct values from obj to existingJob
                existingJob.Price_min = obj.PriceMin;
                existingJob.Price_max = obj.PriceMax;
                existingJob.Job_title = obj.JobTitle;
                existingJob.Job_description = obj.JobDescription;
                existingJob.Duration_type = obj.DurationType;
                existingJob.Job_category = obj.JobCategory;
                existingJob.From_date = obj.FromDate;
                existingJob.bidder_type = obj.BidderType;
                existingJob.To_date = obj.ToDate;
                existingJob.Lat = obj.Lat;
                existingJob.Lng = obj.Lng;
                existingJob.Price_type = obj.PriceType;

                _FeJobsRepository.Update(existingJob);
                return true; // Indicate that the update was successful
            }
            return false; // Indicate that the update failed
        }


        public FeJobsDTO GetJobsById(UpdateJobRequestDTO obj)
        {
            var jobs = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = obj.JobId }).FirstOrDefault();
            FeJobsDTO response = _mapper.Map<Fe_jobs, FeJobsDTO>(jobs);
             return response;
        }

     

        IEnumerable<FeJobsDTO> IFeJobsService.GetCustomerJobs(UpdateJobRequestDTO obj)
        {
            var jobs = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetCustomerJobs, new { CustomerId = obj.CustomerId }).ToList();
            return _mapper.Map<List<Fe_jobs>, List<FeJobsDTO>>(jobs);
        }

        public IEnumerable<JobHistoryDTO> GetJobHistory(UpdateJobRequestDTO obj)
        {
            var jobs = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetCustomerJobsHistory, new { JobId = obj.JobId }).ToList();
            return _mapper.Map<List<Fe_jobs>, List<JobHistoryDTO>>(jobs);
        }

        public JobResponseDTO LoadCustomerActiveJobsDetails(JobRequestDTO obj)
        {
            var jobList = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetCustomerActiveJobDetail, new { JobId = obj.JobId }).FirstOrDefault();
            return _mapper.Map<Fe_jobs, JobResponseDTO>(jobList);
        }

        public IEnumerable<FeGetAssignJobDTO> GetAgencyAssignJobs(FeJobBidDTO obj)
        {
            var jobList = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetAgencyAssignJobs, new { AgencyId = obj.AgencyId }).ToList();
            return _mapper.Map<List<Fe_jobs>, List<FeGetAssignJobDTO>>(jobList);

        }

        public Result AgencyUnAssignJob(AgencyRequestDTO obj)
        {
            Result result = new Result(false);
            long updatedBy = 0;

            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                updatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }


            var jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
            var job = _FeJobsRepository.GetList(Database.MAIN, FeJobsDAO.GetJobById, new { JobId = jobContract.Job_id }).FirstOrDefault();


            job.Job_status = JobStatus.InProgress.ToString();
            job.Updated_by = updatedBy;


            _FeJobsRepository.Update(job);

            jobContract.Contract_status = JobStatus.InProgress.ToString();
            jobContract.Updated_by = updatedBy;
            jobContract.Agent_id = 0;
            _FeJobContractRepository.Update(jobContract);


            result.Success = true;
            result.Message = "Job unAssign to agent successfully.";
            return result;
        }
    }
}
