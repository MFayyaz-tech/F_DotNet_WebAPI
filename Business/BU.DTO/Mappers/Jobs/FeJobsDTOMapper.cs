using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.ResponseDTO.Job;
using DA.Entities.Jobs;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Jobs
{
    public class FeJobsDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_jobs, FeJobsDTO>()
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer_name))
             .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.CustomerProfile, opt => opt.MapFrom(src => src.Customer_Profile))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
            .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => src.Price_type))
            .ForMember(dest => dest.PriceMin, opt => opt.MapFrom(src => src.Price_min))
            .ForMember(dest => dest.PriceMax, opt => opt.MapFrom(src => src.Price_max))
            .ForMember(dest => dest.DurationType, opt => opt.MapFrom(src => src.Duration_type))
            .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date))
            .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date))
            .ForMember(dest => dest.BidderType, opt => opt.MapFrom(src => src.bidder_type))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.Job_description))
            .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.Job_status))
            .ForMember(dest => dest.JobBidCount, opt => opt.MapFrom(src => src.Job_bid_count))
            .ForMember(dest => dest.AverageBidAmount, opt => opt.MapFrom(src => src.average_bid_amount))
            .ForMember(dest => dest.JobCategory, opt => opt.MapFrom(src => src.Job_category))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_jobs, JobResponseDTO>()
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
              .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
            .ForMember(dest => dest.AgencyRating, opt => opt.MapFrom(src => src.Agency_rating))
            .ForMember(dest => dest.DurationType, opt => opt.MapFrom(src => src.Duration_type))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date))
            .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.Job_description))
            .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.Job_status))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.AgencyPhone, opt => opt.MapFrom(src => src.Agency_phone))
            .ForMember(dest => dest.AgencyProfileImage, opt => opt.MapFrom(src => src.Agency_profile_image))
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
            .ForMember(dest => dest.ContractProgress, opt => opt.MapFrom(src => src.Contract_progress))
            .ForMember(dest => dest.ContractPrice, opt => opt.MapFrom(src => src.Contract_price))
            .ForMember(dest => dest.JobCategory, opt => opt.MapFrom(src => src.Job_category))
            .ReverseMap();

            cfg.CreateMap<Fe_jobs, JobHistoryDTO>()
             .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
             .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
             .ForMember(dest => dest.ContractProgress, opt => opt.MapFrom(src => src.Contract_progress))
             .ForMember(dest => dest.ContractNote, opt => opt.MapFrom(src => src.Contract_notes))
             .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
             .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.Job_status))
             .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
             .ReverseMap();

            cfg.CreateMap<Fe_jobs, FeGetAssignJobDTO>()
              .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
              .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
              .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
              .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer_name))
              .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
              .ForMember(dest => dest.ContractProgress, opt => opt.MapFrom(src => src.Contract_progress))
              .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
              .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
              .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.Job_status))
              .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Agent_id))
              .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent_name))
              .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date))
              .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date))
              .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
              .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
              .ForMember(dest => dest.DurationType, opt => opt.MapFrom(src => src.Duration_type))
              .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => src.Price_type))
              .ForMember(dest => dest.PriceMax, opt => opt.MapFrom(src => src.Price_max))
              .ForMember(dest => dest.PriceMin, opt => opt.MapFrom(src => src.Price_min))
              .ReverseMap();




        }
    }
}
