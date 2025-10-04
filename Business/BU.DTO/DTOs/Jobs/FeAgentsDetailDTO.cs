using System;
using BU.DTO.DTOs.ResponseDTO.Testimonials;
using System.Collections.Generic;

namespace BU.DTO.DTOs.Jobs
{
	public class FeAgentsDetailDTO
	
        {
        public long AgentId { get; set; }
        public long AgencyId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public string Experince { get; set; }
        public string Introduction { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string PhotoPath { get; set; }
        public string EmailAddress { get; set; }
        public string TotalCompletedJob { get; set; }
        public string InProgressJob { get; set; }
        public string CancelledJob { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<FeAgentsReviewDTO> Review { get; set; }
        public IEnumerable<FeJobsDTO> jobs { get; set; }

    }
}

public class FeAgentsReviewDTO

{
    public long ContractId { get; set; }
    public long AgencyId { get; set; }
    public long AgentId { get; set; }
    public long JobId { get; set; }
    public string ContractStatus { get; set; }
    public string CustomerFeedback { get; set; }
    public decimal CustomerRating { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public long BidId { get; set; }
    public string JobTitle { get; set; }
    public string JobDescription { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhoto { get; set; }
    public string JobStatus { get; set; }
    public string DurationType { get; set; }
    public DateTime ToDate { get; set; }
    public DateTime FromDate { get; set; }
    public string JobCategory { get; set; }
    public string  PriceType { get; set; }
    public decimal PriceMin { get; set; }
    public decimal PriceMax { get; set; }

}