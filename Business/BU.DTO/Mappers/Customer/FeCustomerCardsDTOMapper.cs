using AutoMapper;
using BU.DTO.DTOs.Customer;
using DA.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.DTO.Mappers.Customer
{
    public class FeCustomerCardsDTOMapper
    {
        public static void Mapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fe_customer_cards, FeCustomerCardsDTO>()
            .ForMember(dest => dest.CustomerCardId, opt => opt.MapFrom(src => src.Customer_card_id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer_id))
            .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.Card_id))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.Expire_date))
            .ForMember(dest => dest.CvvNumber, opt => opt.MapFrom(src => src.Cvv_number))

            .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.Credit_card_number))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.Is_default))
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
