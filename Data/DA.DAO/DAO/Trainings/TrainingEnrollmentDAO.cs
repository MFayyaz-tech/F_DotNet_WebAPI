using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Trainings
{
    public class TrainingEnrollmentDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => @"update fe_trainings set is_deleted = 1, update_date = getdate(),updated_by = @UpdatedBy where enrollment_id = @EnrollmentId";

        public static string AlreadyEnrolledQuery => @"SELECT TOP 1 * FROM fe_training_enrollment 
                                                       WHERE ISNULL(is_deleted,0) = 0 AND training_id = @TrainingId AND customer_id = @CustomerId AND isnull(Enrollment_status,'Rejected') <> 'Rejected'";
        public static string GetEnrollmentByIdQuery => @"SELECT TOP 1 * FROM fe_training_enrollment 
                                                       WHERE ISNULL(is_deleted,0) = 0 AND enrollment_id = @EnrollmentId";

        public static string AgencyTrainingEnrollmentRequestsQuery => @"SELECT 
    t.training_id,
    t.training_title,
    t.agency_id,
	c.customer_id,
	c.first_name +' '+c.last_name as customer_name,
	c.address1,
	c.city,
	c.[state],
	c.zip_code,
	c.country,
	c.lat,
	c.lng,
	c.photo_path,
    te.enrollment_id,
    te.enrollment_status,
    te.enrollment_date,
	te.created_by,
	te.create_date
FROM fe_trainings t
JOIN fe_training_enrollment te ON t.training_id = te.training_id AND ISNULL(te.is_deleted,0) = 0
JOIN fe_customer c ON te.customer_id = c.customer_id AND ISNULL(c.is_deleted ,0 ) = 0
WHERE 
    t.agency_id = @AgencyId AND t.training_id = @TrainingId";


        public static string GetTrainingEnrollmentMedia => @"select * from fe_training_enrollment_media where enrollment_id = @EnrollmentId";

        public static string CustomerTrainingEnrollmentRequestsQuery => @"SELECT 
    t.training_id,
    t.training_title,
    t.agency_id,
    a.company_name,
    a.photo_path as company_profile_photo,
	c.customer_id,
	c.first_name +' '+c.last_name as customer_name,
	c.address1,
	c.city,
	c.[state],
	c.zip_code,
	c.country,
	c.lat,
	c.lng,
	c.photo_path,
    te.enrollment_id,
    te.enrollment_status,
    te.enrollment_date,
	te.created_by,
	te.create_date
FROM fe_trainings t
JOIN fe_training_enrollment te ON t.training_id = te.training_id AND ISNULL(te.is_deleted,0) = 0
JOIN fe_customer c ON te.customer_id = c.customer_id AND ISNULL(c.is_deleted ,0 ) = 0
JOIN fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted ,0 ) = 0
WHERE 
    te.customer_id = @CustomerId
    AND ISNULL(te.is_deleted, 0) = 0
    AND ISNULL(t.is_deleted, 0) = 0";

        public static string CustomerCompletedTrainingQuery => @"SELECT 
    e.customer_id, 
    e.enrollment_status, 
    e.update_date, 
    t.training_id, 
    t.training_title,
    COALESCE(f.feedback_count, 0) AS feedback_count
FROM 
    fe_training_enrollment e
LEFT JOIN 
    fe_trainings t ON e.training_id = t.training_id
LEFT JOIN (
    SELECT 
        training_id, 
        COUNT(*) AS feedback_count
    FROM 
        fe_training_feedback
    GROUP BY 
        training_id
) f ON e.training_id = f.training_id
WHERE 
    e.customer_id = @CustomerId
    AND e.enrollment_status = 'Completed'";

    }


}
