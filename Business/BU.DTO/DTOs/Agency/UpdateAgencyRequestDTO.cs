using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.Agency
{
	public class UpdateAgencyRequestDTO: BaseDTO
    {
        public long AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencySite { get; set; }
        public string Phone { get; set; }
        public string AgencyProfile { get; set; }
        public string AgencySupportEmail { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Base64Image { get; set; }
    }
}

