using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Agency
{
	[Table("fe_agency")]
	public class Fe_agency : BaseEntity
	{
        [Key]
        public long Agency_id { get; set; }
        public long User_id { get; set; }
        public string Company_name { get; set; }
        public string Phone { get; set; }
        public string Agency_site { get; set; }
        public string Agency_support_email { get; set; }
        public string Agency_fax { get; set; }
        public string Agency_profile { get; set; }
        public string Agency_contact_person { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_code { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Signature { get; set; }
        public string Photo_path { get; set; }
        public bool Is_active { get; set; }

    }
}
