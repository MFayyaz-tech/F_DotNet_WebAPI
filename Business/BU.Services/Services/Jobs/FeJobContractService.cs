using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
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
using System.Linq;
using System.Text;

namespace BU.Services.Services.Jobs
{
    public class FeJobContractService : IFeJobContractService
    {
        private readonly IRepository<Fe_job_contract> _FeJobContractRepository;
        private readonly IRepository<Fe_jobs> _FeJobRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FeJobContractService(IRepository<Fe_job_contract> FeJobContractRepository, IRepository<Fe_jobs> FeJobRepository, IMapper mapper, IConfiguration configuration)
        {
            _FeJobContractRepository = FeJobContractRepository;
            _FeJobRepository = FeJobRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public FeJobContractDTO Add(FeJobContractDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            obj.ContractStatus = JobStatus.Open.ToString();
            obj.IsActive = true;
            Fe_job_contract ent = _mapper.Map<FeJobContractDTO, Fe_job_contract>(obj);
            obj.ContractId = _FeJobContractRepository.Insert(ent);
            return obj;
        }
        public List<FeJobContractDTO> GetList()
        {
            var jobs = _FeJobContractRepository.GetAll(Database.MAIN, new string[] { }).ToList();
            return _mapper.Map<List<Fe_job_contract>, List<FeJobContractDTO>>(jobs);
        }
        public bool Delete(FeJobContractDTO obj)
        {
            long userId = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                userId = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            var job = _FeJobContractRepository.Get(Database.MAIN, obj.JobId);
            job.Updated_by = userId;
            job.Is_deleted = true;
            return _FeJobContractRepository.Update(job);
        }
        public bool Update(FeJobContractDTO obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FeJobContractDTO> loadGrid(string[] parameters)
        {
            var jobContracts = _FeJobContractRepository.GetSearchData(Common.Database.MAIN);
            return _mapper.Map<IEnumerable<Fe_job_contract>, IEnumerable<FeJobContractDTO>>(jobContracts);
        }
        public FeJobContractDTO Get(long id)
        {
            var job = _FeJobContractRepository.Get(Database.MAIN, id);
            return _mapper.Map<Fe_job_contract, FeJobContractDTO>(job);
        }

        public FeJobContractDTO CreateJobContract(FeJobContractDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            obj.ContractStatus = JobStatus.Rewarded.ToString();
            obj.IsActive = true;
            Fe_job_contract ent = _mapper.Map<FeJobContractDTO, Fe_job_contract>(obj);
            obj.ContractId = _FeJobContractRepository.Insert(ent);
            return obj;
        }

        public IEnumerable<JobContractResponseDTO> GetAgencyAwardedJobs(AgencyRequestDTO obj)
        {
            var awardedJobs = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetAgencyRewaredJobsQuery, new { AgencyId = obj.AgencyId });
            return _mapper.Map<IEnumerable<Fe_job_contract>, IEnumerable<JobContractResponseDTO>>(awardedJobs);
        }

        public IEnumerable<JobContractResponseDTO> GetAgencyJobContracts(AgencyRequestDTO obj)
        {
            var awardedJobs = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetAgencyJobContracts, new { AgencyId = obj.AgencyId,ContractStatus = obj.ContractStatus });
            return _mapper.Map<IEnumerable<Fe_job_contract>, IEnumerable<JobContractResponseDTO>>(awardedJobs);
        }
    }
}
