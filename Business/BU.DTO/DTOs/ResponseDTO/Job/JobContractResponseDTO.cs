using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.ResponseDTO.Job
{
    public class JobContractResponseDTO
    {
        public long ContractId { get; set; }
        public long JobId { get; set; }
        public string JobTitle { get; set; }
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string ContractStatus { get; set; }
        public string ContractProgress { get; set; }
        public string DurationType { get; set; }
        public string BidderType { get; set; }
        public string PriceType { get; set; }
        public string FromDate { get; set; }
        public string PhotoPath { get; set; }
        public string ToDate { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public long CustomerId { get; set; }


    }
}
