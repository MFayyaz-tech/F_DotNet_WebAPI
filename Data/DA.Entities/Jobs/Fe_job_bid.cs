using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DA.Entities.Jobs
{
    [Table("Fe_job_bid")]
    public class Fe_job_bid : BaseEntity
    {
        [Key]
        public long Bid_id { get; set; }
        public long Agency_id { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_name { get; set; }
        public long Job_id { get; set; }
        [Write(false)]
        [Computed]
        public string Job_title { get; set; }
        [Write(false)]
        [Computed]
        public string Job_status { get; set; }
        [Write(false)]
        [Computed]
        public decimal Lat { get; set; }
        [Write(false)]
        [Computed]
        public decimal Lng { get; set; }
        [Write(false)]
        [Computed]
        public DateTime From_date { get; set; }
        [Write(false)]
        [Computed]
        public DateTime To_date { get; set; }
        [Write(false)]
        [Computed]
        public string Job_assignment_status { get; set; }
        [Write(false)]
        [Computed]
        public decimal Average_bid_amount { get; set; }
        public decimal Bid_amount { get; set; }
        public DateTime Bid_date { get; set; }
        public string Bid_type { get; set; }
        public string Bid_notes { get; set; }
        public bool Is_active { get; set; }
        [Write(false)]
        [Computed]
        public decimal Agency_lat { get; set; }
        [Write(false)]
        [Computed]
        public decimal Agency_lng { get; set; }
        [Write(false)]
        [Computed]
        public string Photo_path { get; set; }
        [Write(false)]
        [Computed]
        public string Agency_profile { get; set; }
        [Write(false)]
        [Computed]
        public long User_id { get; set; }
        [Write(false)]
        [Computed]
        public string Full_name { get; set; }
    }
}
