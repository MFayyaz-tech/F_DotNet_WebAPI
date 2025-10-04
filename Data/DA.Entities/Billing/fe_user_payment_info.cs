using Dapper.Contrib.Extensions;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Entities.Billing
{
	[Table("fe_user_payment_info")]
	public class fe_user_payment_info : BaseEntity
	{
		[Key]
		public long card_id { get; set; }
		public long user_id { get; set; }
		public string card_holder_name { get; set; }
		public string card_number { get; set; }
		public DateTime card_expiry_date { get; set; }
		public int card_cvv { get; set; }
		public string card_type{ get; set; }
		public bool is_deleted { get; set; }
		public bool is_active { get; set; }
		public DateTime create_date { get; set; }
		public long created_by { get; set; }
		public DateTime update_date { get; set; }
		public long updated_by { get; set; }
	}
}
