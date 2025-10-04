using Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services.IServices.Email
{
    public interface IEmailService
    {
        bool CustomerRegistrationEmail(Fe_users objUser, string appUrl);
        bool ForgotPassword(Fe_users objUser, string origin, string appUrl);
        bool ResetPasswordEmail(Fe_users objUser, string appUrl);
        bool ResendOTPEmail(Fe_users objUser, string appUrl);
        string RandomTokenString();
        bool AccountBlockedEmailToUser(Fe_users objUser, string appUrl);
        bool AccountBlockedEmailToAdmin(Fe_users adminUser, Fe_users objUser, string appUrl, List<string> ccTo, List<string> bCCTo);
        bool Send(string to, List<string> ccTo, List<string> bCCTo, string subject, string html, string from = null);


		/********************************Agency*************************************/
		bool AgencyRegistrationEmail(Fe_users objUser, string appUrl);


	}
}
