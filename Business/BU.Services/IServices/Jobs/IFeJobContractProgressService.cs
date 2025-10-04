using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Jobs
{
    public interface IFeJobContractProgressService
    {
        FeJobContractProgressDTO Add(FeJobContractProgressDTO obj);
        List<FeJobContractProgressDTO> GetList();
        bool Delete(FeJobContractProgressDTO obj);
        bool Update(FeJobContractProgressDTO obj);
        IEnumerable<FeJobContractProgressDTO> loadGrid(string[] parameters);
        FeJobContractProgressDTO Get(long id);
        FeJobContractProgressDTO SaveJobContractProgress(FeJobContractProgressDTO obj);
    }
}
