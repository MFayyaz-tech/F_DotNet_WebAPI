using AutoMapper;
using BU.DTO.DTOs.RequestDTO.Trainings;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using DA.Entities.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Trainings
{
    public class TrainingEnrollmentDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_training_enrollment, TrainingEnrollmentDTO>()
            .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.Enrollment_id))
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.EnrollmentStatus, opt => opt.MapFrom(src => src.Enrollment_status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.Rejection_reason))
            .ForMember(dest => dest.EnrollmentDate, opt => opt.MapFrom(src => src.Enrollment_date))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_training_enrollment, TrainingEnrolRequestDTO>()
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_training_enrollment, TrainingEnrollmentsResponseDTO>()
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training_title))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company_name))
            .ForMember(dest => dest.CompanyProfilePhoto, opt => opt.MapFrom(src => src.Company_profile_photo))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer_name))
            .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Zip_code))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.Enrollment_id))
            .ForMember(dest => dest.EnrollmentStatus, opt => opt.MapFrom(src => src.Enrollment_status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.Rejection_reason))
            .ForMember(dest => dest.EnrollmentDate, opt => opt.MapFrom(src => src.Enrollment_date))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_training_enrollment, CustomerCompletedTrainingsResponseDTO>()
           .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
           .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training_title))
           .ForMember(dest => dest.Enrollment_status, opt => opt.MapFrom(src => src.Enrollment_status))
           .ForMember(dest => dest.FeedbackCount, opt => opt.MapFrom(src => src.Feedback_count))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))

           .ReverseMap();

        }
    }
}
