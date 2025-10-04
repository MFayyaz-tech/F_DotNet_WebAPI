using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Jobs
{
    public class FeJobContractProgressDTO : BaseDTO
    {
        public long ContractProgressId { get; set; }
        public long ContractId { get; set; }
        public int ContractProgress { get; set; }
        public string ContractStatus { get; set; }
        public string ContractNotes { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
