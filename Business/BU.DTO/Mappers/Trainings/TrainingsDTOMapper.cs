using AutoMapper;
using BU.DTO.DTOs.ResponseDTO.Testimonials;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using DA.Entities.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Trainings
{
    public class TrainingsDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_trainings, TrainingsDTO>()
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training_title))
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.Trainer_id))
            .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date))
            .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.Fee, opt => opt.MapFrom(src => src.Fee))
            .ForMember(dest => dest.LocationLat, opt => opt.MapFrom(src => src.Location_lat))
            .ForMember(dest => dest.LocationLng, opt => opt.MapFrom(src => src.Location_lng))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
            .ForMember(dest => dest.TrainingCategory, opt => opt.MapFrom(src => src.Training_category))
            .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => src.Training_status))
            .ForMember(dest => dest.IsApprovalRequired, opt => opt.MapFrom(src => src.Is_approval_required))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
             .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Average_rating))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();
           


            cfg.CreateMap<Fe_trainings, TrainingDetailResponseDTO>()
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.AgencyPhoto, opt => opt.MapFrom(src => src.Agency_photo))
         .ForMember(dest => dest.MediaPath, opt => opt.MapFrom(src => src.Media_path))
            .ForMember(dest => dest.AgencyPhone, opt => opt.MapFrom(src => src.Agency_phone))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training_title))
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.Trainer_id))
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer_name))
            .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date))
            .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.Fee, opt => opt.MapFrom(src => src.Fee))
            .ForMember(dest => dest.LocationLat, opt => opt.MapFrom(src => src.Location_lat))
            .ForMember(dest => dest.LocationLng, opt => opt.MapFrom(src => src.Location_lng))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
             .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Average_rating))
             .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.TrainingCategory, opt => opt.MapFrom(src => src.Training_category))
                        .ForMember(dest => dest.isApprovalRequired, opt => opt.MapFrom(src => src.Is_approval_required))
            .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => src.Training_status))
            .ForMember(dest => dest.TrainingProgress, opt => opt.MapFrom(src => src.Training_progress))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.TotalEnrolledCustomers, opt => opt.MapFrom(src => src.Total_enrolled_customers))
            .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.Enrolment_id))
            .ForMember(dest => dest.EnrollmentStatus, opt => opt.MapFrom(src => src.Enrollment_status))
            .ForMember(dest => dest.TrainingEnrollDate, opt => opt.MapFrom(src => src.Training_enroll_date))

            .ReverseMap();



            cfg.CreateMap<Fe_trainings, TestimonialsDTO>()
            .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Rating_count))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Average_rating))
            .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training_title))
            .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => src.Training_status))
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer_name))
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
                .ReverseMap();


            cfg.CreateMap<Fe_trainings, TestimonialsDetails>()
            .ForMember(dest => dest.AttachmentMedia, opt => opt.MapFrom(src => src.Attachment_media))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.FeedBackComment, opt => opt.MapFrom(src => src.Feedback))
            .ForMember(dest => dest.FeedBackRating, opt => opt.MapFrom(src => src.Feedback_rating))
            .ForMember(dest => dest.TrainingFeedbackId, opt => opt.MapFrom(src => src.Training_feedback_id))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer_Name))
              .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
                .ReverseMap();

            cfg.CreateMap<Fe_feedback_reply, FeedbackReplyDTO>()
     .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
     .ForMember(dest => dest.TrainingFeedbackId, opt => opt.MapFrom(src => src.Training_feedback_id))
     .ForMember(dest => dest.MessageReply, opt => opt.MapFrom(src => src.Message_reply))
     .ReverseMap();

        }
    }
}
