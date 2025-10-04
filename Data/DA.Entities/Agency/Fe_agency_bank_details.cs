using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Agency
{
    [Table("Fe_agency_bank_details")]
    public class Fe_agency_bank_details : BaseEntity
    {
        [Key]
        public long Bank_id { get; set; }
        public long Agency_id { get; set; }
        public string Bank_name { get; set; }
        public string Account_title { get; set; }
        public string Account_number { get; set; }
        public string Description { get; set; }
        public bool Is_default { get; set; }
        public bool Is_active { get; set; }
    }
}
