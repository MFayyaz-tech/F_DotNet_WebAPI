using System;
using Entities.Base;
using Dapper.Contrib.Extensions;
using Entities.Base;

namespace DA.Entities.Jobs
{
    [Table("Fe_agent")]
    public class Fe_agent : BaseEntity
    {
        [Key]
        public long Agent_id { get; set; }
        public long Agency_id { get; set; }
        public long User_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Phone { get; set; }
        public string Email_address { get; set; }
        public string License_number { get; set; }
        public string Experince { get; set; }
        public string Introduction { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_code { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Photo_path { get; set; }
        public bool Is_active { get; set; }
        [Write(false)]
        [Computed]
        public string Job_assign { get; set; }
        [Write(false)]
        [Computed]
        public string Total_completed_jobs { get; set; }
        [Write(false)]
        [Computed]
        public string In_progress_Jobs { get; set; }
        [Write(false)]
        [Computed]
        public string Cancelled_jobs { get; set; }



    }
}

