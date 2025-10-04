using System;
using Dapper.Contrib.Extensions;
using Entities.Base;

namespace DA.Entities.Billing
{
    [Table("fe_payment")]
    public class Fe_payment : BaseEntity
    {
        [Key]
        public long Payment_id { get; set; }
        public String Transaction_id { get; set; }
        public decimal Amount { get; set; }
        public long? Card_id { get; set; }
        public long? Job_id { get; set; }
        public long? Bid_id { get; set; }
        public long? Training_id { get; set; }
        public string Payment_type { get; set; }
        public string Payment_status { get; set; }



        [Write(false)]
        [Computed]
        public long Agency_id { get; set; }
        [Write(false)]
        [Computed]
        public decimal Total_job_payment { get; set; }
        [Write(false)]
        [Computed]
        public decimal Total_training_payment { get; set; }
        [Write(false)]
        [Computed]
        public decimal Total_payment_earned { get; set; }
        [Write(false)]
        [Computed]
        public decimal Total_jobs_done { get; set; }
        [Write(false)]
        [Computed]
        public decimal Total_training_done { get; set; }

    }
}

