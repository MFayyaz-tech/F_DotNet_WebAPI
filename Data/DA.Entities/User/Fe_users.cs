using Dapper.Contrib.Extensions;
using Entities.Base;
using System;

namespace Entities.Users
{
    [Table("fe_users")]
    public class Fe_users : BaseEntity
    {
        [Key]
        public long User_id { get; set; }
        public string User_name { get; set; }
        public string Email_address { get; set; }
        public string Password { get; set; }
        public string User_type { get; set; }
        public long Role_id { get; set; }
        public string Approval_status { get; set; }
        public string Rejected_reason { get; set; }
        public string Status { get; set; }
        public string Login_type { get; set; }
        public string Social_id { get; set; }

        public string Reset_password_token { get; set; }
        public DateTime? Token_expiry_date { get; set; }
        public string Reset_password_OTP { get; set; }
        public DateTime? OTP_expiry_date { get; set; }
        public DateTime? Last_login_date { get; set; }
        public bool Is_active { get; set; }



    }
}
