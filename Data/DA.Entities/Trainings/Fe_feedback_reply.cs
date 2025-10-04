using System;
using Dapper.Contrib.Extensions;
using Entities.Base;

namespace DA.Entities.Trainings
{
    [Table("fe_feedback_reply")]
    public class Fe_feedback_reply : BaseEntity
    {
        [Key]
        public long Reply_id { get; set; }

        public long Training_feedback_id { get; set; }

        public string Message_reply { get; set; }

    }
}