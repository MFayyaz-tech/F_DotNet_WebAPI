using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BU.DTO.DTOs.Common.Account
{
    public class ChangePasswordRequest
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string OldPassword { get; set; }

        [Required]
        //[MinLength(6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class DeActivateAccountDTO
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public long UserID { get; set; }
    }

}
