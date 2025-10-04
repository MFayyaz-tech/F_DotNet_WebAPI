using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.Services;
using BU.Services.IServices.Services;
using Common;
using Common.Helper;
using DA.DAO.DAO.Jobs;
using DA.DAO.DAO.Services;
using DA.Entities.Jobs;
using DA.Entities.Services;
using DAO;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BU.Services.Services.Services
{
    public class FeServices : IFeServices

    {
        private readonly IRepository<Fe_services> _FeServicesRepository;

        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FeServices(
            IRepository<Fe_services> FeServicesRepository,
            IMapper mapper, IConfiguration configuration)
        {
            _FeServicesRepository = FeServicesRepository;
            _configuration = configuration;
            _mapper = mapper;
        }






        public FeServicesDTO addService(FeServicesDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }


            // Handle image processing
            if (!string.IsNullOrEmpty(obj.ServiceBanner))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.ServiceBanner.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                long nextIdentity = _FeServicesRepository.GetNextIdentityId("fe_services");
                string folderPath = $"\\Documents\\Services\\{nextIdentity}";
                string fullPath = Path.Combine(rootPath, folderPath);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(fullPath, fileName);

                File.WriteAllBytes(filePath, imageBytes);
                obj.ServiceBanner = Path.Combine(folderPath, fileName);

            }

            // Map DTO to Entity
            Fe_services ent = _mapper.Map<FeServicesDTO, Fe_services>(obj);

            obj.ServicesId = _FeServicesRepository.Insert(ent);
            obj.IsObsulate = 0;

            return obj;
        }

        public IEnumerable<FeServicesDTO> getCustomerServices()
        {

            var customerServices = _FeServicesRepository.GetList(Database.MAIN, FeServicesDAO.getCustomerServices).ToList();
            //if (customerServices == null || !customerServices.Any())
            //{
            //    throw new InvalidOperationException("Customer services not found.");
            //}

            var servicesDto = _mapper.Map<IEnumerable<Fe_services>, IEnumerable<FeServicesDTO>>(customerServices);

            return servicesDto;
        }

        public IEnumerable<FeServicesDTO> GetAgencyServices(FeServicesDTO obj)
        {

           

            var customerServices = _FeServicesRepository.GetList(Database.MAIN, FeServicesDAO.getAgencyServices, new { AgencyId =obj.AgencyId , isObsulate = obj.IsObsulate }).ToList();


            var servicesDto = _mapper.Map<IEnumerable<Fe_services>, IEnumerable<FeServicesDTO>>(customerServices);

            return servicesDto;
        }

        public FeServicesDetail GetServiceById(FeServicesDTO obj)
        {
            var customerServices = _FeServicesRepository.GetList(Database.MAIN, FeServicesDAO.GetServiceDetailById, new { ServiceId = obj.ServicesId, isObsulate = obj.IsObsulate }).FirstOrDefault();
            var servicesDto = _mapper.Map<Fe_services, FeServicesDetail>(customerServices);
            return servicesDto;
        }

        public bool Update(FeServicesDTO obj)
        {
            var existingServices = _FeServicesRepository.GetList(Database.MAIN, FeServicesDAO.GetServiceDetailById, new { ServiceId = obj.ServicesId }).FirstOrDefault();

            if (existingServices != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.EncUserID))
                {
                    existingServices.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
                }

                existingServices.Price_type = obj.PriceType;
                existingServices.Service_title = obj.ServiceTitle;
                existingServices.Service_description = obj.ServiceDescription;
                existingServices.Service_description = obj.ServiceDescription;
                existingServices.Price = obj.Price;
                existingServices.Service_banner = obj.ServiceBanner;

                _FeServicesRepository.Update(existingServices);
                return true; 
            }
            return false;
        }

        public bool MarkObsulate(FeServicesDTO obj)
        {
            var existingServices = _FeServicesRepository.GetList(Database.MAIN, FeServicesDAO.GetServiceDetailById, new { ServiceId = obj.ServicesId }).FirstOrDefault();

            if (existingServices != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.EncUserID))
                {
                    existingServices.Updated_by = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
                }

                existingServices.Is_obsulate = obj.IsObsulate;

                _FeServicesRepository.Update(existingServices);
                return true;
            }
            return false;
        }

        public IEnumerable<FeServicesDTO> GetServiceByCatergoies(FeServicesDTO obj)
        {

            var customerServices = _FeServicesRepository.GetList(Database.MAIN, FeServicesDAO.getServicesByCategories, new { CategoryId = obj.CategoryId, isObsulate = obj.IsObsulate }).ToList();
            var servicesDto = _mapper.Map<IEnumerable<Fe_services>, IEnumerable<FeServicesDTO>>(customerServices);

            return servicesDto;
        }
    }
}

