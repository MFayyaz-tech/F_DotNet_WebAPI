using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Customer
{
    [Table("Fe_customer_cards")]
    public class Fe_customer_cards : BaseEntity
    {
        [Key]
        public long Customer_card_id { get; set; }
        public long Customer_id { get; set; }
        public string Card_id { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public string Expire_date { get; set; }
        public string Cvv_number { get; set; }
        public string Exp_month { get; set; }
        public string Credit_card_number { get; set; }
        public bool Is_default { get; set; }
        public bool Is_active { get; set; }
    }
}
