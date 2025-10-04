using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.RequestDTO.Trainings
{
    public class TrainingUpdateRequestDTO : BaseDTO
    {
        public string TrainingID { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String TrainerName { get; set; }
        public String TrainingTitle { get; set; }
        public String TrainingDescription { get; set; }
        public decimal TrainingPrice { get; set; }
        public long TrainerId { get; set; }
        public string Duration { get; set; }
    }
}

