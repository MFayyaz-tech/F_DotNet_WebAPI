using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Agency
{
	public class RegisterAgencyRequestDTO : BaseDTO
	{
		public long AgencyId { get; set; }
		public long UserId { get; set; }
		public string AgencyName { get; set; }
		public string AgencySite { get; set; }
		public string AgencySupportEmail { get; set; }
		public string AgencyFax { get; set; }
		public string AgencyProfile { get; set; }
		public string AgencyContactPerson { get; set; }
		public string EmailAddress { get; set; }
		public string Phone { get; set; }
		public string Address1 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Country { get; set; }
		public string Signature { get; set; }
		public decimal Lat { get; set; }
		public decimal Lng { get; set; }
		public string Password { get; set; }
		public string PhotoPath { get; set; }
		public long RoleId { get; set; }
		

        public string Base64Image { get; set; }
		public string UserType { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
        public string LoginType { get; set; }
        public string GoogleId { get; set; }

        public List<AgencyLicenseDTO> AgencyLicenses { get; set; }
		public List<AgencyBankDetailsDTO> AgencyBankDetails { get; set; }
	}
}
