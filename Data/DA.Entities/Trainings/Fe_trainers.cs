using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Trainings
{
    [Table("Fe_trainers")]

    public class Fe_trainers : BaseEntity
    {
        [Key]
        public long Trainer_id { get; set; }
        public long Agency_id { get; set; }
        public long User_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Phone { get; set; }
        public string License_number { get; set; }
        public string Experience { get; set; }
        public string Intoduction { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_code { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Photo_path { get; set; }
        public bool Is_active { get; set; }
    }
}
