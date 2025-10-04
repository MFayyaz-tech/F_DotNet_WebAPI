using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace BU.DTO.DTOs.ResponseDTO.Trainings
{
    public class TrainingFeedBackResponseDTO
    {
        public long TrainingFeedBackId { get; set; }
        public long TrainingId { get; set; }
        public long CustomerId { get; set; }
        public string FeedBack { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
