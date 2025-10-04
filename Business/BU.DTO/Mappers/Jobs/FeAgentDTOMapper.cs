using System;
using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.ResponseDTO.Bids;
using BU.DTO.DTOs.ResponseDTO.Job;
using DA.Entities.Jobs;

namespace BU.DTO.Mappers.Jobs
{
	public class FeAgentDTOMapper
	{
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_agent, AgentsDTO>()
           .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Agent_id))
           .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.First_name))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Last_name))
           .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
           .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.License_number))
           .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experince))
           .ForMember(dest => dest.Intoduction, opt => opt.MapFrom(src => src.Introduction))
           .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
           .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
           .ForMember(dest => dest.TotalCompletedJob, opt => opt.MapFrom(src => src.Total_completed_jobs))
           .ForMember(dest => dest.JobAssign, opt => opt.MapFrom(src => src.Job_assign))
           .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Zip_code))
           .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
           .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
           .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
           .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))

           .ReverseMap();


         cfg.CreateMap<Fe_agent, FeAgentsDetailDTO>()
        .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Agent_id))
        .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.First_name))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Last_name))

        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
        .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email_address))
        .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.License_number))
        .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
        .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
        .ForMember(dest => dest.TotalCompletedJob, opt => opt.MapFrom(src => src.Total_completed_jobs))
          .ForMember(dest => dest.InProgressJob, opt => opt.MapFrom(src => src.In_progress_Jobs))
             .ForMember(dest => dest.CancelledJob, opt => opt.MapFrom(src => src.Cancelled_jobs))
        .ForMember(dest => dest.Zip_Code, opt => opt.MapFrom(src => src.Zip_code))
        .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
        .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
        .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
        .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
        .ReverseMap();

            cfg.CreateMap<Fe_job_contract, FeAgentsReviewDTO>()
           .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract_id))
           .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
           .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
           .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Agent_id))
                         .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer_name))
           .ForMember(dest => dest.CustomerPhoto, opt => opt.MapFrom(src => src.Customer_photo))

           .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bid_id))
           .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
           .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.Contract_status))
           .ForMember(dest => dest.CustomerFeedback, opt => opt.MapFrom(src => src.Customer_feedback))
           .ForMember(dest => dest.CustomerRating, opt => opt.MapFrom(src => src.Customer_rating))
           .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
           .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.Job_description)) 
           .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.Job_status)) 
           .ForMember(dest => dest.DurationType, opt => opt.MapFrom(src => src.duration_type))
           .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.From_date.HasValue ? src.From_date.Value : DateTime.MinValue))
           .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.To_date.HasValue ? src.To_date.Value : DateTime.MinValue))
           .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => src.price_type))
           .ForMember(dest => dest.PriceMin, opt => opt.MapFrom(src => src.Price_min))
           .ForMember(dest => dest.PriceMax, opt => opt.MapFrom(src => src.Price_Max)).ReverseMap();



        }


    }
}

