using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Jobs
{
    public class FeJobsDAO : IDAO
    {


        public string GetAllQyery => @"SELECT * FROM fe_jobs WHERE ISNULL(is_deleted,0) = 0";

        public string GetSingleQuery => @"SELECT * FROM fe_jobs WHERE ISNULL(is_deleted,0) = 0 AND job_id = @Id";

        public string GridDataQuery => @"SELECT * FROM fe_jobs WHERE ISNULL(is_deleted,0) = 0";

        public string DoArchiveQuery => throw new NotImplementedException();

        public static string GetJobsListQuery => @"SELECT * FROM fe_jobs WHERE ISNULL(is_deleted,0) = 0";
        public static string GetJobById => @"SELECT * FROM fe_jobs WHERE job_id = @JobId AND ISNULL(is_deleted,0) = 0";
        public static string GetCustomerJobs => @"SELECT j.*, c.contract_status,a.company_name as agency_name FROM fe_jobs j
JOIN fe_job_contract c ON j.job_id = c.job_id 
join fe_agency a on a.agency_id = c.agency_id
WHERE j.customer_id = @CustomerId";

        public static string GetCustomerJobsHistory => @"SELECT 
p.contract_progress_id,
p.contract_id,
p.contract_progress,
p.contract_status as job_status,
p.create_date,
p.contract_notes,
 j.job_title
FROM fe_job_contract_progress p
JOIN fe_job_contract c ON p.contract_id = c.contract_id
JOIN fe_jobs j ON c.job_id = j.job_id
WHERE j.job_id = @JobId;";




        public static string GetOpenJobsQuery => @"

SELECT 
fe.job_id, 
fe.customer_id,

fe.duration_type,
fe.from_date,
fe.to_date,
fe.price_type,
fe.price_min,
fe.bidder_type,
fe.price_max,
fe.job_status,
fe.create_date,
c.first_name + ' ' + c.last_name AS customer_name,
c.photo_path,
c.address1 + ', ' + c.city + ', ' + c.country AS address,
c.photo_path, '',

fe.job_title,fe.lat,fe.lng,fe.job_description,
COUNT(jb.job_id) AS job_bid_count,
isnull(AVG(jb.bid_amount),0) AS average_bid_amount
FROM fe_jobs AS fe
LEFT JOIN fe_job_bid AS jb ON fe.job_id = jb.job_id
LEFT JOIN fe_customer AS c ON c.customer_id = fe.customer_id
WHERE ISNULL(fe.is_deleted, 0) = 0
AND (fe.job_status = 'Pending' OR fe.job_status = 'Open')
GROUP BY 
fe.customer_id, 
c.first_name,
c.last_name,
c.address1,
c.photo_path,
c.city,
c.country,
fe.job_title,
fe.lat,
fe.lng,
fe.job_description,
fe.job_id,
fe.duration_type,
fe.from_date,
fe.to_date,
fe.price_type,
fe.price_min,
fe.bidder_type,
fe.price_max,
fe.job_status,
fe.create_date

";

        public static string GetCustomerOpenJobsQuery => @"
SELECT
    fe.job_id,
    fe.customer_id,
    c.first_name +' '+ c.last_name as customer_name,
    c.photo_path,
    fe.job_title, fe.lat, fe.lng, fe.job_description,
    COUNT(jb.job_id) AS job_bid_count,
    AVG(jb.bid_amount) AS average_bid_amount
FROM fe_jobs AS fe
    LEFT JOIN fe_job_bid AS jb ON fe.job_id = jb.job_id
    LEFT JOIN fe_customer AS c ON c.customer_id = fe.customer_id
WHERE ISNULL(fe.is_deleted, 0) = 0
    AND fe.customer_id = @CustomerId
    AND (fe.job_status = 'Pending' OR fe.job_status = 'Open')
GROUP BY 
fe.customer_id, 
c.photo_path,
c.first_name,
c.last_name,
fe.job_title,
fe.lat,
fe.lng,
fe.job_description,
fe.job_id";
        public static string GetCustomerAllJobsQuery => @"SELECT 
                                                            fe.job_id, 
                                                            fe.customer_id,
                                                            c.first_name +' '+ c.last_name as customer_name,
                                                            fe.job_title,fe.lat,fe.lng,fe.job_description,
                                                            COUNT(jb.job_id) AS job_bid_count,
                                                            AVG(jb.bid_amount) AS average_bid_amount
                                                            FROM fe_jobs AS fe
                                                            LEFT JOIN fe_job_bid AS jb ON fe.job_id = jb.job_id
                                                            LEFT JOIN fe_customer AS c ON c.customer_id = fe.customer_id
                                                            WHERE ISNULL(fe.is_deleted, 0) = 0
                                                              AND fe.customer_id = @CustomerId
                                                            GROUP BY 
                                                            fe.customer_id, 
                                                            c.first_name,
                                                            c.last_name,
                                                            fe.job_title,
                                                            fe.lat,
                                                            fe.lng,
                                                            fe.job_description,
                                                            fe.job_id";


        public static string GetCustomerActiveJobs => @"
select j.job_id,
j.job_title,
j.job_description,
c.photo_path,
j.duration_type,
j.from_date,
j.to_date,
j.job_status,
a.agency_id,
a.company_name as agency_name,
a.phone as agency_phone,
a.photo_path as agency_profile_image,
jc.contract_id,
jc.contract_progress,
jb.bid_amount as contract_price
from fe_jobs j
INNER JOIN fe_job_contract jc ON jc.job_id = j.job_id  AND (jc.contract_status = 'InProgress' OR jc.contract_status = 'Delivered' OR jc.contract_status = 'Rewarded')
LEFT JOIN fe_agency a ON a.agency_id = jc.agency_id AND ISNULL(a.is_deleted,0) = 0
LEFT JOIN fe_customer c on c.customer_id = j.customer_id
LEFT JOIN fe_job_bid jb ON jc.bid_id = jb.bid_id
where ISNULL(j.is_deleted,0) = 0 AND j.customer_id = @CustomerId
AND (j.job_status = 'InProgress' OR j.job_status = 'Delivered'  OR j.job_status = 'Rewarded')";


        public static string GetCustomerActiveJobDetail => @"

select j.job_id,
j.job_title,
COALESCE(AVG(CAST(ra.customer_rating AS DECIMAL(3, 2))), 0) AS agency_rating,  -- Cast to decimal for average
j.job_description,
j.duration_type,
j.from_date,
j.to_date,

j.job_status,
a.agency_id,
a.company_name as agency_name,
a.phone as agency_phone,
a.photo_path as agency_profile_image,
jc.contract_id,
jc.contract_progress,
jb.bid_amount as contract_price
from fe_jobs j
INNER JOIN fe_job_contract jc ON jc.job_id = j.job_id  AND (jc.contract_status = 'InProgress' OR jc.contract_status = 'Delivered' OR jc.contract_status = 'Rewarded')
LEFT JOIN fe_agency a ON a.agency_id = jc.agency_id AND ISNULL(a.is_deleted,0) = 0
LEFT JOIN 
    fe_job_contract ra ON ra.agency_id = a.agency_id
LEFT JOIN fe_job_bid jb ON jc.bid_id = jb.bid_id
where ISNULL(j.is_deleted,0) = 0 AND j.job_id = @JobId
AND (j.job_status = 'InProgress' OR j.job_status = 'Delivered'  OR j.job_status = 'Rewarded')
GROUP BY 
    j.job_id,
    j.job_title,
    j.job_description,
    j.duration_type,
    j.from_date,
    j.to_date,
    j.job_status,
    a.agency_id,
    a.company_name,
    a.phone,
    a.photo_path,
    jc.contract_id,
    jc.contract_progress,
    jb.bid_amount";

        public static string GetAgencyAssignJobs => @"SELECT 
        CASE 
        WHEN u.agent_id = 0 THEN 'Not Assigned'
        ELSE u.first_name + ' ' + u.last_name 
        END AS agent_name,
        jc.*, 
        j.*
        FROM 
        fe_job_contract jc
        JOIN 
        fe_jobs j ON j.job_id = jc.job_id
        LEFT JOIN 
        fe_agent u ON u.agent_id = jc.agent_id
        WHERE 
        jc.agency_id = @AgencyId AND jc.contract_progress != 100
        AND jc.contract_status = 'InProgress'";
                                                    
    }
}
