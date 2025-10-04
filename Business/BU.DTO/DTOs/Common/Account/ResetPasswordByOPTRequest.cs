using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BU.DTO.DTOs.Common.Account
{
	public class ResetPasswordByOPTRequest
	{
		[Required]
		public string OTP { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
