using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Customer
{
    public class CustomerRegistrationRequestDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public long UserId { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Signature { get; set; }
        public int RoleId { get; set; }
        public string UserType { get; set; }
        public string PhotoPath { get; set; }
        public string LoginType { get; set; }
        public string GoogleId { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string Base64Image { get; set; }
        public List<FeCustomerCardsDTO> CustomerCards { get; set; }
    }
}
