using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Base
{
    public abstract class BaseEntity
    {
		[JsonIgnore]
		public long Created_by { get; set; }
		[JsonIgnore]
		public DateTime Create_date { get; set; }
		[JsonIgnore]
		public long? Updated_by { get; set; }
		public DateTime? Update_date { get; set; }
		public bool Is_deleted { get; set; }
	}
}
