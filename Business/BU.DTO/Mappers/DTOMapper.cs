using AutoMapper;
using DTO.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using DTO.Mappers.User;
using DTO.Mappers.Setup;
using BU.DTO.Mappers.Agency;
using BU.DTO.Mappers.Jobs;
using BU.DTO.Mappers.Customer;
using BU.DTO.Mappers.Trainings;
using BU.DTO.Mappers.Chat;
using BU.DTO.Mappers.Notifications;
using BU.DTO.Mappers.Payment;
using BU.DTO.Mappers.Services;

namespace DTO.Mappers
{
    public class DTOMapper
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                DDLDTOMapper.Mapping(cfg);
                RoleDTOMapper.Mapping(cfg);
                RolePermissionDTOMapper.Mapping(cfg);
                UserDTOMapper.Mapping(cfg);
                FeCustomerDTOMapper.Mapping(cfg);
                FeCustomerCardsDTOMapper.Mapping(cfg);
                AgencyDTOMapper.Mapping(cfg);
                AgencyBankDetailsDTOMapper.Mapping(cfg);
                FeAgencyLicenseDTOMapper.Mapping(cfg);
                FeJobsDTOMapper.Mapping(cfg);
                FeJobBidDTOMapper.Mapping(cfg);
                FeJobContractDTOMapper.Mapping(cfg);
                FeJobContractProgressDTOMapper.Mapping(cfg);
                TrainingsDTOMapper.Mapping(cfg);
                TrainingMediaDTOMapper.Mapping(cfg);
                TrainingEnrollmentDTOMapper.Mapping(cfg);
                TrainingEnrollmentMediaDTOMapper.Mapping(cfg);
                TrainingFeedBackDTOMapper.Mapping(cfg);
                TrainersDTOMapper.Mapping(cfg);
                FeNotificationDTOMapper.Mapping(cfg);
                FeChatDTOMapper.Mapping(cfg);
                ListItemDTOMapper.Mapping(cfg);
                PaymentDTOMapper.Mapping(cfg);
                ServicesDTOMapper.Mapping(cfg);
                FeAgentDTOMapper.Mapping(cfg);

            });
            return config;
        }
    }
}
