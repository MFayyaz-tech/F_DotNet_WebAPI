using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.ResponseDTO.Job;
using DA.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Jobs
{
    public class FeJobContractDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_job_contract, FeJobContractDTO>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bid_id))
            .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
            .ForMember(dest => dest.ContractProgress, opt => opt.MapFrom(src => src.Contract_progress))
            .ForMember(dest => dest.AgencyFeedback, opt => opt.MapFrom(src => src.Agency_feedback))
            .ForMember(dest => dest.AgencyRating, opt => opt.MapFrom(src => src.Agency_rating))
            .ForMember(dest => dest.CustomerFeedback, opt => opt.MapFrom(src => src.Customer_feedback))
            .ForMember(dest => dest.CustomerRating, opt => opt.MapFrom(src => src.Customer_rating))
            .ForMember(dest => dest.AttachmentMedia, opt => opt.MapFrom(src => src.Attachment_media))
            .ForMember(dest => dest.CancelationReason, opt => opt.MapFrom(src => src.Cancelation_reason))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ReverseMap();

            cfg.CreateMap<Fe_job_contract, JobContractResponseDTO>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
              .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
            .ForMember(dest => dest.ContractProgress, opt => opt.MapFrom(src => src.Contract_progress))
            .ForMember(dest => dest.DurationType, opt => opt.MapFrom(src => src.duration_type))
            .ForMember(dest => dest.BidderType, opt => opt.MapFrom(src => src.bidder_type))
            .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => src.price_type))
            .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date))
            .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
            .ReverseMap();

            cfg.CreateMap<Fe_job_contract, AgencyJobsDetailDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address1))
            .ForMember(dest => dest.AgencyContactPerson, opt => opt.MapFrom(src => src.Agency_contact_person))
             .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.AgencyPhoto, opt => opt.MapFrom(src => src.Agency_photo))
            .ForMember(dest => dest.AgencyProfile, opt => opt.MapFrom(src => src.Agency_profile))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Average_rating))
            .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           
            .ReverseMap();

            cfg.CreateMap<Fe_job_contract, AgencyJobsFeedBack>()
          .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
          .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
           .ForMember(dest => dest.CustomerFeedback, opt => opt.MapFrom(src => src.Customer_feedback))
          .ForMember(dest => dest.CustomerRating, opt => opt.MapFrom(src => src.Customer_rating))
          .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
          .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))   
          .ReverseMap();
        }
    }
}
