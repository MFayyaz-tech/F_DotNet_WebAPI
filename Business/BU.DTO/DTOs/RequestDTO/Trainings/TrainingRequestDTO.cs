using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.RequestDTO.Trainings
{
    public class TrainingRequestDTO:BaseDTO
    {
        public long TrainingId { get; set; }
        public long AgencyId { get; set; }
        public long CustomerId { get; set; }
        public long EnrollmentId { get; set; }
        public string TrainingStatus { get; set; }
        public int TrainingProgress { get; set; }
        public int TrainingFeedbackId { get; set; }
    }
}
