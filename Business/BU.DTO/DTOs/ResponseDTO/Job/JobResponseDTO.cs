using Dapper.Contrib.Extensions;
using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.ResponseDTO.Job
{
    public class JobResponseDTO
    {
        public long JobId { get; set; }
        public string JobTitle { get; set; }
        public string DurationType { get; set; }//One-time,Date-specific
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string JobDescription { get; set; }
        public string JobStatus { get; set; }
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencyPhone { get; set; }
        public string AgencyProfileImage { get; set; }
        public long ContractId { get; set; }
        public int ContractProgress { get; set; }
        public int CustomerId { get; set; }
        public decimal ContractPrice{ get; set; }
        public string JobCategory { get; set; }
        public string PhotoPath { get; set; }
        public decimal AgencyRating { get; set; }
    }
}
