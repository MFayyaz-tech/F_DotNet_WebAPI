using AutoMapper;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.ResponseDTO.Bids;
using DA.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Jobs
{
    public class FeJobBidDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_job_bid, FeJobBidDTO>()
            .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bid_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
            .ForMember(dest => dest.BidAmount, opt => opt.MapFrom(src => src.Bid_amount))
            .ForMember(dest => dest.BidDate, opt => opt.MapFrom(src => src.Bid_date))
            .ForMember(dest => dest.BidType, opt => opt.MapFrom(src => src.Bid_type))
            .ForMember(dest => dest.BidNotes, opt => opt.MapFrom(src => src.Bid_notes))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Is_deleted))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Create_date))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Created_by))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_date))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Updated_by))
            .ReverseMap();

            cfg.CreateMap<Fe_job_bid, JobBidsListResponseDTO>()
            .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bid_id))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency_name))
            .ForMember(dest => dest.AgencyLat, opt => opt.MapFrom(src => src.Agency_lat))
            .ForMember(dest => dest.BidNotes, opt => opt.MapFrom(src => src.Bid_notes))

            .ForMember(dest => dest.AgencyProfile, opt => opt.MapFrom(src => src.Agency_profile))
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))


            .ForMember(dest => dest.AgencyLng, opt => opt.MapFrom(src => src.Agency_lng))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
            .ForMember(dest => dest.BidAmount, opt => opt.MapFrom(src => src.Bid_amount))
            .ForMember(dest => dest.BidDate, opt => opt.MapFrom(src => src.Bid_date))
            .ForMember(dest => dest.BidType, opt => opt.MapFrom(src => src.Bid_type))
            .ForMember(dest => dest.BidNotes, opt => opt.MapFrom(src => src.Bid_notes))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ReverseMap();

            cfg.CreateMap<Fe_job_bid, AgencyBidsResponseDTO>()
            .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bid_id))
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_id))
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Full_name))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job_id))
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.Photo_path))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job_title))
            .ForMember(dest => dest.JobLat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.JobLng, opt => opt.MapFrom(src => src.Lng))
            .ForMember(dest => dest.JobFromDate, opt => opt.MapFrom(src => src.From_date))
            .ForMember(dest => dest.JobToDate, opt => opt.MapFrom(src => src.To_date))
            .ForMember(dest => dest.JobAssignmentStatus, opt => opt.MapFrom(src => src.Job_assignment_status))
            .ForMember(dest => dest.AverageBidAmount, opt => opt.MapFrom(src => src.Average_bid_amount))
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency_id))
            .ForMember(dest => dest.BidAmount, opt => opt.MapFrom(src => src.Bid_amount))
            .ForMember(dest => dest.BidDate, opt => opt.MapFrom(src => src.Bid_date))
            .ForMember(dest => dest.BidType, opt => opt.MapFrom(src => src.Bid_type))
            .ForMember(dest => dest.BidNotes, opt => opt.MapFrom(src => src.Bid_notes))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Is_active))
            .ReverseMap();
        }
    }
}
