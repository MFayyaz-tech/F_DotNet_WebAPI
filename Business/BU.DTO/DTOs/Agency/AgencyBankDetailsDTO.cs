using DTO.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.DTOs.Agency
{
	public class AgencyBankDetailsDTO : BaseDTO
	{
        public long BankId { get; set; }
        public long AgencyId { get; set; }
        public string AccountTitle { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
	}
}
