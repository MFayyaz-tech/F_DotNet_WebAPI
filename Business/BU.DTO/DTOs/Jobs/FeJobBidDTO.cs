using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Jobs
{
    public class FeJobBidDTO : BaseDTO
    {
        public long BidId { get; set; }
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public long JobId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public string BidType { get; set; } // hourly,fixed
        public string BidNotes { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
