using AutoMapper;
using BU.DTO.DTOs.Agency;
using DA.Entities.Agency;
using DTO.DTOs.User;
using DTO.DTOs.Users;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Agency
{
    public class AgencyDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_agency, AgencyDTO>()
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Company_name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.AgencySite, opt => opt.MapFrom(src => src.Agency_site))
                .ForMember(dest => dest.AgencySupportEmail, opt => opt.MapFrom(src => src.Agency_support_email))
                .ForMember(dest => dest.AgencyFax, opt => opt.MapFrom(src => src.Agency_fax))
                .ForMember(dest => dest.AgencyProfile, opt => opt.MapFrom(src => src.Agency_profile))
                .ForMember(dest => dest.AgencyContactPerson, opt => opt.MapFrom(src => src.Agency_contact_person))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Zip_code))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
                .ForMember(dest => dest.Signature, opt => opt.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
                .ReverseMap();

            cfg.CreateMap<Fe_agency, RegisterAgencyRequestDTO>()
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Company_name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.AgencySite, opt => opt.MapFrom(src => src.Agency_site))
                .ForMember(dest => dest.AgencySupportEmail, opt => opt.MapFrom(src => src.Agency_support_email))
                .ForMember(dest => dest.AgencyFax, opt => opt.MapFrom(src => src.Agency_fax))
                .ForMember(dest => dest.AgencyProfile, opt => opt.MapFrom(src => src.Agency_profile))
                .ForMember(dest => dest.AgencyContactPerson, opt => opt.MapFrom(src => src.Agency_contact_person))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Zip_code))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
                .ForMember(dest => dest.Signature, opt => opt.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
                .ReverseMap();

            cfg.CreateMap<Fe_agency, AgenciesListReponseDTO>()
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Company_name))
                .ForMember(dest => dest.ProfilePath, opt => opt.MapFrom(src => src.Photo_path))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Zip_code))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
                .ReverseMap();


    }
}
}
