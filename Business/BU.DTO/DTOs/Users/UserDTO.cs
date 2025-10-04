using DTO.DTOs.Base;
using System;

namespace DTO.DTOs.User
{
    public class UserDTO : BaseDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public long RoleId { get; set; }
        public string ApprovalStatus { get; set; }
        public string RejectedReason { get; set; }
        public string Status { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public string ResetPasswordOTP { get; set; }
        public DateTime OTPExpiryDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
