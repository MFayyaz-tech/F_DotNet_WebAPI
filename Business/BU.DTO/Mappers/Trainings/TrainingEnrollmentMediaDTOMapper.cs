using AutoMapper;
using BU.DTO.DTOs.Trainings;
using DA.Entities.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Trainings
{
    public class TrainingEnrollmentMediaDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_training_enrollment_media, TrainingEnrollmentMediaDTO>()
            .ForMember(dest => dest.MediaId, opt => opt.MapFrom(src => src.Media_id))
            .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.Enrollment_id))
            .ForMember(dest => dest.MediaName, opt => opt.MapFrom(src => src.Media_name))
            .ForMember(dest => dest.MediaPath, opt => opt.MapFrom(src => src.Media_path))
            .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.Media_type))
            .ForMember(dest => dest.MediaCategory, opt => opt.MapFrom(src => src.Media_category))
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
