using Common;
using DAO;
using Entities;
using DTO.Core;
using ORM;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DTO.DTOs.Users;
using DAO.DAO.User;
using AutoMapper;
using Common.Helper;
using Entities.Users;
using Microsoft.Extensions.Configuration;
using BU.DTO.DTOs.Common.Account;
using Services.IServices.Email;
using IN.Common.Utilities;
using DA.Entities.Customer;
using DA.Entities.Agency;
using DA.DAO.DAO.Customer;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.Agency;
using DA.DAO.DAO.Agency;
using Logging;

namespace Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Fe_users> _userRepository;
        private readonly IRepository<Fe_customers> _customerRepository;
        private readonly IRepository<Fe_agency> _agencyRepository;
        public IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private IDDLService _ddlService;
        private readonly ILogging _logging;
        public AuthenticationService(IRepository<Fe_users> userRepository,
            IRepository<Fe_customers> customerRepository,
            IRepository<Fe_agency> agencyRepository,
            IEmailService emailService, IMapper mapper, IConfiguration configuration, ILogging logging)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _agencyRepository = agencyRepository;
			_emailService = emailService;
            _mapper = mapper;
            _configuration = configuration;
            _logging = logging;
        }
        public UserContext Authenticate(UserAuthDTO model)
        {
            UserContext uc = new UserContext();
            var userExist = _userRepository.GetList(Database.MAIN, UserDAO.IsEmailExistQuery, new { Email = model.EmailAddress }).FirstOrDefault();
            if (userExist != null)
            {
                var encryptedpass = !string.IsNullOrEmpty(model.Password) ? (CryptoEngine.Encrypt(model.Password)) : "";
                model.Password = encryptedpass;

                _logging.Fatal($"User try to login with email {model.EmailAddress}");

                var objUser = _userRepository.GetList(Database.MAIN, UserDAO.LogInQuery, new { Email = model.EmailAddress, model.Password }).FirstOrDefault();



                if (objUser != null)
                {
                    uc.User = _mapper.Map<Fe_users, UserAuthDTO>(objUser);
                    if(uc.User.UserType == "Customer")
                    {

                        uc.User.CustomerDetails = _mapper.Map<Fe_customers,FeCustomerDTO>(_customerRepository.GetList(Database.MAIN, FeCustomerDAO.GetCustomerByUserIdQuery, new { UserId = objUser.User_id }).FirstOrDefault());
                    }
                    else
                    {
                        uc.User.AgencyDetails = _mapper.Map<Fe_agency, AgencyDTO>(_agencyRepository.GetList(Database.MAIN, AgencyDAO.GetAgencyByUserId, new { UserId = objUser.User_id }).FirstOrDefault());
                    }

                    var upadteLastLogin = _userRepository.GetList(Database.MAIN, UserDAO.UpdatedLastLoginQuery, new { UserID = objUser.User_id }).FirstOrDefault();

                    return uc;
                }
            }
            else
            {
                return uc;
            }
            return uc;

        }

        public UserContext AuthenticateViaGoogle(UserAuthDTO model)
        {
            UserContext uc = new UserContext();

            // Ensure GoogleId is provided
            if (string.IsNullOrEmpty(model.GoogleId))
            {
                _logging.Error("Google ID is required for Google authentication.");
                return uc; // Return empty context if Google ID is missing
            }

            // Check if the user exists based on EmailAddress (assuming EmailAddress is unique)
            var userExist = _userRepository.GetList(Database.MAIN, UserDAO.IsEmailExistQuery, new { Email = model.EmailAddress }).FirstOrDefault();

            if (userExist != null)
            {
                // User exists, proceed with user data retrieval
                var objUser = _userRepository.GetList(Database.MAIN, UserDAO.LogInEmailQuery, new { Email = model.EmailAddress }).FirstOrDefault();

                if (objUser != null)
                {
                    uc.User = _mapper.Map<Fe_users, UserAuthDTO>(objUser);
                    if (uc.User.UserType == "Customer")
                    {
                        uc.User.CustomerDetails = _mapper.Map<Fe_customers, FeCustomerDTO>(_customerRepository.GetList(Database.MAIN, FeCustomerDAO.GetCustomerByUserIdQuery, new { UserId = objUser.User_id }).FirstOrDefault());
                    }
                    else
                    {
                        uc.User.AgencyDetails = _mapper.Map<Fe_agency, AgencyDTO>(_agencyRepository.GetList(Database.MAIN, AgencyDAO.GetAgencyByUserId, new { UserId = objUser.User_id }).FirstOrDefault());
                    }

                    var updateLastLogin = _userRepository.GetList(Database.MAIN, UserDAO.UpdatedLastLoginQuery, new { UserID = objUser.User_id }).FirstOrDefault();

                    return uc;
                }
                else
                {
                    _logging.Error($"No user found with Google ID {model.GoogleId}.");
                    return uc; // Return empty context if user not found
                }
            }
            else
            {
                _logging.Error($"No user found with Email {model.EmailAddress}.");
                return uc; // Return empty context if email not found
            }
        }



        public bool ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            string appUrl = _configuration["ClientApp:clientAppUrl"];
            bool isSuccess = true;
            var objUser = _userRepository.GetList(Database.MAIN, UserDAO.GetUserByEmail, new { email = model.EmailAddress}).FirstOrDefault();
            if (objUser == null)
            {
                isSuccess = false;
                return isSuccess;
            }
            try
            {
				//create reset token that expires after 5 minutes
				//objUser.Reset_password_token = Utils.RandomTokenString();
				//objUser.Token_expiry_date = DateTime.UtcNow.AddMinutes(5);
				//create reset otp that expires after 5 minutes
				objUser.Reset_password_OTP = Utils.GenerateOTP();
				objUser.OTP_expiry_date = DateTime.UtcNow.AddMinutes(5);

				//send email
				_emailService.ForgotPassword(objUser, origin, appUrl);
                //Update the user with token
                _userRepository.Update(objUser);

                return isSuccess;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isSuccess = false;
                return isSuccess;
            }

        }

        public bool ResetPassword(ResetPasswordRequest model)
        {
            bool isSuccess = true;
            try
            {
				Fe_users account = _userRepository.GetList(Database.MAIN, string.Format(UserDAO.ResetUserPasswordByToken, model.Token)).FirstOrDefault();
                if (account == null)
                {
                    isSuccess = false;
                    return isSuccess;
                }
                //encrypt and save 
                account.Password = CryptoEngine.Encrypt(model.Password);
                //remove reset token and expiry date
                account.Reset_password_token = null;
                account.Token_expiry_date = null;
                //update user with updated password
                _userRepository.Update(account);
                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                return isSuccess;
            }

        }

		public bool ResetPasswordByOTP(ResetPasswordByOPTRequest model)
		{
			bool isSuccess = true;
			try
			{
				Fe_users account = _userRepository.GetList(Database.MAIN, UserDAO.VerifyUserOTPToResetPassword, new { OTP = model.OTP }).FirstOrDefault();

				if (account == null)
				{
					isSuccess = false;
					return isSuccess;
				}
				//encrypt and save 
				account.Password = CryptoEngine.Encrypt(model.Password);
				//remove reset token and expiry date
				account.Reset_password_OTP = null;
				account.OTP_expiry_date = null;
				account.Is_active = true;
				//update user with updated password
				_userRepository.Update(account);
				return isSuccess;
			}
			catch (Exception ex)
			{
				isSuccess = false;
				return isSuccess;
			}
		}

		public bool ChangePassword(ChangePasswordRequest model)
        {
            bool isSuccess = true;
            try
            {
                model.OldPassword = CryptoEngine.Encrypt(model.OldPassword);
				Fe_users account = _userRepository.GetList(Database.MAIN, string.Format(UserDAO.GetUserToChangePassword, model.EmailAddress, model.OldPassword)).FirstOrDefault();
                if (account == null)
                {
                    isSuccess = false;
                    return isSuccess;
                }
                //encrypt and save 
                account.Password = CryptoEngine.Encrypt(model.NewPassword);
                //remove reset token and expiry date
                account.Reset_password_token = null;
                account.Token_expiry_date = null;
                //update user with updated password
                _userRepository.Update(account);
                return isSuccess;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isSuccess = false;
                return isSuccess;
            }

        }

		public string ValidateResetToken(ValidateResetTokenRequest model)
		{
			Fe_users account = _userRepository.GetList(Database.MAIN, string.Format(UserDAO.VerifyUserTokenToResetPassword, model.Token)).FirstOrDefault();

			if (account == null)
				return null;
			else
			{
				//remove token expiry date
				account.Token_expiry_date = null;
				//update user with updated password
				_userRepository.Update(account);
				return account.Reset_password_token;
			}
		}

		public string ValidateResetOTP(ValidateResetOTPRequest model)
		{
			Fe_users account = _userRepository.GetList(Database.MAIN, UserDAO.VerifyUserOTPToResetPassword,new { OTP = model.OTP}).FirstOrDefault();

			if (account == null)
				return null;
			else
			{
				//remove token expiry date
				//account.OTP_expiry_date = null;
				//update user with updated password
				//_userRepository.Update(account);
				return account.Reset_password_OTP;
			}
		}

		public bool ResendOTP(ResendOTPRequestDTO model, string origin)
		{
			string appUrl = _configuration["ClientApp:clientAppUrl"];
			bool isSuccess = true;
			var objUser = _userRepository.GetList(Database.MAIN, UserDAO.GetUserByEmail, new { Email = model.EmailAddress }).FirstOrDefault();
			if (objUser == null)
			{
				isSuccess = false;
				return isSuccess;
			}
			try
			{
				// create reset otp that expires after 5 minutes
				objUser.Reset_password_OTP = Utils.GenerateOTP();
				objUser.OTP_expiry_date = DateTime.UtcNow.AddMinutes(5);
				// send email
				_emailService.ResendOTPEmail(objUser,appUrl);
				//Update the user with otp
				_userRepository.Update(objUser);

				return isSuccess;
			}
			catch (Exception ex)
			{
				string msg = ex.Message;
				isSuccess = false;
				return isSuccess;
			}
		}

       
        public bool DeActivateAccount(DeActivateAccountDTO model)
        {
            bool isSuccess = true;
            try
            {
                // Encrypt the old password for verification
                model.Password = CryptoEngine.Encrypt(model.Password);

                // Retrieve the user account using the email and encrypted password
                Fe_users account = _userRepository
                    .GetList(Database.MAIN, UserDAO.DeActivateAccount,new { UserId= model.UserID,Password = model.Password })
                    .FirstOrDefault();

                // If the account is not found, the password is incorrect
                if (account == null)
                {
                    isSuccess = false;
                    return isSuccess;
                }

                account.Is_deleted = false; 

                // Update the user account in the database
                _userRepository.Update(account);

                return isSuccess;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                string msg = ex.Message;
                isSuccess = false;
                return isSuccess;
            }
        }

    }
}
