using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BU.DTO.DTOs.Common.Account
{
	public class ValidateResetOTPRequest
	{
		[Required]
		public string OTP{get;set;}
	}
}
