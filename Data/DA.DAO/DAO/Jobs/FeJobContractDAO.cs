using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Jobs
{
    public class FeJobContractDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();
        public static string GetJobContractByContractId => @"select * From fe_job_contract where contract_id = @ContractId AND ISNULL(is_deleted,0) = 0";
        public static string GetAgencyRewaredJobsQuery => @"select jc.contract_id,jc.contract_status,jc.contract_progress,jc.agency_id,j.job_id,j.job_title,j.duration_type,j.from_date,j.to_date,j.lat,j.lng,
                                                            j.price_type,
                                                            j.bidder_type
                                                            from fe_job_contract jc
                                                            LEFT JOIN fe_jobs j ON j.job_id = jc.job_id AND ISNULL(j.is_deleted,0) = 0
                                                            where jc.agency_id = @AgencyId and jc.contract_status = 'Rewarded' and ISNULL(jc.is_deleted,0) = 0 order by jc.create_date desc";

        public static string GetAgencyJobContracts => @"
WITH ContractData AS (
    SELECT 
        jc.contract_id,
        jc.contract_status,
        jc.contract_progress,
        fu.photo_path,
        jc.agency_id,
        CASE 
        WHEN jc.contract_status = 'Rewarded' THEN '' 
        ELSE a.first_name + ' ' + a.last_name 
    END AS agency_name,
        j.job_id,
        j.job_title,
        j.duration_type,
        j.from_date,
        j.to_date,
        j.lat,
        j.lng,
        j.price_type,
        j.customer_id,
        j.bidder_type,
        ROW_NUMBER() OVER (PARTITION BY jc.contract_id ORDER BY jc.create_date DESC) AS RowNum
    FROM 
        fe_job_contract jc
        LEFT JOIN fe_jobs j 
            ON j.job_id = jc.job_id  
            AND ISNULL(j.is_deleted, 0) = 0
            LEFT JOIN fe_customer fu on fu.customer_id = j.customer_id
        LEFT JOIN fe_agent a 
            ON a.agency_id = jc.agency_id 
            AND ISNULL(a.is_deleted, 0) = 0
    WHERE 
         jc.agency_id = @AgencyId 
        AND jc.contract_status = @ContractStatus 
)
SELECT * 
FROM ContractData
WHERE RowNum = 1";


     
    }
}
