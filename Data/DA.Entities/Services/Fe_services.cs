using System;
using Dapper.Contrib.Extensions;
using Entities.Base;

namespace DA.Entities.Services
{
    [Table("Fe_services")]
    public class Fe_services : BaseEntity
	{
        [Key]
        public long Services_id { get; set; }
        public long Agency_id { get; set; }
        public long Category_id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Service_title { get; set; }
        public string Price_type { get; set; }
        public string Service_description { get; set; }
        public string Service_banner { get; set; }
        public long Is_obsulate { get; set; }

        [Write(false)]
        [Computed]
        public string Company_name { get; set; }
        [Write(false)]
        [Computed]
        public string Phone { get; set; }
        [Write(false)]
        [Computed]
        public string Email_address { get; set; }
        [Write(false)]
        [Computed]
        public string User_id { get; set; }



    }
}

