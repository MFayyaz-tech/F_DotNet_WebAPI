using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using BU.Services.IServices.Jobs;
using Common;
using Common.Helper;
using DA.DAO.DAO.Jobs;
using DA.Entities.Jobs;
using DAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BU.Services.Services.Jobs
{
    public class FeJobContractProgressService : IFeJobContractProgressService
    {
        private readonly IRepository<Fe_job_contract_progress> _FeJobContractProgressRepository;
        private readonly IRepository<Fe_job_contract> _FeJobContractRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FeJobContractProgressService(IRepository<Fe_job_contract_progress> FeJobContractProgressRepository,
            IRepository<Fe_job_contract> FeJobContractRepository,
            IMapper mapper, IConfiguration configuration)
        {
            _FeJobContractProgressRepository = FeJobContractProgressRepository;
            _FeJobContractRepository = FeJobContractRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public FeJobContractProgressDTO Add(FeJobContractProgressDTO obj)
        {
            throw new NotImplementedException();
        }
        public List<FeJobContractProgressDTO> GetList()
        {
            throw new NotImplementedException();
        }
        public bool Delete(FeJobContractProgressDTO obj)
        {
            throw new NotImplementedException();
        }
        public bool Update(FeJobContractProgressDTO obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FeJobContractProgressDTO> loadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }
        public FeJobContractProgressDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public FeJobContractProgressDTO SaveJobContractProgress(FeJobContractProgressDTO obj)
        {
            long actorId = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                actorId = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            //save job progress
            Fe_job_contract_progress ent = _mapper.Map<FeJobContractProgressDTO, Fe_job_contract_progress>(obj);
            ent.Contract_status = obj.ContractStatus;
            ent.Contract_notes = obj.ContractNotes;
            ent.Created_by = actorId;
            _FeJobContractProgressRepository.Insert(ent);
            
            //update latest contract progress
            var jobContract = _FeJobContractRepository.GetList(Database.MAIN, FeJobContractDAO.GetJobContractByContractId, new { ContractId = obj.ContractId }).FirstOrDefault();
            jobContract.Contract_progress = obj.ContractProgress;
            jobContract.Contract_status = obj.ContractStatus;
            jobContract.Updated_by = actorId;
            _FeJobContractRepository.Update(jobContract);
            return obj;

        }
    }
}
