using DA.DAO.DAO.Agency;
using DA.DAO.DAO.Chat;
using DA.DAO.DAO.Customer;
using DA.DAO.DAO.Jobs;
using DA.DAO.DAO.ListItems;
using DA.DAO.DAO.Notifications;
using DA.DAO.DAO.Payment;
using DA.DAO.DAO.Services;
using DA.DAO.DAO.Trainings;
using DA.Entities.Agency;
using DA.Entities.Billing;
using DA.Entities.Chat;
using DA.Entities.Customer;
using DA.Entities.ItemList;
using DA.Entities.Jobs;
using DA.Entities.Notifications;
using DA.Entities.Services;
using DA.Entities.Trainings;
using DAO.DAO.Core;
using DAO.DAO.Setup;
using DAO.DAO.User;
using Entities.Base;
using Entities.Core;
using Entities.Setup;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO
{
    public class DAOFactory<T> where T : BaseEntity, new()
    {
        public static readonly Dictionary<Type, Type> ModelMap = new Dictionary<Type, Type>();

        static DAOFactory()
        {
            ModelMap.Add(typeof(list_item), typeof(DDLlistDAO));
            ModelMap.Add(typeof(Role), typeof(RoleDAO));
            ModelMap.Add(typeof(Role_Permission), typeof(RolePermission));
            ModelMap.Add(typeof(Fe_users), typeof(UserDAO));
            ModelMap.Add(typeof(Fe_customers), typeof(FeCustomerDAO));
            ModelMap.Add(typeof(Fe_customer_cards), typeof(FeCustomerCardsDAO));
            ModelMap.Add(typeof(Fe_agency), typeof(AgencyDAO));
            ModelMap.Add(typeof(Fe_agency_bank_details), typeof(AgencyBankDetailsDAO));
            ModelMap.Add(typeof(Fe_agency_license), typeof(AgencyLicenseDAO));
            ModelMap.Add(typeof(Fe_jobs), typeof(FeJobsDAO));
            ModelMap.Add(typeof(Fe_job_bid), typeof(FeJobBidDAO));
            ModelMap.Add(typeof(Fe_job_contract), typeof(FeJobContractDAO));
            ModelMap.Add(typeof(Fe_job_contract_progress), typeof(FeJobContractProgressDAO));
            ModelMap.Add(typeof(Fe_trainings), typeof(TrainingsDAO));
            ModelMap.Add(typeof(Fe_training_media), typeof(TrainingMediaDAO));
            ModelMap.Add(typeof(Fe_training_enrollment), typeof(TrainingEnrollmentDAO));
            ModelMap.Add(typeof(Fe_training_enrollment_media), typeof(TrainingEnrollmentMediaDAO));
            ModelMap.Add(typeof(Fe_training_feedback), typeof(TrainingFeedBackDAO));
            ModelMap.Add(typeof(Fe_trainers), typeof(TrainersDAO));
            ModelMap.Add(typeof(Fe_chat), typeof(FeChatDAO));
            ModelMap.Add(typeof(Fe_feedback_reply), typeof(TrainingFeedBackDAO));
            ModelMap.Add(typeof(Fe_notifications_tokens), typeof(FeNotificationsDAO));
            ModelMap.Add(typeof(Fe_item_list), typeof(FeListItemDAO));
            ModelMap.Add(typeof(Fe_payment), typeof(FePaymentDAO));
            ModelMap.Add(typeof(Fe_agent), typeof(FeAgentsDAO));
            ModelMap.Add(typeof(Fe_services), typeof(FeServicesDAO));

        }


        internal static IDAO GetDAO()
        {
            IDAO obj;
            if (ModelMap.ContainsKey(typeof(T)))
            {
                obj = (IDAO)Activator.CreateInstance(ModelMap[typeof(T)]);
            }
            else
            {
                throw new NotImplementedException(string.Format("DAO Object is not implemented for model type '{0}' ", typeof(T).Name));
            }

            return obj;
        }
    }
}
