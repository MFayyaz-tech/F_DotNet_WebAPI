
using System.Collections.Generic;

namespace BU.DTO.DTOs.ResponseDTO.Testimonials
{
	public class TestimonialsDTO 
    {
		public int RatingCount { set; get; }
		public long TrainingId { get; set; }
		public string TrainingTitle { get; set; }

		public string TrainingStatus { get; set; }
		public long Rating { get; set; }
		public string TrainerName { get; set; }
        public IEnumerable<TestimonialsDetails> feedBackList { get; set; }


    }
}

