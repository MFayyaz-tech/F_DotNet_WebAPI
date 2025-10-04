using DTO.DTOs.Users;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Core
{
    public class UserContext
    {
        public string AuthToken { get; set; }
        public UserAuthDTO User { get; set; }
    }
}
