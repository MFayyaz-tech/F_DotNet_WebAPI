using System;
using System.IO;
using BU.DTO.DTOs.ResponseDTO.Job;
using BU.Services.IServices.Jobs;
using Common.Helper;
using DA.Entities.Jobs;
using DAO;
using Entities.Users;
using Microsoft.Extensions.Configuration;

using AutoMapper;
using System.Collections.Generic;
using Common;
using DA.DAO.DAO.Jobs;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.ResponseDTO.Testimonials;
using DA.DAO.DAO.Trainings;
using DA.Entities.Trainings;
using System.Linq;
using System.Collections;

namespace BU.Services.Services.Jobs
{
    public class FeAgentServices : IFeAgentsServices
    {
        private readonly IRepository<Fe_agent> _Agentrepository;
        private readonly IRepository<Fe_jobs> _JobRepositoruy;
        private readonly IRepository<Fe_job_contract> _ContractRepository;
        private readonly IRepository<Fe_users> _UserRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FeAgentServices(IRepository<Fe_agent> Agentrepository, IRepository<Fe_jobs> JobRepositoruy, IRepository<Fe_job_contract> ContractRepository, IRepository<Fe_users> UserRepository, IMapper mapper, IConfiguration configuration)
        {
            _ContractRepository = ContractRepository;
            _JobRepositoruy = JobRepositoruy;
            _Agentrepository = Agentrepository;
            _UserRepository = UserRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public AgentsDTO Add(AgentsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            long nextIdentity = _Agentrepository.GetNextIdentityId("fe_agent");
            string randomPassword = "welcome";
            if (!string.IsNullOrEmpty(obj.Base64Image))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                string folderPath = $"\\Documents\\Trainers\\{nextIdentity}";
                string fullPath = $"{rootPath}\\Documents\\Trainers\\{nextIdentity}";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                obj.PhotoPath = Path.Combine(folderPath, fileName);
            }

            Fe_users userRecord = _mapper.Map<AgentsDTO, Fe_users>(obj);
            userRecord.User_name = obj.FirstName + " " + obj.LastName;
            userRecord.User_type = "Agent";
            userRecord.Approval_status = "Approved";
            userRecord.Password = CryptoEngine.Encrypt(randomPassword);
            userRecord.Is_active = true;
            userRecord.Last_login_date = null;
            userRecord.User_id = _UserRepository.Insert(userRecord);

            // save customer record in fe_customer
            Fe_agent agent = _mapper.Map<AgentsDTO, Fe_agent>(obj);

            //agent.User_id = userRecord.User_id;
            long agentId = _Agentrepository.Insert(agent);
            return obj;
        }

        public IEnumerable<AgentsDTO> getAgents(AgentsDTO obj)
        {
            {
                var awardedJobs = _Agentrepository.GetList(Database.MAIN, FeAgentsDAO.GetAllAgents, new { AgencyId = obj.AgencyId });
                return _mapper.Map<IEnumerable<Fe_agent>, IEnumerable<AgentsDTO>>(awardedJobs);
            }
        }

        public FeAgentsDetailDTO getAgentDetail(AgentsDTO obj)
        {
            // Fetch the main agent details
            var agent = _Agentrepository.GetList(Database.MAIN, FeAgentsDAO.getAgentDetail, new { obj.AgentId }).FirstOrDefault();
            if (agent == null)
            {
                throw new InvalidOperationException("Agent not found.");
            }

            FeAgentsDetailDTO response = _mapper.Map<Fe_agent, FeAgentsDetailDTO>(agent);

            var reviewList = _ContractRepository.GetList(Database.MAIN, FeAgentsDAO.GetAgentReviews, new { obj.AgentId }).ToList();

            response.Review = _mapper.Map<IEnumerable<Fe_job_contract>, IEnumerable<FeAgentsReviewDTO>>(reviewList);

            var jobs = _JobRepositoruy.GetList(Database.MAIN, FeAgentsDAO.GetAgentJobs, new { obj.AgentId }).ToList();

            response.jobs = _mapper.Map<IEnumerable<Fe_jobs>, IEnumerable<FeJobsDTO>>(jobs);


            return response;
        }


    }
}

