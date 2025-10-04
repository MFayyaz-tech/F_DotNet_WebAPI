using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.RequestDTO.Customer;
using BU.DTO.DTOs.Users;
using DTO.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Agency
{
	public interface IAgencyService
	{
		RegisterAgencyRequestDTO RegisterAgency(RegisterAgencyRequestDTO obj);
        RegisterAgencyRequestDTO RegisterAgencyViaGoogle(RegisterAgencyRequestDTO obj);
        AgencyEarningDTO GetAgencyEarning(AgencyEarningDTO obj);
        AgencyBankDetailsDTO AddBankDetail(AgencyBankDetailsDTO obj);
        IEnumerable<AgencyBankDetailsDTO> GetAgencyCard(AgencyBankDetailsDTO obj);
        AgencyJobsDetailDTO GetAgencyJobsDetail(RegisterAgencyRequestDTO obj);

        IEnumerable<AgenciesListReponseDTO> LoadAgenciesList(string[] parameters);
		bool DeleteAgency(RegisterAgencyRequestDTO obj);
        public bool UpdateAgency(UpdateAgencyRequestDTO obj);
        AgencyDTO GetFeAgencyById(AgencyDTO obj);





    }

   

}
