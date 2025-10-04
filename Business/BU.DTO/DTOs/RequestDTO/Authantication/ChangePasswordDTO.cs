using System;
using System.ComponentModel.DataAnnotations;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.RequestDTO.Authantication
{
	public class ChangePasswordDTO : BaseDTO
	{
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}

