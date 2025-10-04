using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Trainings
{
    public class TrainingEnrollmentMediaDTO : BaseDTO
    {
        public long MediaId { get; set; }
        public long EnrollmentId { get; set; }
        public string MediaName { get; set; }
        public string MediaPath { get; set; }
        public string MediaType { get; set; }
        public string MediaCategory { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
