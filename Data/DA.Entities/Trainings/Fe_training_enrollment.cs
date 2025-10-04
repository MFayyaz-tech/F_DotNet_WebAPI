using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Trainings
{
    [Table("Fe_training_enrollment")]
    public class Fe_training_enrollment : BaseEntity
    {
        [Key]
        public long Enrollment_id { get; set; }
        public long Training_id { get; set; }
        public long Customer_id { get; set; }
        public string Enrollment_status { get; set; }
        public DateTime Enrollment_date { get; set; }
        public string Rejection_reason { get; set; }
        public bool Is_active { get; set; }
        [Write(false)]
        [Computed]
        public string Training_title { get; set; }
        [Write(false)]
        [Computed]
        public int Training_progress { get; set; }
        [Write(false)]
        [Computed]
        public string Training_status { get; set; }
        [Write(false)]
        [Computed]
        public decimal Location_lat { get; set; }
        [Write(false)]
        [Computed]
        public decimal Location_lng { get; set; }
        [Write(false)]
        [Computed]
        public DateTime From_date { get; set; }
        [Write(false)]
        [Computed]
        public string Duration { get; set; }
        [Write(false)]
        [Computed]
        public DateTime To_date { get; set; }
        [Write(false)]
        [Computed]
        public long Agency_id {  get; set; }
        [Write(false)]
        [Computed]
        public string Company_name { get; set; }
        [Write(false)]
        [Computed]
        public string Company_profile_photo { get; set; }
        [Write(false)]
        [Computed]
        public string agency_phone { get; set; }
        [Write(false)]
        [Computed]
        public string trainer_name { get; set; }
        [Write(false)]
        [Computed]
        public string Customer_name { get; set; }
        [Write(false)]
        [Computed]
        public string Address1 { get; set; }
        [Write(false)]
        [Computed]
        public string City { get; set; }
        [Write(false)]
        [Computed]
        public string State { get; set; }
        [Write(false)]
        [Computed]
        public string Zip_code { get; set; }
        [Write(false)]
        [Computed]
        public string Country { get; set; }
        [Write(false)]
        [Computed]
        public decimal Lat { get; set; }
        [Write(false)]
        [Computed]
        public decimal Lng { get; set; }
        [Write(false)]
        [Computed]
        public string Photo_path { get; set; }
        [Write(false)]
        [Computed]
        public string Total_enrolled_customers { get; set; }
        [Write(false)]
        [Computed]
        public string Feedback_count { get; set; }
    }
}
