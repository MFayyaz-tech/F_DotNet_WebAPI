using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.RequestDTO.Job
{
	public class UpdateJobRequestDTO : BaseDTO
    {

        public long JobId { get; set; }
        public long CustomerId { get; set; }
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
        public string JobCategory { get; set; }

    }
}

