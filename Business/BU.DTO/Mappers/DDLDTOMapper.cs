using AutoMapper;
using DTO.DTOs;
using Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Mappers
{
    public class DDLDTOMapper
    {
          public static void Mapping(IMapperConfigurationExpression cfg)
        {

            cfg.CreateMap<list_item, DDLDTO>()
            .ForMember(dest => dest.T1, opt => opt.MapFrom(src => src.t1))
            .ForMember(dest => dest.T2, opt => opt.MapFrom(src => src.t2))
            .ForMember(dest => dest.T3, opt => opt.MapFrom(src => src.t3))
            .ForMember(dest => dest.T4, opt => opt.MapFrom(src => src.t4))
            .ForMember(dest => dest.T5, opt => opt.MapFrom(src => src.t5))
            .ForMember(dest => dest.T6, opt => opt.MapFrom(src => src.t6));


        }

    }
}
