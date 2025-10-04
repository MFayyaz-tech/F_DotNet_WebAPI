using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Trainings
{
    public class TrainersDTO : BaseDTO
    {
        public long TrainerId { get; set; }
        public long AgencyId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string UserType { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public string Experience { get; set; }
        public string Intoduction { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Base64Image { get; set; }
        public string PhotoPath { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
