using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Trainings
{
    public class TrainingEnrollmentDTO : BaseDTO
    {
        public long EnrollmentId { get; set; }
        public long TrainingId { get; set; }
        public long CustomerId { get; set; }
        public string EnrollmentStatus { get; set; }
        public string RejectionReason { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
