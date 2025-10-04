using System;
using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.FCM;
using BU.DTO.DTOs.ResponseDTO.Job;
using DA.Entities.Jobs;
using DA.Entities.Notifications;

namespace BU.DTO.Mappers.Notifications
{
	public class FeNotificationDTOMapper
	{
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_notifications_tokens, SaveFcmTokenRequestDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
            .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.token))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

        }

    }
}

