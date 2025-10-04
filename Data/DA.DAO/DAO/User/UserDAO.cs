using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.DAO.User
{
    public class UserDAO : IDAO
    {
        public static string IsEmailExistQuery
        {
            get
            {
                return @"select *  from [fe_users] u where u.email_address = @Email  and is_active = 1";
            }
        }

		public static string IsUserExistQuery
		{
			get
			{
				return @"select *  from [fe_users] u where u.email_address = @Email AND u.user_id != @UserId AND ISNULL(u.is_deleted,0) = 0";
			}
		}

        public static string UpdateUserPassword
        {
            get
            {
                return @"UPDATE fe_users SET [password] = @Password WHERE user_id = @UserId";
            }
        }

        public static string LogInQuery
        {
            get
            {
                return @"select  u.* 
                        from fe_users u 
                        where u.email_address = @Email and u.password = @Password 
                        and isnull(u.is_deleted,0) = 0 and isnull(u.is_active,0) = 1";
            }
           
        }

        public static string LogInEmailQuery
        {
            get
            {
                return @"select  u.* 
                        from fe_users u 
                        where u.email_address = @Email  
                        and isnull(u.is_deleted,0) = 0 and isnull(u.is_active,0) = 1";
            }

        }



        public static string UpdatedLastLoginQuery
        {
            get
            {
                return @"update fe_users set last_login_date = getdate() where user_id = @UserID";
            }
        }



        public string GetSingleQuery => "select top 1 from fe_users where u.email_address = @Email and u.password = @password";
        public string GridDataQuery => @"select * from fe_users where isnull(is_deleted, 0) <> 1 ";

		public static string GetUserById
		{
			get
			{
				return "select top(1) * from [fe_users] u where u.user_id = @UserId and isnull( u.is_deleted, 0 ) = 0";
			}
		}
        public static string GetCustomerById
        {
            get
            {
                return "select top(1) * from [fe_users] u where u.user_id = @CustomerId and isnull( u.is_deleted, 0 ) = 0";
            }
        }


        public static string GetUserByEmail
        {
            get
            {
                return "select top(1) * from [fe_users] u where u.email_address = @Email and isnull( u.is_deleted, 0 ) = 0";
            }
        }

        public static string VerifyUserTokenToResetPassword
        {
            get
            {
                return @"select top(1)* from [fe_users] u 
                            where u.reset_password_token = @token 
                            and token_expiry_date > GETUTCDATE()";
            }
        }

		public static string VerifyUserOTPToResetPassword
		{
			get
			{
				return @"select top(1) * from [fe_users] u 
                            where u.reset_password_otp = @OTP 
                            and u.otp_expiry_date > GETUTCDATE()";
			}
		}

        public static string DeActivateAccount
        {
            get
            {
                return @"SELECT * FROM Fe_users WHERE user_id = @UserId AND Password = @Password";
            }
        }

        public static string ResetUserPasswordByToken
        {
            get
            {
                return "select top(1)* from [fe_users] u where u.reset_password_token = '{0}' and isnull( u.is_deleted, 0 ) = 0";
            }
        }
        public static string GetUserToChangePassword
        {
            get
            {
                return "select top(1)* from [fe_users] u where u.email_address = '{0}' and u.password = '{1}' and isnull( u.is_deleted, 0 ) = 0";
            }
        }
        public static string GetAgencyByUserId
        {
            get
            {
                return "select fu.user_id from fe_agency fa join fe_users fu ON fu.user_id = fa.user_id where fa.agency_id = @AgencyId";
            }
        }

        public static string GetCustomerByUserId
        {
            get
            {
                return "select fu.user_id from fe_customer fa join fe_users fu ON fu.user_id = fa.user_id where fa.customer_id = @CustomerId";
            }
        }

      

        public static string GetUserByUserId => @"select * FROM fe_users  where user_id = @userId";

		public string DoArchiveQuery => throw new NotImplementedException();

        public string GetAllQyery => throw new NotImplementedException();
    }
}
