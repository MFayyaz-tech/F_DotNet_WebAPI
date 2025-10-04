using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOs.Users
{
    public class UserAuthDTO
    {
		public string EncUserId { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string RoleId { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string LastLoginDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string GoogleId { get; set; }
        public string LoginType { get; set; }
        public FeCustomerDTO CustomerDetails { get; set; }
        public AgencyDTO AgencyDetails { get; set; }
    }
}
