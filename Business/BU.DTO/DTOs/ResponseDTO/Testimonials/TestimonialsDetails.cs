using System;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using System.Collections.Generic;

namespace BU.DTO.DTOs.ResponseDTO.Testimonials
{
	public class TestimonialsDetails
	{
     
        public string FeedBackComment { get; set; }
        public string AttachmentMedia { get; set; }
        public long  TrainingFeedbackId { get; set; }
        public DateTime CreateDate { get; set; }
        public long FeedBackRating { get; set; }
        public string CustomerName { get; set; }
        public string PhotoPath { get; set; }
        public FeedbackReplyDTO Replies { get; set; }




    }
}

