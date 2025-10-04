using Services.IServices;
using Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using FH.Services.IServices.User;
using FH.Services.Services.User;
using Services.Services.Setup;
using Services.IServices.Setup;
using Services.IServices.Email;
using FH.Services.Services.email;
using BU.Services.IServices.Agency;
using BU.Services.Services.Agency;
using BU.Services.IServices.Jobs;
using BU.Services.Services.Jobs;
using BU.Services.Services.Customer;
using BU.Services.IServices.Customer;
using BU.Services.IServices.Trainings;
using BU.Services.Services.Trainings;
using BU.Services.IServices.Chat;
using BU.Services.Services.Chat;
using BU.Services.IServices.Notification;
using BU.Services.IServices.AuthNetPaymentService;
using BU.Services.Services.AuthNetPaymentService;
using BU.Services.IServices.Services;
using BU.Services.Services.Services;
//using Services.IServices;

namespace DependencyResolution.Modules
{
    internal static class ServiceModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDDLService, DDLService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeCustomerService, FeCustomerService>();
            services.AddScoped<IFeCustomerCardsService, FeCustomerCardsService>();
            services.AddScoped<IAgencyService, AgencyService>();
            services.AddScoped<IFeJobsService, FeJobsService>();
            services.AddScoped<IFeJobContractService, FeJobContractService>();
            services.AddScoped<IFeJobContractProgressService, FeJobContractProgressService>();
            services.AddScoped<ITrainingsService, TrainingsService>();
            services.AddScoped<ITrainingEnrollmentService, TrainingEnrollmentService>();
            services.AddScoped<ITrainingFeedBackService, TrainingFeedBackService>();
            services.AddScoped<ITrainersService, TrainersService>();
            services.AddScoped<IFeChatService, FeChatService>();
            services.AddScoped<INotificationService, FeNotificationServices>();
            services.AddScoped<IItemListService, FeItemListService>();
            services.AddScoped<IAuthNetPaymentService, AuthNetPaymentService>();
            services.AddScoped<IFeAgentsServices, FeAgentServices>();
            services.AddScoped<IFeServices, FeServices>();


        }
    }
}
