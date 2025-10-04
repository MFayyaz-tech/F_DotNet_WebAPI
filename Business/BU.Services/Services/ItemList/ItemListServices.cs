using AutoMapper;

using BU.DTO.DTOs.ListItem;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using BU.Services.IServices.Agency;
using Common;
using DA.DAO.DAO.ListItems;
using DA.DAO.DAO.Trainings;
using DA.Entities.ItemList;
using DA.Entities.Trainings;
using DAO;
using Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BU.Services.Services.Customer
{
    public class FeItemListService : IItemListService
    {
        private readonly IRepository<Fe_item_list> _FeItemRepository;

        IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogging _logging;
        public FeItemListService(IRepository<Fe_item_list> feItemRepository,  IMapper mapper, IConfiguration configuration, ILogging logging)
        {
            _FeItemRepository = feItemRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logging = logging;
        }

        public AllListItemDTO GetListItems(string[] parameters)
        {
            var addJobTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetAddJobTypeQuery, new { });
            var licenceTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetLicenceTypeQuery, new { });
            var agencyBidTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetAgencyBidTypeQuery, new { });
            var addTrainingTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetAddTrainingTypeQuery, new { });
            var addTrainerExperienceListTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetAddTrainerExperienceListTypeQuery, new { });
            var addTrainerSkillsTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetAddTrainerSkillsTypeQuery, new { });
            var cancelJobTypeItems = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetCancelJobTypeQuery, new { });
            var serviceCategories = _FeItemRepository.GetList(Database.MAIN, FeListItemDAO.GetServiceCategroy, new { });


            var allListItems = new AllListItemDTO
            {
                AddJobType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(addJobTypeItems),
                LicenceType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(licenceTypeItems),
                AgencyBidType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(agencyBidTypeItems),
                AddTrainingType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(addTrainingTypeItems),
                AddTrainerExperinceListType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(addTrainerExperienceListTypeItems),
                AddTrainerSkillsType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(addTrainerSkillsTypeItems),
                CancelJobType = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(cancelJobTypeItems),
                ServiceCategries = _mapper.Map<IEnumerable<Fe_item_list>, List<ListItemDTO>>(serviceCategories),
            };

            return allListItems;
        }

     
    }
}
