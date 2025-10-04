using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.ResponseDTO.Job
{
	public class JobHistoryDTO : BaseDTO
	{
        public long ContractProgressId { get; set; }
        public long ContractId { get; set; }
        public long ContractProgress { get; set; }//One-time,Date-specific
        public string JobStatus { get; set; }
        public string ContractNote { get; set; }
        public string JobTitle { get; set; }

    }
}

