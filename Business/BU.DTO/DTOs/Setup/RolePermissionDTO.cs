using DTO.DTOs.Base;
using System;

namespace DTO.DTOs.Setup
{
    public class RolePermissionDTO : BaseDTO
    {
        public int RolePermissionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        
    }
}
