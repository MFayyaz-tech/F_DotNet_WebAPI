using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Job
{
    public class HireAgencyRequestDTO
    {
        public long JobId { get; set; }
        public long AgencyId { get; set; }
        public long BidId { get; set; }
    }
}
