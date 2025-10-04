using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Trainings
{
    [Table("Fe_trainings")]
    public class Fe_trainings : BaseEntity
    {
        [Key]
        public long Training_id { get; set; }
        public long Agency_id { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_name { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_photo { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_phone { get; set; }
        public string Training_title { get; set; }
        public string Photo_path { get; set; }
        public long Trainer_id { get; set; }
        [Write(false)]
        [Computed]
        public string Trainer_name { get; set; }
        public DateTime From_date { get; set; }
        public DateTime To_date { get; set; }
        public string Duration { get; set; }
        public decimal Fee { get; set; }
        public decimal Location_lat { get; set; }
        public decimal Location_lng { get; set; }
        public string Details { get; set; }
        public string Training_category { get; set; }
        public string Training_status { get; set; }
        public bool Is_approval_required { get; set; }
        public int Training_progress { get; set; }
        public bool Is_active { get; set; }
        [Write(false)]
        [Computed]
        public int Total_enrolled_customers { get; set; }
        [Write(false)]
        [Computed]
        public long Enrolment_id { get; set; }
        [Write(false)]
        [Computed]
        public string Enrollment_status { get; set; }
        [Write(false)]
        [Computed]
        public DateTime Training_enroll_date { get; set; }
        [Write(false)]
        [Computed]
        public long Average_rating { get; set; }
        [Write(false)]
        [Computed]
        public int Rating_count { get; set; }
        [Write(false)]
        [Computed]
        public string Attachment_media { get; set; }
        [Write(false)]
        [Computed]
        public string Feedback { get; set; }
        [Write(false)]
        [Computed]
        public long Feedback_rating { get; set; }
        [Write(false)]
        [Computed]
        public long Training_feedback_id { get; set;}
        [Write(false)]
        [Computed]
        public string Customer_Name { get; set; }
        [Write(false)]
        [Computed]
        public long Rating { get; set; }
        [Write(false)]
        [Computed]
        public string Media_path { get; set; }





    }
}
