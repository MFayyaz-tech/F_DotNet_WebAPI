
using BU.DTO.DTOs.Jobs;
using System.Collections.Generic;
using BU.DTO.DTOs.ResponseDTO.Job;

namespace BU.Services.IServices.Jobs
{
    public interface IFeAgentsServices
    {
        AgentsDTO Add(AgentsDTO obj);
        IEnumerable<AgentsDTO> getAgents(AgentsDTO obj);
        FeAgentsDetailDTO getAgentDetail(AgentsDTO obj);


    }
}

