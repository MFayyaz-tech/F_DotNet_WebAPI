using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Trainings
{
    [Table("Fe_training_enrollment_media")]
    public class Fe_training_enrollment_media : BaseEntity
    {
        [Key]
        public long Media_id { get; set; }
        public long Enrollment_id { get; set; }
        public string Media_name { get; set; }
        public string Media_path { get; set; }
        public string Media_type { get; set; }
        public string Media_category { get; set; }
        public bool Is_active { get; set; }
    }
}
