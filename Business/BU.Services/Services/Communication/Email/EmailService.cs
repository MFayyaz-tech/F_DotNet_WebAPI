using Common.Helper;
using Entities.Users;
using Logging;
using Services.IServices.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Common.Email;
using DTO.DTOs.User;

namespace FH.Services.Services.email
{
	public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogging _logging;
        private string _webBaseURL;
		private string companyName = "Figgers Enterprises";
		public EmailService(ILogging logging, IConfiguration configuration, IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
            _logging = logging;
        }

		public bool Send(string to, List<string> ccTo, List<string> bCCTo, string subject, string html, string from = null)
		{
			bool isSend = true;
			try
			{
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse(from ?? _emailConfiguration.EmailFrom));
				email.To.Add(MailboxAddress.Parse(to));
				email.Subject = subject;
				email.Body = new TextPart(TextFormat.Html) { Text = html };

				if (ccTo != null && ccTo.Count > 0)
				{
					InternetAddressList ccToList = new InternetAddressList();
					foreach (var emailaddress in ccTo)
					{
						ccToList.Add(new MailboxAddress("", emailaddress));
					}
					email.Cc.AddRange(ccToList);
				}

				if (bCCTo != null && bCCTo.Count > 0)
				{
					InternetAddressList bCCToList = new InternetAddressList();
					foreach (var emailaddress in bCCTo)
					{
						bCCToList.Add(new MailboxAddress("", emailaddress));
					}
					email.Bcc.AddRange(bCCToList);
				}

				// send email
				var smtp = new SmtpClient();
				smtp.Connect(_emailConfiguration.SmtpHost, _emailConfiguration.SmtpPort, SecureSocketOptions.Auto);

				smtp.Authenticate(_emailConfiguration.SmtpUser, CryptoEngine.Decrypt(_emailConfiguration.SmtpPass));
				smtp.Send(email);
				smtp.Disconnect(true);

				return isSend;
			}
			catch (System.Exception ex)
			{
				_logging.Fatal(ex.ToString());
				isSend = false;
				return isSend;
			}

		}

		public bool SendWithAttachement(string to, List<string> ccTo, List<string> bCCTo, string subject, string html, string from = null, MemoryStream file = null, string fileName = null)
		{
			bool isSend = true;
			try
			{

				// create message
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse(from ?? _emailConfiguration.EmailFrom));
				email.To.Add(MailboxAddress.Parse(to));
				email.Subject = subject;
				var builder = new BodyBuilder();
				builder.HtmlBody = html;

				if (ccTo != null && ccTo.Count > 0)
				{
					InternetAddressList ccToList = new InternetAddressList();
					foreach (var emailaddress in ccTo)
					{
						ccToList.Add(new MailboxAddress("", emailaddress));
					}
					email.Cc.AddRange(ccToList);
				}

				if (bCCTo != null && bCCTo.Count > 0)
				{
					InternetAddressList bCCToList = new InternetAddressList();
					foreach (var emailaddress in bCCTo)
					{
						bCCToList.Add(new MailboxAddress("", emailaddress));
					}
					email.Bcc.AddRange(bCCToList);
				}
				if (file != null)
					builder.Attachments.Add(fileName, file);
				email.Body = builder.ToMessageBody();
				// send email
				var smtp = new SmtpClient();
				smtp.Connect(_emailConfiguration.SmtpHost, _emailConfiguration.SmtpPort, SecureSocketOptions.Auto);

				smtp.Authenticate(_emailConfiguration.SmtpUser, CryptoEngine.Decrypt(_emailConfiguration.SmtpPass));
				smtp.Send(email);
				smtp.Disconnect(true);

				return isSend;
			}
			catch (System.Exception ex)
			{
				_logging.Fatal(ex.ToString());
				isSend = false;
				return isSend;
			}

		}

		public bool CustomerRegistrationEmail(Fe_users objUser, string appUrl)
		{
			string message;
			message = $@"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Customer Registration</title>
</head>
<body>
    <p>Hello {objUser.User_name},</p>
    
    <p>Thank you for registering with {companyName}. Here are your login details:</p>
    
    <ul>
        <li><strong>Email:</strong> {objUser.Email_address}</li>
        <li><strong>Password:</strong> {objUser.Password}</li>
    </ul>
    
    <p>If you have any questions or need assistance, feel free to contact us.</p>
    
    <p>Best regards,<br>{companyName}</p>
</body>
</html>";
			//Send Email
			return Send(
				to: objUser.Email_address,
				ccTo: null,
				bCCTo: null,
				subject: string.Format("Welcome to {0}", companyName),
				html: $@"{message}"
			);
		}

     
        public bool ForgotPassword(Fe_users objUser, string origin, string appUrl)
        {
			string message;
			message = $@"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>One-Time Password (OTP)</title>
</head>
<body>
    <p>Hello {objUser.User_name},</p>
    
    <p>Your One-Time Password (OTP) is:</p>
    
    <h2>{objUser.Reset_password_OTP}</h2>
    
    <p>This OTP is valid for a short period. Please use it to change your password.</p>
    
    <p>If you did not request this OTP, please ignore this email.</p>
    
    <p>Best regards,<br>{companyName}</p>
</body>
</html>
";

			return Send(
				to: objUser.Email_address,
				ccTo: null,
				bCCTo: null,
				subject: string.Format("{0} - Forgot Password",companyName),
				html: $@"{message}"
			);
		}

        public string RandomTokenString()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[40];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                // convert random bytes to hex string
                return BitConverter.ToString(randomBytes).Replace("-", "");
            }
        }
        public bool ResetPasswordEmail(Fe_users objUser, string appUrl)
        {
            string message;
            var passwordUrl = appUrl + $"resetpassword?token={objUser.Reset_password_token}";

            message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{passwordUrl}"">{passwordUrl}</a></p>";



            //Send Email
            return Send(
                to: objUser.Email_address,
                ccTo: null,
                bCCTo: null,
                subject: "[PORTAL] - Reset Password",
                html: $@"{message}"
            );
        }

		public bool AccountBlockedEmailToUser(Fe_users objUser, string appUrl)
		{
			throw new NotImplementedException();
		}

		public bool AccountBlockedEmailToAdmin(Fe_users adminUser, Fe_users objUser, string appUrl, List<string> ccTo, List<string> bCCTo)
		{
			throw new NotImplementedException();
		}

		public bool ResendOTPEmail(Fe_users objUser, string appUrl)
		{
			string message;
			message = $@"<!DOCTYPE html>
<html lang='en'>

<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Resend One-Time Password (OTP)</title>
</head>

<body style='font-family: Arial, sans-serif;'>

    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; border-radius: 5px;'>
        <h2 style='text-align: center; color: #333;'>Resend One-Time Password (OTP)</h2>

        <p>Dear {objUser.User_name},</p>

        <p>We noticed that you requested to resend your One-Time Password (OTP) for accessing our services. Your security is important to us, and we're here to assist you.</p>

        <p>Your OTP: <strong>{objUser.Reset_password_OTP}</strong></p>

        <p>If you didn't request this OTP, please ignore this email. Someone might have entered your email address by mistake.</p>

        <p>Thank you for using our services.</p>

        <p>Best regards,<br>{companyName}</p>
    </div>

</body>

</html>
";

			return Send(
				to: objUser.Email_address,
				ccTo: null,
				bCCTo: null,
				subject: string.Format("{0} - Forgot Password", companyName),
				html: $@"{message}"
			);
		}

		public bool AgencyRegistrationEmail(Fe_users objUser, string appUrl)
		{
			string message;
			message = $@"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Agency Registration</title>
</head>
<body>
    <p>Hello {objUser.User_name},</p>
    
    <p>Thank you for registering with {companyName}. Here are your login details:</p>
    
    <ul>
        <li><strong>Email:</strong> {objUser.Email_address}</li>
        <li><strong>Password:</strong> {objUser.Password}</li>
    </ul>
    
    <p>If you have any questions or need assistance, feel free to contact us.</p>
    
    <p>Best regards,<br>{companyName}</p>
</body>
</html>";
			//Send Email
			return Send(
				to: objUser.Email_address,
				ccTo: null,
				bCCTo: null,
				subject: string.Format("Welcome to {0}", companyName),
				html: $@"{message}"
			);
		}
	}
}
