using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Trainings
{
    public class TrainingFeedBackDTO : BaseDTO
    {
        public long TrainingFeedBackId { get; set; }

        public long TrainingId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TrainingTitle { get; set; }
        public string FeedBack { get; set; }
        public int Rating { get; set; }
        public string Base64Image { get; set; }
        public string AttachmentMedia { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
