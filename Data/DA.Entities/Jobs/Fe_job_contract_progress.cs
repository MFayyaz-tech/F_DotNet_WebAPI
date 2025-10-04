using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Jobs
{
    [Table("Fe_job_contract_progress")]
    public class Fe_job_contract_progress : BaseEntity
    {
        [Key]
        public long Contract_progress_id { get; set; }
        public long Contract_id { get; set; }
        public int Contract_progress { get; set; }
        public string Contract_status { get; set; }
        public string Contract_notes { get; set; }
        public bool Is_active { get; set; }
    }
}
