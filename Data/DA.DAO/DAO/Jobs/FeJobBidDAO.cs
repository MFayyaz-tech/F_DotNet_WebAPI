using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Jobs
{
    public class FeJobBidDAO : IDAO
    {
        public string GetAllQyery => @"SELECT * FROM fe_job_bid WHERE ISNULL(is_deleted,0) = 0";

        public string GetSingleQuery => @"SELECT * FROM fe_job_bid WHERE ISNULL(is_deleted,0) = 0 AND bid_id = @Id";

        public string GridDataQuery => @"SELECT * FROM fe_job_bid WHERE ISNULL(is_deleted,0) = 0";

        public string DoArchiveQuery => throw new NotImplementedException();
        public static string GetBidByIdQuery => @"SELECT * FROM fe_job_bid WHERE ISNULL(is_deleted,0) = 0 AND bid_id = @BidId";

        public static string GetJobBidsListQuery => @"SELECT a.company_name as agency_name,
a.agency_profile,
jb.bid_notes,
a.photo_path,
 a.lat as agency_lat, a.lng as agency_lng, jb.*
FROM fe_job_bid jb
    LEFT JOIN fe_agency a ON a.agency_id = jb.agency_id AND ISNULL(a.is_deleted,0) = 0
WHERE ISNULL(jb.is_deleted,0) = 0 AND jb.job_id = @JobId";
        public static string GetIfAgencyAlreadyBidOnJobQuery => @"SELECT * FROM fe_job_bid WHERE agency_id = @AgencyId AND job_id = @JobId AND ISNULL(is_deleted,0) = 0";

        public static string GetAgencyBidsQuery => @"
DECLARE @job_id BIGINT = 0,
        @avg_bid_amount DECIMAL(10,2) = 0.00;

-- Get the job ID for the agency
SELECT @job_id = job_id 
FROM fe_job_bid 
WHERE agency_id = @AgencyId;

-- Calculate the average bid amount for the job
SELECT @avg_bid_amount = AVG(bid_amount) 
FROM fe_job_bid 
WHERE job_id = @job_id 
GROUP BY job_id;

-- Get the user profile image





-- Main query to fetch job bids for the agency with ""Open"" job status
SELECT
    jb.bid_id,
    fu.photo_path,
    fu.first_name + ' ' + fu.last_name as full_name,
    fu.user_id,
    jb.job_id,
    jb.agency_id,
    jb.bid_amount,
    jb.bid_date,
    jb.bid_type,
    j.job_title,
    j.job_status,
    j.lat,
    j.lng,
    j.from_date,
    j.to_date,
    COALESCE(c.contract_status, 'Not Assigned') AS contract_status,
    CASE 
        WHEN c.agency_id IS NOT NULL AND c.agency_id <> jb.agency_id AND c.contract_status <> 'Cancelled' 
             THEN 'Rewarded'
        ELSE 'Pending'
    END AS job_assignment_status,
    @avg_bid_amount AS average_bid_amount
FROM
    fe_job_bid jb
JOIN
    fe_jobs j ON jb.job_id = j.job_id
LEFT JOIN
    fe_job_contract c ON jb.job_id = c.job_id

LEFT JOIN
    fe_customer fu ON fu.customer_id = j.customer_id    
WHERE
    jb.agency_id = @AgencyId
    AND jb.is_deleted = 0
    AND j.is_deleted = 0
    AND j.job_status = 'Open' -- Filter for only ""Open"" jobs
ORDER BY
    jb.bid_date DESC;
";
    }
}
