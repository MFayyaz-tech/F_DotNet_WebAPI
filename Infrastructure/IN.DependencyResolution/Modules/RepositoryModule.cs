using DAO;
using Entities;
using Entities.Users;
using Entities.Core;
using ORM;
using ORM.MSSQL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Setup;
using DA.Entities.Agency;
using DA.Entities.Jobs;
using DA.Entities.Customer;
using DA.Entities.Trainings;
using DA.Entities.Chat;
using DA.Entities.Notifications;
using DA.Entities.ItemList;
using DA.Entities.Billing;
using DA.Entities.Services;

namespace DependencyResolution.Modules
{
    internal static class RepositoryModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IRepository<list_item>, Repository<list_item>>();
            services.AddScoped<IDataContext<list_item>, DataContext<list_item>>();

            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IDataContext<Role>, DataContext<Role>>();

            services.AddScoped<IRepository<Role_Permission>, Repository<Role_Permission>>();
            services.AddScoped<IDataContext<Role_Permission>, DataContext<Role_Permission>>();

            services.AddScoped<IRepository<Fe_users>, Repository<Fe_users>>();
            services.AddScoped<IDataContext<Fe_users>, DataContext<Fe_users>>();

            services.AddScoped<IRepository<Fe_customers>, Repository<Fe_customers>>();
            services.AddScoped<IDataContext<Fe_customers>, DataContext<Fe_customers>>();
            services.AddScoped<IRepository<Fe_customer_cards>, Repository<Fe_customer_cards>>();
            services.AddScoped<IDataContext<Fe_customer_cards>, DataContext<Fe_customer_cards>>();


            services.AddScoped<IRepository<Fe_agency>, Repository<Fe_agency>>();
			services.AddScoped<IDataContext<Fe_agency>, DataContext<Fe_agency>>();
            services.AddScoped<IRepository<Fe_agency_bank_details>, Repository<Fe_agency_bank_details>>();
            services.AddScoped<IDataContext<Fe_agency_bank_details>, DataContext<Fe_agency_bank_details>>();
            services.AddScoped<IRepository<Fe_agency_license>, Repository<Fe_agency_license>>();
            services.AddScoped<IDataContext<Fe_agency_license>, DataContext<Fe_agency_license>>();

            services.AddScoped<IRepository<Fe_jobs>, Repository<Fe_jobs>>();
            services.AddScoped<IDataContext<Fe_jobs>, DataContext<Fe_jobs>>();

            services.AddScoped<IRepository<Fe_job_bid>, Repository<Fe_job_bid>>();
            services.AddScoped<IDataContext<Fe_job_bid>, DataContext<Fe_job_bid>>();

            services.AddScoped<IRepository<Fe_job_contract>, Repository<Fe_job_contract>>();
            services.AddScoped<IDataContext<Fe_job_contract>, DataContext<Fe_job_contract>>();

            services.AddScoped<IRepository<Fe_job_contract_progress>, Repository<Fe_job_contract_progress>>();
            services.AddScoped<IDataContext<Fe_job_contract_progress>, DataContext<Fe_job_contract_progress>>();

            services.AddScoped<IRepository<Fe_trainings>, Repository<Fe_trainings>>();
            services.AddScoped<IDataContext<Fe_trainings>, DataContext<Fe_trainings>>();

            services.AddScoped<IRepository<Fe_training_media>, Repository<Fe_training_media>>();
            services.AddScoped<IDataContext<Fe_training_media>, DataContext<Fe_training_media>>();

            services.AddScoped<IRepository<Fe_training_enrollment>, Repository<Fe_training_enrollment>>();
            services.AddScoped<IDataContext<Fe_training_enrollment>, DataContext<Fe_training_enrollment>>();
            services.AddScoped<IRepository<Fe_training_enrollment_media>, Repository<Fe_training_enrollment_media>>();
            services.AddScoped<IDataContext<Fe_training_enrollment_media>, DataContext<Fe_training_enrollment_media>>();

            services.AddScoped<IRepository<Fe_training_feedback>, Repository<Fe_training_feedback>>();
            services.AddScoped<IDataContext<Fe_training_feedback>, DataContext<Fe_training_feedback>>();

            services.AddScoped<IRepository<Fe_trainers>, Repository<Fe_trainers>>();
            services.AddScoped<IDataContext<Fe_trainers>, DataContext<Fe_trainers>>();

            services.AddScoped<IRepository<Fe_chat>, Repository<Fe_chat>>();
            services.AddScoped<IDataContext<Fe_chat>, DataContext<Fe_chat>>();

            services.AddScoped<IRepository<Fe_feedback_reply>, Repository<Fe_feedback_reply>>();
            services.AddScoped<IDataContext<Fe_feedback_reply>, DataContext<Fe_feedback_reply>>();

            services.AddScoped<IRepository<Fe_notifications_tokens>, Repository<Fe_notifications_tokens>>();
            services.AddScoped<IDataContext<Fe_notifications_tokens>, DataContext<Fe_notifications_tokens>>();

            services.AddScoped<IRepository<Fe_item_list>, Repository<Fe_item_list>>();
            services.AddScoped<IDataContext<Fe_item_list>, DataContext<Fe_item_list>>();

            services.AddScoped<IRepository<Fe_payment>, Repository<Fe_payment>>();
            services.AddScoped<IDataContext<Fe_payment>, DataContext<Fe_payment>>();

            services.AddScoped<IRepository<Fe_agent>, Repository<Fe_agent>>();
            services.AddScoped<IDataContext<Fe_agent>, DataContext<Fe_agent>>();

            services.AddScoped<IRepository<Fe_services>, Repository<Fe_services>>();
            services.AddScoped<IDataContext<Fe_services>, DataContext<Fe_services>>();

        }
    }
}
