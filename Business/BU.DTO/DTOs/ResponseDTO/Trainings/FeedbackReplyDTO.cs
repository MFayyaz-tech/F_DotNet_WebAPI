using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.ResponseDTO.Trainings
{
    public class FeedbackReplyDTO : BaseDTO
    {


        public long TrainingFeedbackId { get; set; }

        public string MessageReply { get; set; }

        public long Reply_id { get; set; }

    }
}
