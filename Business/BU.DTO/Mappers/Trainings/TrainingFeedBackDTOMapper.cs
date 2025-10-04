using AutoMapper;
using BU.DTO.DTOs.ResponseDTO.Trainings;
using BU.DTO.DTOs.Trainings;
using DA.Entities.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Trainings
{
    public class TrainingFeedBackDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_training_feedback, TrainingFeedBackDTO>()
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training_Title))
            .ForMember(dest => dest.TrainingFeedBackId, opt => opt.MapFrom(src => src.Training_feedback_id))

            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer_name))
            .ForMember(dest => dest.FeedBack, opt => opt.MapFrom(src => src.Feedback))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.AttachmentMedia, opt => opt.MapFrom(src => src.Attachment_media))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_feedback_reply, FeedbackReplyDTO>()
            .ForMember(dest => dest.MessageReply, opt => opt.MapFrom(src => src.Message_reply))
             .ForMember(dest => dest.TrainingFeedbackId, opt => opt.MapFrom(src => src.Training_feedback_id))
             .ForMember(dest => dest.Reply_id, opt => opt.MapFrom(src => src.Reply_id))

            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();




        }
    }
}
