using AutoMapper;
using BU.DTO.DTOs.Agency;
using DA.Entities.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Agency
{
    public class FeAgencyLicenseDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_agency_license, AgencyLicenseDTO>()
            .ForMember(dest => dest.LicenseId, opt => opt.MapFrom(src => src.License_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.LicenseName, opt => opt.MapFrom(src => src.License_name))
            .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => src.License_type))
            .ForMember(dest => dest.IssuingAuthority, opt => opt.MapFrom(src => src.Issuing_authority))
            .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.Expiry_date))
            .ForMember(dest => dest.LicenseState, opt => opt.MapFrom(src => src.License_state))
            .ForMember(dest => dest.LicenseFrontImagePath, opt => opt.MapFrom(src => src.License_front_image_path))
            .ForMember(dest => dest.LicenseBackImagePath, opt => opt.MapFrom(src => src.License_back_image_path))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.Is_default))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();
        }
    }
}
