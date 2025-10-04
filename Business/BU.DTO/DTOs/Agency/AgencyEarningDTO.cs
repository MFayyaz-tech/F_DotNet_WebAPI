using System;
namespace BU.DTO.DTOs.Agency
{
	public class AgencyEarningDTO
	{
        public long AgencyId { get; set; }
        public decimal TotalJobPayment { get; set; }
        public decimal TotalTrainingPayment { get; set; }
        public decimal TotalPaymentEarned { get; set; }
        public decimal TotalJobsDone { get; set; }
        public decimal TotalTrainingDone { get; set; }
      
    }
}

