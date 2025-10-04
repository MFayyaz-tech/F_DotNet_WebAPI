using System;
namespace BU.DTO.DTOs.Jobs
{
	public class FeGetAssignJobDTO
    {
        public long JobId { get; set; }
        public long CustomerId { get; set; }
        public long AgentId { get; set; }
        public long AgencyId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string AgentName { get; set; }
        public string ContractStatus { get; set; }
        public int ContractProgress { get; set; }
        public string AgencyName { get; set; }
        public string CustomerProfile { get; set; }
        public long ContractId { get; set; }
        public string ContractNotes { get; set; }


        public string JobTitle { get; set; }
        public string PriceType { get; set; }//Open,Range
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public string DurationType { get; set; }//One-time,Date-specific
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string BidderType { get; set; }//Any-One,Licensed,Professional-Company
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string JobDescription { get; set; }
        public string JobStatus { get; set; }//Open
        public long JobBidCount { get; set; }
        public decimal AverageBidAmount { get; set; }
        public string JobCategory { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


    }
}

