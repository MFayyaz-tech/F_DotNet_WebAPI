using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.Services
{
	public class FeServicesDetail : BaseDTO
	{
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string UserId { get; set; }
        public long ServicesId { get; set; }
        public long AgencyId { get; set; }
        public decimal Price { get; set; }
        public string ServiceTitle { get; set; }
        public string PriceType { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceBanner { get; set; }
        public long IsObsulate { get; set; }
        public decimal Discount { get; set; }


    }
}

