using BU.DTO.DTOs.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.ResponseDTO.Trainings
{
    public class TrainingDetailResponseDTO
    {
        public long TrainingId { get; set; }
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencyPhoto { get; set; }
        public string AgencyPhone { get; set; }
        public string TrainingTitle { get; set; }
        public string PhotoPath { get; set; }
        public long TrainerId { get; set; }
        public string TrainerName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Duration { get; set; }
        public decimal Fee { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLng { get; set; }
        public string Details { get; set; }
        public string TrainingCategory { get; set; }
        public string TrainingStatus { get; set; }
        public int TrainingProgress { get; set; }
        public bool IsActive { get; set; }
        public int TotalEnrolledCustomers { get; set; }
        public long EnrollmentId { get; set; }
        public DateTime TrainingEnrollDate { get; set; }
        public string EnrollmentStatus { get; set; }
        public long AverageRating { get; set; }
        public string MediaPath { get; set; }
        public long Rating { get; set; }
        public bool isApprovalRequired { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TrainingMediaDTO> TrainingMedia { get; set; }
        public List<TrainingMediaDTO> TrainingBanner { get; set; }
        public List<TrainingFeedBackDTO> TrainingFeedBacks { get; set; }
    }
}
