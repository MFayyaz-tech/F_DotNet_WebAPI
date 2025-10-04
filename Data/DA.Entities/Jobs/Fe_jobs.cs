using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Jobs
{
    [Table("fe_jobs")]
    public class Fe_jobs : BaseEntity
    {
        private string price_type;

        [Key]
        public long Job_id { get; set; }
        public long Customer_id { get; set; }
        [Write(false)]
        [Computed]
        public string Customer_name { get; set; }
        [Write(false)]
        [Computed]
        public string Address { get; set; }
        [Write(false)]
        [Computed]
        public string Customer_Profile { get; set; }
        public string Job_title { get; set; }
        public string Price_type { get => price_type; set => price_type = value; }
        public decimal? Price_min { get; set; }
        public decimal? Price_max { get; set; }
        public string Duration_type { get; set; }
        public DateTime? From_date { get; set; }
        public DateTime? To_date { get; set; }
        public string bidder_type { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Job_description { get; set; }
        public string Job_status { get; set; }
        public string Job_category { get; set; }
        public bool Is_active { get; set; }
        [Write(false)]
        [Computed]
        public long Job_bid_count { get; set; }
        [Write(false)]
        [Computed]
        public decimal average_bid_amount { get; set; }
        [Write(false)]
        [Computed]
        public long Agency_id { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_name { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_phone { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_profile_image { get; set; }
        [Write(false)]
        [Computed]
        public long Contract_id { get; set; }
        [Write(false)]
        [Computed]
        public int Contract_progress { get; set; }
        [Write(false)]
        [Computed]
        public decimal Contract_price { get; set; }
        [Write(false)]
        [Computed]
        public string Contract_notes { get; set; }
        [Write(false)]
        [Computed]
        public string Contract_status { get; set; }
        [Write(false)]
        [Computed]
        public string Agent_name { get; set; }
        [Write(false)]
        [Computed]
        public long Agent_id { get; set; }
        [Write(false)]
        [Computed]
        public decimal Agency_rating { get; set; }
        [Write(false)]
        [Computed]
        public string Photo_path { get; set; }

    }
}
