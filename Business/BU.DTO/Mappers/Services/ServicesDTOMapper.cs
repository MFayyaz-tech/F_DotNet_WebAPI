using System;
using AutoMapper;
using BU.DTO.DTOs.Payments;
using BU.DTO.DTOs.Services;
using DA.Entities.Billing;
using DA.Entities.Services;

namespace BU.DTO.Mappers.Services
{
    public class ServicesDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_services, FeServicesDTO>()
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ServiceBanner, opt => opt.MapFrom(src => src.Service_banner))
            .ForMember(dest => dest.ServiceDescription, opt => opt.MapFrom(src => src.Service_description))
            .ForMember(dest => dest.ServiceTitle, opt => opt.MapFrom(src => src.Service_title))
             .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
             .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category_id))
            .ForMember(dest => dest.ServicesId, opt => opt.MapFrom(src => src.Services_id))
             .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => src.Price_type))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.IsObsulate, opt => opt.MapFrom(src => src.Is_obsulate))

            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();


            cfg.CreateMap<Fe_services, FeServicesDetail>()
           .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
           .ForMember(dest => dest.ServiceBanner, opt => opt.MapFrom(src => src.Service_banner))
           .ForMember(dest => dest.ServiceDescription, opt => opt.MapFrom(src => src.Service_description))
           .ForMember(dest => dest.ServiceTitle, opt => opt.MapFrom(src => src.Service_title))
           .ForMember(dest => dest.ServicesId, opt => opt.MapFrom(src => src.Services_id))
            .ForMember(dest => dest.IsObsulate, opt => opt.MapFrom(src => src.Is_obsulate))
           .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => src.Price_type))
           .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company_name))
           .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
           .ReverseMap();

        }

    }
}

