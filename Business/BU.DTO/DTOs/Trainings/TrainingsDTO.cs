using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Trainings
{
    public class TrainingsDTO : BaseDTO
    {
        public long TrainingId { get; set; }
        public long AgencyId { get; set; }
        public string TrainingTitle { get; set; }
        public string Base64Image { get; set; }
        public string PhotoPath { get; set; }
        public long TrainerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Duration { get; set; }
        public decimal Fee { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLng { get; set; }
        public string Details { get; set; }
        public string TrainingCategory { get; set; }
        public string TrainingStatus { get; set; }
        public bool IsApprovalRequired { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long AverageRating { get; set; }
    }
}
