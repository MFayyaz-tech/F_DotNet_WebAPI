using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Common.Account
{
	public class ResendOTPRequestDTO
	{
		public long Id { get; set; }
		public string EmailAddress { get; set; }
		public string Phone { get; set; }
	}
}
