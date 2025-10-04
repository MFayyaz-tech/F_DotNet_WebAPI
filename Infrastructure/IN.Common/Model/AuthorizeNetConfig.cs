using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN.Common.Model
{
	public class AuthorizeNetConfig
	{
		public string ApiLoginKey { get; set; }
		public string TransactionKey { get; set; }
		public string Environment { get; set; }
	}
}
