using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BU.DTO.DTOs.Agency
{
	public class AgencyLicenseDTO : BaseDTO
	{
		public long LicenseId { get; set; }
		public long AgencyId { get; set; }
		public string LicenseName {get;set;}
        public string LicenseType { get; set; }
        public string LicenseNumber { get; set; }
        public string IssuingAuthority { get; set; }
		public string ExpiryDate { get; set; }
		public string LicenseState { get; set; }
		public string LicenseFrontImagePath { get; set; }
		public string LicenseBackImagePath { get; set; }
		public string LicenseFrontBase64 { get; set; }
		public string LicenseBackBase64 { get; set; }
		public bool IsDefault { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }

	}
}
