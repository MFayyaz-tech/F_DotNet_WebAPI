using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Jobs
{
    [Table("fe_job_contract")]
    public class Fe_job_contract : BaseEntity
    {
        [Key]
        public long Contract_id { get; set; }
        public long Job_id { get; set; }

        [Write(false)]
        [Computed]
        public string Job_title { get; set; }
        public long Agency_id { get; set; }
        public long Bid_id { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_name { get; set; }
        public string Contract_status { get; set; }
        public int Contract_progress { get; set; }
        public string Agency_feedback { get; set; }
        public int Agency_rating { get; set; }
        public string Customer_feedback { get; set; }
        public int Customer_rating { get; set; }
        public string Attachment_media { get; set; }
        public string Cancelation_reason { get; set; }
        public bool Is_active { get; set; }
        public long Agent_id { get; set; }

        [Write(false)]
        [Computed]
        public string duration_type { get; set; }
        [Write(false)]
        [Computed]
        public string bidder_type { get; set; }
        [Write(false)]
        [Computed]
        public string price_type { get; set; }
        [Write(false)]
        [Computed]
        public DateTime? From_date { get; set; }
        [Write(false)]
        [Computed]
        public DateTime? To_date { get; set; }
        [Write(false)]
        [Computed]
        public decimal Lat { get; set; }
        [Write(false)]
        [Computed]
        public decimal Lng { get; set; }
        [Write(false)]
        [Computed]
        public long Customer_id { get; set; }
        [Write(false)]
        [Computed]
        public string Address1 { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_contact_person { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_photo { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_profile { get; set; }
        [Computed]
        public decimal Average_rating { get; set; }
        [Write(false)]
        [Computed]
        public string User_id { get; set; }
        [Write(false)]
        [Computed]
        public string First_name { get; set; }
        [Write(false)]
        [Computed]
        public string Last_name { get; set; }
        [Write(false)]
        [Computed]
        public string Phone { get; set; }
        [Write(false)]
        [Computed]
        public string Email_address { get; set; }
        [Write(false)]
        [Computed]
        public string License_number { get; set; }
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
        public string Total_complete_job { get; set; }
        [Write(false)]
        [Computed]
        public string In_progress_job { get; set; }
        [Write(false)]
        [Computed]
        public string Cancelled_job { get; set; }
        [Write(false)]
        [Computed]
        public string Country { get; set; }
        [Write(false)]
        [Computed]
        public string Photo_path { get; set; }


        [Write(false)]
        [Computed]
        public string Job_description { get; set; }


        [Write(false)]
        [Computed]
        public string Job_status { get; set; }


        [Write(false)]
        [Computed]
        public decimal Price_min { get; set; }


        [Write(false)]
        [Computed]
        public decimal Price_Max { get; set; }


        [Write(false)]
        [Computed]
        public DateTime Created_date { get; set; }


        [Write(false)]
        [Computed]
        public string Updated_date { get; set; }


        [Write(false)]
        [Computed]
        public string Customer_name { get; set; }

        [Write(false)]
        [Computed]
        public string Customer_photo { get; set; }

    }
}
