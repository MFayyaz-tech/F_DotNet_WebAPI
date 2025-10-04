using DTO.DTOs.Base;
using System;

namespace DTO.DTOs.Setup
{
    public class RoleDTO : BaseDTO
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string RoleType { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
