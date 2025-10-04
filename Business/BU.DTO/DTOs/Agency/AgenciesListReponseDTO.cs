using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Agency
{
	public class AgenciesListReponseDTO : BaseDTO
	{
		public long AgencyId { get; set; }
		public long UserId { get; set; }
		public string AgencyName { get; set; }
		public string ProfilePath { get; set; } = "";
		public string Address1 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public decimal Lat { get; set; }
		public decimal Lng { get; set; }
	}
}
