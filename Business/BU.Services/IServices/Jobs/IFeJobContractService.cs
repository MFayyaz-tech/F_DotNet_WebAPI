using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.DTO.DTOs.ResponseDTO.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Jobs
{
    public interface IFeJobContractService
    {
        FeJobContractDTO Add(FeJobContractDTO obj);
        FeJobContractDTO CreateJobContract(FeJobContractDTO obj);
        List<FeJobContractDTO> GetList();
        bool Delete(FeJobContractDTO obj);
        bool Update(FeJobContractDTO obj);
        IEnumerable<FeJobContractDTO> loadGrid(string[] parameters);
        IEnumerable<JobContractResponseDTO> GetAgencyAwardedJobs(AgencyRequestDTO obj);
        IEnumerable<JobContractResponseDTO> GetAgencyJobContracts(AgencyRequestDTO obj);
        FeJobContractDTO Get(long id);
    }
}
