using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Jobs
{
    public class JobBidsListResponseDTO
    {
        public long BidId { get; set; }
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public decimal AgencyLat { get; set; }
        public decimal AgencyLng { get; set; }
        public long JobId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public string BidType { get; set; }
        public string BidNotes { get; set; }
        public bool IsActive { get; set; }
        public string PhotoPath { get; set; }
        public string AgencyProfile { get; set; }
    }
}
