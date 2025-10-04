using System;
using BU.DTO.DTOs.Trainings;
using System.Collections.Generic;
using DTO.DTOs.Base;

namespace BU.DTO.DTOs.Agency
{
	public class AgencyJobsDetailDTO : BaseDTO
	{

        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string AgencyPhoto { get; set; }
        public string ContractStatus { get; set; }
        public long AgencyId { get; set; }
        public string AgencyContactPerson { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string AgencyProfile { get; set; }
        public decimal AverageRating { get; set; }
        public List<AgencyJobsFeedBack> AgencyFeedBack { get; set; }


    }

    public class AgencyJobsFeedBack : BaseDTO
    {
        public string JobTitle { get; set; }
        public long CustomerRating { get; set; }
        public string CustomerFeedback { get; set; }
        public string ContractStatus { get; set; }
        public long ContractId { get; set; }


    }
}

