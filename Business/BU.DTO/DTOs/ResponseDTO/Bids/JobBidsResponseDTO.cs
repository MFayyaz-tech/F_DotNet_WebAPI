using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.ResponseDTO.Bids
{
    public class JobBidsResponseDTO : BaseDTO
    {
        public long BidId { get; set; }
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public long JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal JobLat { get; set; }
        public decimal JobLng { get; set; }
        public DateTime JobFromDate { get; set; }
        public DateTime JobToDate { get; set; }
        public string JobAssignmentStatus { get; set; }
        public decimal BidAmount { get; set; }
        public decimal AverageBidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public string BidType { get; set; } // hourly,fixed
        public string BidNotes { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
