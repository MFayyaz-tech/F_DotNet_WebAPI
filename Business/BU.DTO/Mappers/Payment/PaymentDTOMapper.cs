using System;
using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Payments;
using BU.DTO.DTOs.RequestDTO.FCM;
using DA.Entities.Billing;
using DA.Entities.Notifications;

namespace BU.DTO.Mappers.Payment
{
	public class PaymentDTOMapper
	{
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_payment, PaymentDTO>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment_id))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Payment_status))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.Payment_type))
            .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bid_id))
            .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Training_id))
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Transaction_id))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.Card_id))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_payment, AgencyEarningDTO>()
          .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
          .ForMember(dest => dest.TotalJobPayment, opt => opt.MapFrom(src => src.Total_job_payment))
          .ForMember(dest => dest.TotalJobsDone, opt => opt.MapFrom(src => src.Total_jobs_done))
          .ForMember(dest => dest.TotalPaymentEarned, opt => opt.MapFrom(src => src.Total_payment_earned))
          .ForMember(dest => dest.TotalTrainingDone, opt => opt.MapFrom(src => src.Total_training_done))
          .ForMember(dest => dest.TotalTrainingPayment, opt => opt.MapFrom(src => src.Total_training_payment))
    
          .ReverseMap();

        }

    }
}

