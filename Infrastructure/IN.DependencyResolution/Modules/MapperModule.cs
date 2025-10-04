using AutoMapper;
using DTO.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyResolution.Modules
{
    public class MapperModule
    {
        public static void Configure(IServiceCollection services)
        {
            var config = DTOMapper.Configure();
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
