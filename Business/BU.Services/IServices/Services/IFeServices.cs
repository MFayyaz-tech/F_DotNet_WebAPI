using System;
using System.Collections.Generic;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.Services;

namespace BU.Services.IServices.Services
{
	public interface IFeServices
	{
		FeServicesDTO addService(FeServicesDTO obj);
        bool Update(FeServicesDTO obj);
        
        bool MarkObsulate(FeServicesDTO obj);

        IEnumerable<FeServicesDTO> getCustomerServices();
        IEnumerable<FeServicesDTO> GetAgencyServices(FeServicesDTO obj);
        IEnumerable<FeServicesDTO> GetServiceByCatergoies(FeServicesDTO obj);
        FeServicesDetail GetServiceById(FeServicesDTO obj);


    }
}

