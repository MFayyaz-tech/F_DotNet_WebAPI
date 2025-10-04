using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Jobs
{
    public class FeJobContractDTO : BaseDTO
    {
        public long ContractId { get; set; }
        public long JobId { get; set; }
        public long AgencyId { get; set; }
        public long BidId { get; set; }
        public string ContractStatus { get; set; }
        public decimal ContractProgress { get; set; }
        public string AgencyFeedback { get; set; }
        public int AgencyRating { get; set; }
        public string CustomerFeedback { get; set; }
        public int CustomerRating { get; set; }
        public string AttachmentMedia { get; set; }
        public string CancelationReason { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
