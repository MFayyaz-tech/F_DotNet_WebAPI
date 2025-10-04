using AutoMapper;
using BU.DTO.DTOs.Jobs;
using DA.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Jobs
{
    public class FeJobContractProgressDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_job_contract_progress, FeJobContractProgressDTO>()
            .ForMember(dest => dest.ContractProgressId, opt => opt.MapFrom(src => src.Contract_progress_id))
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
            .ForMember(dest => dest.ContractProgress, opt => opt.MapFrom(src => src.Contract_progress))
            .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
            .ForMember(dest => dest.ContractNotes, opt => opt.MapFrom(src => src.Contract_notes))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

        }
    }
}
