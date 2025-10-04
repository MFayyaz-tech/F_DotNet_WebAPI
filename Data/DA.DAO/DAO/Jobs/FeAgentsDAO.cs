using System;
using DAO;

namespace DA.DAO.DAO.Jobs
{
    public class FeAgentsDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();

        public static string GetAllAgents => @"
                                              SELECT a.*, 
                                              CASE 
                                              WHEN EXISTS (
                                              SELECT 1 
                                              FROM fe_job_contract jc 
                                              WHERE jc.agent_id = a.agent_id
                                              AND jc.Contract_status = 'InProgress'
                                              ) THEN 'YES'
                                              ELSE 'NO'
                                              END AS job_assign,
                                              (SELECT COUNT(*)
                                              FROM fe_job_contract jc2
                                              WHERE jc2.agent_id = a.agent_id
                                              AND jc2.Contract_status = 'Completed') AS total_completed_jobs
                                              FROM fe_agent a
                                              WHERE a.agency_id = @AgencyId
                                              AND ISNULL(a.is_deleted, 0) = 0";

        public static string getAgentDetail => @"SELECT 
                                                a.*,
                                                u.user_name,
                                                (SELECT COUNT(*) 
                                                 FROM fe_job_contract jc 
                                                 WHERE jc.agent_id = a.agent_id AND jc.contract_status = 'Completed') AS total_completed_jobs,
                                                (SELECT COUNT(*) 
                                                 FROM fe_job_contract jc 
                                                 WHERE jc.agent_id = a.agent_id AND jc.contract_status = 'InProgress') AS in_progress_jobs,
                                                  (SELECT COUNT(*) 
                                                 FROM fe_job_contract jc 
                                                 WHERE jc.agent_id = a.agent_id AND jc.contract_status = 'Cancelled') AS cancelled_jobs
                                                FROM 
                                                fe_agent a
                                                JOIN 
                                                fe_users u ON a.user_id = u.user_id
                                                WHERE 
                                                a.agent_id = @AgentId";


        public static string GetAgentReviews => @"
                                                 
SELECT 
    fc.contract_id,
    fc.job_id,
    fc.agency_id,
    fc.contract_status,
    fc.customer_feedback,
    fu.first_name + fu.last_name as customer_name,
    fu.photo_path as customer_photo,
    fc.customer_rating,
    fc.create_date,
    fc.update_date,
    fc.bid_id,
    fc.agent_id,
    fj.job_title,
    fj.job_description,
    fj.job_status,
    fj.duration_type,
    fj.to_date,
    fj.from_date,
    fj.job_category,
    fj.price_type,
    fj.price_min,
    fj.price_max
FROM 
    fe_job_contract fc
JOIN 
    fe_jobs fj ON fc.job_id = fj.job_id
JOIN fe_customer fu ON fj.customer_id = fu.customer_id    
WHERE 
    fc.agent_id = @AgentId";


        public static string GetAgentJobs => @"select 

fc.*


 from fe_job_contract jc
 JOIN fe_jobs fc ON jc.job_id = fc.job_id
 
 where agent_id = @AgentId";


    }
}

