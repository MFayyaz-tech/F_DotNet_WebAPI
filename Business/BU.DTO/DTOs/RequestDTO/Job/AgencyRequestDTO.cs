using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Job
{
    public class AgencyRequestDTO : BaseDTO
    {
        public long AgencyId { get; set; }
        public long ContractId { get; set; }
        public string ContractStatus { get; set; }
        public int ContractProgress { get; set; }
        public string FeedBack { get; set; }
        public int CustomerRating { get; set; }
        public long CustomerBonus { get; set; }
        public string FileBase64 { get; set; }
        public long agentId { get; set; }

    }
}
