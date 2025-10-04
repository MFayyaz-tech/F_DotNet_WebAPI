using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Trainings
{
    [Table("Fe_training_feedback")]
    public class Fe_training_feedback : BaseEntity
    {
        [Key]
        public long Training_feedback_id { get; set; }
        public long Training_id { get; set; }
        public long Customer_id { get; set; }
        [Write(false)]
        [Computed]
        public string Customer_name { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public string Attachment_media { get; set; }
        public bool Is_active { get; set; }
        [Write(false)]
        [Computed]
        public string Training_Title { get; set; }
    }
}
