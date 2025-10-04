using AutoMapper;
using BU.DTO.DTOs.Agency;
using DA.Entities.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Agency
{
    public class AgencyBankDetailsDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_agency_bank_details, AgencyBankDetailsDTO>()
            .ForMember(dest => dest.BankId, opt => opt.MapFrom(src => src.Bank_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.Bank_name))
            .ForMember(dest => dest.AccountTitle, opt => opt.MapFrom(src => src.Account_title))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account_number))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
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
