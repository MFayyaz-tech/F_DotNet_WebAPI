using AutoMapper;
using BU.DTO.DTOs.Customer;
using DA.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Customer
{
    public class FeCustomerDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_customers, FeCustomerDTO>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.First_name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Last_name))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
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

            cfg.CreateMap<Fe_customers, CustomerRegistrationRequestDTO>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.First_name))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Last_name))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))

           .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
           .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
           .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
           .ForMember(dest => dest.Zip_Code, opt => opt.MapFrom(src => src.Zip_code))
           .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
           .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
           .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
           .ForMember(dest => dest.Signature, opt => opt.MapFrom(src => src.Signature))
           .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
           .ReverseMap();
        }
    }
}
