/**************************************************************************************************************************************
 * Architechted By: Abid Ali (abidie@hotmail.com)
 * Development Manager: Nexelus
 * Date: August-2023
 * *************************************************************************************************************************************/

using Entities;
using DTO.Core;
using System;
using DTO.DTOs.Users;
using BU.DTO.DTOs.Common.Account;
using BU.DTO.DTOs.RequestDTO.Authantication;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthenticationService
    {
        UserContext Authenticate(UserAuthDTO model);
        
             UserContext AuthenticateViaGoogle(UserAuthDTO model);
        bool ForgotPassword(ForgotPasswordRequest model, string origin);
        string ValidateResetToken(ValidateResetTokenRequest model);
        string ValidateResetOTP(ValidateResetOTPRequest model);
        bool ResetPassword(ResetPasswordRequest model);
		bool ResetPasswordByOTP(ResetPasswordByOPTRequest model);
		bool ChangePassword(ChangePasswordRequest model);
        bool DeActivateAccount(DeActivateAccountDTO model);

        bool ResendOTP(ResendOTPRequestDTO model, string origin);


    }
}
