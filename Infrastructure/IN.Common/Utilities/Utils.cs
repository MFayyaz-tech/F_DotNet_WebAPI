using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace IN.Common.Utilities
{
	public class Utils
	{
		public static string GenerateOTP()
		{
			// Use a random number generator to create a 6-digit OTP
			Random random = new Random();
			int otpNumber = random.Next(100000, 999999); // Generates a number between 100000 and 999999

			return otpNumber.ToString();
		}

		public static string RandomTokenString()
		{
			using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				var randomBytes = new byte[40];
				rngCryptoServiceProvider.GetBytes(randomBytes);
				// convert random bytes to hex string
				return BitConverter.ToString(randomBytes).Replace("-", "");
			}
		}

		public static string CleanPhoneNumber(string phoneNumber)
		{
			if (string.IsNullOrEmpty(phoneNumber))
			{
				return string.Empty;
			}
			var regex = new Regex(@"\D");
			return regex.Replace(phoneNumber, "");
		}
		public static string TruncateString(string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}
	}
}
