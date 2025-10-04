using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Agency
{
	public class AgencyDAO : IDAO
	{
		public string GetAllQyery => @"select * from ";

		public string GetSingleQuery => throw new NotImplementedException();

		public string GridDataQuery => throw new NotImplementedException();

        public static string GetAgencyBankDetail => @"select * from fe_agency_bank_details where agency_id = @AgencyId";

		public string DoArchiveQuery => throw new NotImplementedException();
		public static string GetAgencyListQuery => @"select u.email_address,a.* From fe_agency a
													JOIN fe_users u ON a.user_id = u.user_id
													WHERE ISNULL(a.is_deleted,0) = 0";

        public static string GetAgencyJobFeedBack => @"
SELECT 
fc.job_title,
jc.customer_rating,
ISNULL(jc.customer_feedback,'') as customer_feedback,
jc.contract_status,
jc.contract_id,

jc.update_date

FROM fe_job_contract  jc

LEFT JOIN fe_jobs fc on fc.job_id = jc.job_id
  AND (contract_status = 'Cancelled' OR contract_status = 'Completed')
  WHERE agency_id = @AgencyId ";

		public static string GetAgencyJobDetail => @"
SELECT 
    a.company_name as agency_name,
    ISNULL(a.photo_path,'')  as agency_photo,
    a.agency_id,
    a.agency_contact_person,
    a.lat,
    a.lng,
    a.address1 + a.city + a.country + a.zip_code  as address1,
    a.agency_profile,
    AVG(fc.customer_rating) AS average_rating  -- Calculate overall average rating
    
FROM 
    fe_job_bid jb
LEFT JOIN 
    fe_agency a ON a.agency_id = jb.agency_id
LEFT JOIN 
    fe_jobs fj ON fj.job_id = jb.job_id
LEFT JOIN 
    fe_job_contract fc ON fc.job_id = jb.job_id
WHERE 
    jb.agency_id = @AgencyId 
  
GROUP BY 
    a.company_name,
    a.photo_path,
    a.agency_profile,
    a.agency_contact_person,
    a.lat,
    a.lng,
    a.address1 + a.city + a.country + a.zip_code ,
    a.agency_id;";
       


        public static string GetAgencyByUserId
        {
            get
            {
                return "select top(1) * from [fe_agency] a where a.user_id = @UserId and isnull( a.is_deleted, 0 ) = 0";
            }
        }

        public static string GetAgencyById
		{
			get
			{
				return "select top(1) * from [fe_agency] a where a.agency_id = @AgencyId and isnull( a.is_deleted, 0 ) = 0";
			}


		}
        public static string GetAgencyEarning => @"
SELECT 
    case when p.payment_type = 'job' then jc.agency_id else ft.agency_id end agency_id,
  
    SUM(CASE WHEN p.payment_type = 'job' THEN p.amount ELSE 0 END) AS total_job_payment,
    SUM(CASE WHEN p.payment_type = 'training' THEN p.amount ELSE 0 END) AS total_training_payment,
    SUM(p.amount) AS total_payment_earned,

    COUNT(DISTINCT CASE WHEN p.payment_type = 'job' THEN jc.job_id END) AS total_jobs_done,
    
    COUNT(DISTINCT CASE WHEN p.payment_type = 'training' THEN p.training_id END) AS total_training_done

FROM 
    fe_payment p
    LEFT JOIN fe_job_contract jc ON jc.job_id = p.job_id and p.payment_type ='job' and 
    jc.agency_id = @AgencyId 
    LEFT JOIN fe_trainings ft ON ft.training_id = p.training_id and p.payment_type ='training'  and 
    ft.agency_id = @AgencyId  
    
GROUP BY 
    case when p.payment_type = 'job' then jc.agency_id else ft.agency_id end";
	}
}
