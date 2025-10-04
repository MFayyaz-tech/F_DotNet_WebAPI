using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.DTO.DTOs.ResponseDTO.Bids;
using BU.DTO.DTOs.ResponseDTO.Job;
using Common.Helper;
using DAO.DAO.User;
using DTO.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Jobs
{
    public interface IFeJobsService
    {
        FeJobsDTO Add(FeJobsDTO obj);
        List<FeJobsDTO> GetList();
        bool Delete(FeJobsDTO obj);
        bool Update(FeJobsDTO obj);
        bool UpdateJob(UpdateJobRequestDTO obj);
        FeJobsDTO GetJobsById(UpdateJobRequestDTO obj);
        IEnumerable<FeJobsDTO> GetCustomerJobs(UpdateJobRequestDTO obj);
        IEnumerable<FeGetAssignJobDTO> GetAgencyAssignJobs(FeJobBidDTO obj);
        IEnumerable<JobHistoryDTO> GetJobHistory(UpdateJobRequestDTO obj);
        IEnumerable<FeJobsDTO> loadGrid(string[] parameters);
        FeJobsDTO Get(long id);
        FeJobBidDTO BidAJob(FeJobBidDTO obj);
        bool DeleteBidOnJob(FeJobBidDTO obj);
        FeJobBidDTO CheckIfAgencyAlreadyBidOnJob(FeJobBidDTO obj);
        IEnumerable<FeJobsDTO> LoadOpenJobs();
        IEnumerable<FeJobsDTO> LoadCustomerOpenJobs(FeJobsDTO obj);
        IEnumerable<JobResponseDTO> LoadCustomerActiveJobs(JobRequestDTO obj);
        JobResponseDTO LoadCustomerActiveJobsDetails(JobRequestDTO obj);
        

        IEnumerable<FeJobsDTO> LoadCustomerAllJobs(FeJobsDTO obj);
        IEnumerable<JobBidsListResponseDTO> LoadJobBids(FeJobsDTO obj);
        HireAgencyRequestDTO HireAgency(HireAgencyRequestDTO obj);
        Result AgencyStartJobContract(AgencyRequestDTO obj);
        Result AgencyUnAssignJob(AgencyRequestDTO obj);
        FeJobContractDTO AgencyDeliverTheJob(AgencyRequestDTO obj);
        FeJobContractDTO CustomerCompleteTheJob(AgencyRequestDTO obj);
        CancelJobContractRequestDTO CancelJob(CancelJobContractRequestDTO obj);
        IEnumerable<AgencyBidsResponseDTO> LoadAgencyBids(AgencyRequestDTO obj);
    }
}
