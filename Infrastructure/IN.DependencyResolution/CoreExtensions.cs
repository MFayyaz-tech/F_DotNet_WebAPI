using ORM;
using Logging;
using ORM.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using ORM.MSSql;
using DependencyResolution.Modules;

namespace DependencyResolution
{
    public static class CoreExtensions
    {

        public static void AddCore(this IServiceCollection services, IConfiguration configuration)
        {
           // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Use logging on Errors
            services.AddLogging(Path.Combine(AppContext.BaseDirectory, "nlog.config"));
            services.AddSingleton(configuration);

            //var connections = configuration.GetSection("DbConnections");
            services.AddTransient<IDataAccess>(e =>
               new DataAccess(configuration.GetSection("DbConnections").Get<List<DBConnection>>()));

            ServiceModule.Configure(services);
            RepositoryModule.Configure(services);
            MapperModule.Configure(services);

        }

        public static void AddLogging(this IServiceCollection services, string pathToConfig)
        {
            services.AddSingleton(NLoggerUtil.GetLoggingService(pathToConfig));
        }


    }
}
