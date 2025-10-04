using BU.DTO.DTOs.Trainings;
using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.ResponseDTO.Trainings
{
    public class TrainingEnrollmentsResponseDTO : BaseDTO
    {
        public long TrainingId { get; set; }
        public string TrainingTitle { get; set; }
        public long AgencyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyProfilePhoto { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string ProfilePhoto { get; set; }
        public long EnrollmentId { get; set; }
        public string EnrollmentStatus { get; set; }
        public string RejectionReason { get; set; }
        public DateTime EnrollmentDate { get; set; }


    }
}
