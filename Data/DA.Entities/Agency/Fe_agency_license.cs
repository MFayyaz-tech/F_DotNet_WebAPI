using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Agency
{
    [Table("Fe_agency_license")]
    public class Fe_agency_license : BaseEntity
    {
        [Key]
        public long License_id { get; set; }
        public long Agency_id { get; set; }
        public string License_name { get; set; }
        public string License_type { get; set; }
        public string Issuing_authority { get; set; }
        //make expiry_date to datetime and nullable
        public string Expiry_date { get; set; }
        public string License_state { get; set; }
        public string License_front_image_path { get; set; }
        public string License_back_image_path { get; set; }
        public bool Is_default { get; set; }
        public bool Is_active { get; set; }
    }
}
