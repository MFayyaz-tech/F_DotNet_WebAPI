using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.ResponseDTO.Trainings
{
	public class CustomerCompletedTrainingsResponseDTO : BaseDTO
	{
        public string Enrollment_status { get; set; }
        public long TrainingId { get; set; }
        public string TrainingTitle { get; set; }
        public string FeedbackCount { get; set; }

    }
}

