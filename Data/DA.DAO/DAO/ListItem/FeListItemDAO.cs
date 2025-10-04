using System;
using DAO;

namespace DA.DAO.DAO.ListItems
{
    public class FeListItemDAO : IDAO
    {
        public static string GetAddJobTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'job_types'";

        public static string GetLicenceTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'licence_type'";

        public static string GetAgencyBidTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'agency_bid_type'";

        public static string GetAddTrainingTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'add_training_type'";

        public static string GetAddTrainerExperienceListTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'add_trainer_experince_type'";

        public static string GetAddTrainerSkillsTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'add_trainer_skills_type'";

        public static string GetCancelJobTypeQuery => @"
            SELECT *
            FROM fe_list_item
            WHERE list_type = 'cancel_job_reason_type'";

        public static string GetServiceCategroy => @"
    SELECT *
            FROM fe_list_item
            WHERE list_type = 'service_type'";

        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();
    }
}
