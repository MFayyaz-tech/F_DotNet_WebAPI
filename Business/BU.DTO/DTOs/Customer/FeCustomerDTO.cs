using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Customer
{
    public class FeCustomerDTO : BaseDTO
    {
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Signature { get; set; }
        public string PhotoPath { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
