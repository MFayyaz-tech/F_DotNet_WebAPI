using System;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.Services
{
	public class FeServicesDTO : BaseDTO
	{
        public long ServicesId { get; set; }
        public long AgencyId { get; set; }
        public decimal Price { get; set; }
        public string ServiceTitle { get; set; }
        public string PriceType { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceBanner { get; set; }
        public long IsObsulate { get; set; }
        public long CategoryId { get; set; }
        public decimal Discount { get; set; }





    }
}

