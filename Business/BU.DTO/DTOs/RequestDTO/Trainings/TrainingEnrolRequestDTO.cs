using DTO.DTOs.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Trainings
{
    public class TrainingEnrolRequestDTO : BaseDTO
    {
        public long EnrollmentId { get; set; }
        public long TrainingId { get; set; }
        public long CustomerId { get; set; }
        public long AgencyId { get; set; }
        public string EnrollmentStatus { get; set; }
        public string RejectionReason { get; set; }
        public List<MediaRequestDTO> MediaFiles { get; set; }

    }

    public class MediaRequestDTO
    {
        public long EnrollmentId { get; set; }
        public string MediaCategory { get; set; }
        public IFormFile MediaFile { get; set; }
    }
    
}
