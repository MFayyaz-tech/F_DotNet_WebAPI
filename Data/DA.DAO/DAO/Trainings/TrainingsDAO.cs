using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace DA.DAO.DAO.Trainings
{
    public class TrainingsDAO : IDAO
    {
        public string GetAllQyery => @"Select * from fe_trainings where ISNULL(is_deleted,0) = 0";

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => @"Select * from fe_trainings where ISNULL(is_deleted,0) = 0";

        public string DoArchiveQuery => "update fe_trainings set is_deleted = 1, update_date = getdate(),updated_by = @UpdatedBy where trainer_id = @TrainingId";
        public static string GetAllTrainingsQuery => "Select * from fe_trainings where ISNULL(is_deleted,0) = 0";
        public static string GetOnlyTrainingByIdQuery => "Select * from fe_trainings where ISNULL(is_deleted,0) = 0 and training_id = @TrainingId";

        public static string GetTestimonials => @"
                          SELECT 
                          t.training_id,
                          t.training_title,
                          t.training_status, 
                          ft.first_name + ' ' + ft.last_name AS trainer_name,
                          COUNT(DISTINCT ff.rating) AS rating_count,
                          AVG(ISNULL(ff.rating, 0)) AS average_rating
                      FROM 
                          fe_trainings t
                      LEFT JOIN 
                          fe_training_enrollment te 
                      ON 
                          te.training_id = t.training_id
                      LEFT JOIN 
                          fe_trainers ft 
                      ON 
                          ft.trainer_id = t.trainer_id
                      LEFT JOIN 
                          fe_training_feedback ff 
                      ON 
                          ff.training_id = t.training_id 
                      WHERE
                          ISNULL(t.is_deleted, 0) = 0 
                          AND t.agency_id = @AgencyId
                          AND t.training_status != 'UnPublished'
                      GROUP BY 
                          t.training_id,
                          t.training_title,
                          t.training_status,
                          ft.first_name + ' ' + ft.last_name;";
                      

        public static string GetTestimonialsById => @"
SELECT 
    t.training_id,
    t.training_title,
    t.training_status, 
    ft.first_name + ' ' + ft.last_name AS trainer_name,
    AVG(ISNULL(ff.rating, 0)) AS average_rating
FROM 
    fe_trainings t
LEFT JOIN 
    fe_training_enrollment te 
ON 
    te.training_id = t.training_id
LEFT JOIN 
    fe_trainers ft 
ON 
    ft.trainer_id = t.trainer_id
LEFT JOIN 
    fe_training_feedback ff 
ON 
    ff.training_id = t.training_id 
WHERE
    ISNULL(t.is_deleted, 0) = 0 
    AND t.training_id = @TrainingId
  
GROUP BY 
    t.training_id,
    t.training_title,
    t.training_status,
    ft.first_name + ' ' + ft.last_name;";
        public static string GetFeedbackById => @"SELECT * from Fe_training_feedback where training_feedback_id = @feedbackId";


        public static string GetTrainingByIdQuery => @"SELECT 
    a.company_name AS Agency_name,
    a.photo_path AS agency_photo,
    a.phone AS agency_phone,

    tr.first_name + ' ' + tr.last_name AS Trainer_name,
    t.*,
    
    AVG(f.rating) AS average_rating,
    
    -- Use MAX or MIN to get a single enrolment_id value
    ISNULL(MAX(te.enrollment_id), 0) AS enrolment_id

FROM fe_trainings t
LEFT JOIN fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted, 0) = 0
LEFT JOIN fe_trainers tr ON tr.trainer_id = t.trainer_id AND ISNULL(tr.is_deleted, 0) = 0
LEFT JOIN fe_training_feedback f ON f.training_id = t.training_id AND ISNULL(f.is_deleted, 0) = 0
LEFT JOIN fe_training_enrollment te ON te.training_id = t.training_id AND ISNULL(te.is_deleted, 0) = 0

WHERE ISNULL(t.is_deleted, 0) = 0 
AND t.training_id = @TrainingId

GROUP BY 
    a.company_name,
    a.photo_path,
    a.phone,
    t.training_title,
    t.from_date,
    t.fee,
    t.to_date,
    t.duration,
    tr.first_name,
    tr.last_name,
    t.training_id,
    t.agency_id,
    t.trainer_id,
    t.training_status,
    t.is_deleted,
    t.location_lat,
    t.location_lng,
    t.details,
    t.is_active,
    t.create_date,
    t.update_date,
    t.photo_path,
    t.training_category,
    t.is_approval_required,
    t.training_progress,
    t.created_by,
    t.updated_by";




        public static string GetTrainingByStatusQuery => @"Select 
                            a.company_name as Agency_name,a.photo_path as agency_photo,a.phone as agency_phone,
                            tr.first_name+' '+tr.last_name as Trainer_name, 
                            t.* 
                            from fe_trainings t
                            LEFT JOIN fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted,0) = 0
                            LEFT JOIN fe_trainers tr ON tr.trainer_id = t.trainer_id AND ISNULL(tr.is_deleted,0) = 0
                            where ISNULL(t.is_deleted,0) = 0 AND ISNULL(t.training_status,'UnPublished') = @TrainingStatus";
        public static string GetTrainingByAgencyQuery => @"SELECT 
    a.company_name AS Agency_name,
    a.photo_path AS agency_photo,
    a.phone AS agency_phone,
    tr.first_name + ' ' + tr.last_name AS Trainer_name,
    t.*, 
    (SELECT TOP 1 tm.media_path 
     FROM fe_training_media tm 
     WHERE tm.training_id = t.training_id AND tm.category = 'Banner'
     ORDER BY tm.media_id) AS media_path
FROM 
    fe_trainings t
    LEFT JOIN fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted, 0) = 0
    LEFT JOIN fe_trainers tr ON tr.trainer_id = t.trainer_id AND ISNULL(tr.is_deleted, 0) = 0
WHERE 
    ISNULL(t.is_deleted, 0) = 0 
    AND ISNULL(t.training_status, 'UnPublished') = @TrainingStatus 
    AND a.agency_id = @AgencyId
";

        public static string GetCustomerEnrolledTraingingsQuery => @"
SELECT 
            a.company_name as agency_name,
            a.photo_path as agency_photo,
            a.phone as agency_phone,
            tr.trainer_id,
            tr.first_name+' '+tr.last_name as Trainer_name,
            te.enrollment_id,
            te.create_date as training_enroll_date,
            te.enrollment_status,
            (SELECT COUNT(*) FROM Fe_training_enrollment WHERE training_id = t.training_id AND ISNULL(is_deleted, 0) = 0) AS total_enrolled_customers,
            t.training_id,
            t.agency_id,
            t.training_title,
            t.training_progress,
            t.training_status,
 t.training_category,
            t.location_lat,
            t.location_lng,
              (SELECT TOP 1 tm.media_path 
            FROM fe_training_media tm 
            WHERE tm.training_id = t.training_id 
            AND ISNULL(tm.is_deleted, 0) = 0 
            AND tm.category = 'Banner'
            ORDER BY tm.create_date ASC) AS photo_path,
                t.fee,
                t.create_date,
            
            t.from_date,
            t.to_date,
            t.duration,
            t.fee
            FROM Fe_trainings t
            LEFT JOIN fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted,0) = 0
            JOIN Fe_training_enrollment te ON t.training_id = te.training_id
            LEFT JOIN fe_trainers tr ON tr.trainer_id = t.trainer_id AND ISNULL(tr.is_deleted,0) = 0
            WHERE 
            te.customer_id = @CustomerId 
            AND ISNULL(t.training_status,'UnPublished') <> 'UnPublished'
            AND ISNULL(te.enrollment_status,'Pending') <> 'Pending'
            AND ISNULL(t.is_deleted,0) = 0 
            AND ISNULL(te.is_deleted,0) = 0 
            GROUP BY 
            a.company_name,
            a.photo_path,
            a.phone,
            tr.trainer_id,
            tr.first_name,
            tr.last_name,
            te.enrollment_id,
            te.create_date,
            te.enrollment_status,
            t.training_id,
            t.agency_id,
            t.training_title,
            t.training_progress,
            t.training_status,
 t.training_category,
            t.location_lat,
            t.location_lng,
            t.from_date,
            t.to_date,
            t.duration,
            t.photo_path,
         t.create_date,
            t.fee,
            t.training_id";
        public static string GetTrainingFeedBack => @"SELECT 
    f.*, 
    c.first_name + c.last_name as customer_name, 
    t.training_title
FROM 
    fe_training_feedback f
JOIN 
    fe_customer c ON f.customer_id = c.customer_id
JOIN 
    fe_trainings t ON f.training_id = t.training_id
WHERE 
    ISNULL(f.is_deleted, 0) = 0 
    AND f.training_id = @TrainingId";


        public static string GetFeedbackListInTestimonial => @"
 SELECT 

    ISNULL(ff.attachment_media,'') as attachment_media,
    ff.training_feedback_id,
    ff.create_date ,

    ISNULL(ff.feedback,'') as feedback,
    ISNULL( ff.rating ,0)  AS feedback_rating,
    fc.photo_path,
    fc.first_name + ' ' + fc.last_name AS customer_name,
    AVG(ISNULL(ff.rating, 0)) AS average_rating
    FROM 
    fe_trainings t    
    LEFT JOIN 
    fe_training_enrollment te 
    ON 
    te.training_id = t.training_id
    LEFT JOIN 
    fe_trainers ft 
    ON 
    ft.trainer_id = t.trainer_id
    LEFT JOIN 
    fe_training_feedback ff 
    ON 
    ff.training_id = t.training_id 
    LEFT JOIN 
     fe_customer fc 
    ON
    fc.customer_id = ff.customer_id         
    WHERE
    ISNULL(t.is_deleted, 0) = 0 
	AND  ISNULL(ft.is_deleted, 0) = 0 
	AND  ISNULL(ff.is_deleted, 0) = 0 
    AND t.training_id =50056
    
    GROUP BY 
    ff.feedback,
    ff.rating,
    ff.training_feedback_id,
    ff.create_date,
    ff.attachment_media,
    fc.photo_path,
    fc.first_name + ' ' + fc.last_name,
    ft.first_name + ' ' + ft.last_name;";

        public static string GetFeedbackRepliesByFeedbackId => @"select * from fe_feedback_reply where training_feedback_id = @training_feedback_id";


        public static string GetFeaturedTraining => @"SELECT TOP 3 
    a.company_name AS agency_name,
    a.photo_path AS agency_photo,
    a.phone AS agency_phone,
    tr.first_name + ' ' + tr.last_name AS Trainer_name,
    NULL AS training_enroll_date,  -- Since there's no enrollment, set to NULL
    (SELECT COUNT(*) FROM Fe_training_enrollment WHERE training_id = t.training_id AND ISNULL(is_deleted, 0) = 0) AS total_enrolled_customers,
    t.training_id,
    t.agency_id,
    t.training_title,
    t.training_progress,
    t.training_status,
    t.training_category,
    t.location_lat,
    t.location_lng,
    t.from_date,
    t.to_date,
    t.duration,
    (SELECT TOP 1 tm.media_path 
       FROM fe_training_media tm 
       WHERE tm.training_id = t.training_id 
       AND ISNULL(tm.is_deleted, 0) = 0 
       AND tm.category = 'Banner'
       ORDER BY tm.create_date ASC) AS photo_path,
    t.fee,
    t.create_date,
    CAST(AVG(tf.rating) AS FLOAT) AS rating,
    t.details
FROM 
    Fe_trainings t
LEFT JOIN 
    Fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted, 0) = 0
LEFT JOIN 
    Fe_trainers tr ON tr.trainer_id = t.trainer_id AND ISNULL(tr.is_deleted, 0) = 0
LEFT JOIN 
    Fe_training_enrollment te ON t.training_id = te.training_id AND te.customer_id = @CustomerId
LEFT JOIN 
    fe_training_feedback tf ON tf.training_id = t.training_id AND ISNULL(tf.is_deleted, 0) = 0  -- Link feedback to training_id
WHERE 
    te.customer_id IS NULL  -- Exclude trainings where the user is already enrolled
    AND ISNULL(t.is_deleted, 0) = 0
GROUP BY 
    a.company_name,
    a.photo_path,
    a.phone,
    tr.first_name,
    tr.last_name,
    t.training_id,
    t.agency_id,
    t.training_title,
    t.training_progress,
    t.training_status,
    t.location_lat,
    t.location_lng,
    t.from_date,
    t.to_date,
    t.duration,
    t.training_category,
    t.fee,
    t.create_date,
    t.details
ORDER BY 
    rating DESC";


        public static string GetTrainingsCustomerNotEnrolledQuery => @"
           
     SELECT
                a.company_name AS agency_name,
                a.photo_path AS agency_photo,
                a.phone AS agency_phone,
                tr.first_name + ' ' + tr.last_name AS Trainer_name,
                NULL AS training_enroll_date,  -- Since there's no enrollment, set to NULL
                (SELECT COUNT(*) FROM Fe_training_enrollment WHERE training_id = t.training_id AND ISNULL(is_deleted, 0) = 0) AS total_enrolled_customers,
                t.training_id,
                t.agency_id,
                t.training_title,
                t.training_progress,
                t.training_status,
                t.training_category,
                t.location_lat,
                t.location_lng,
                t.from_date,
                t.to_date,
                t.duration,
                (SELECT TOP 1 tm.media_path 
       FROM fe_training_media tm 
       WHERE tm.training_id = t.training_id 
       AND ISNULL(tm.is_deleted, 0) = 0 
       AND tm.category = 'Banner'
       ORDER BY tm.create_date ASC) AS photo_path,
                t.fee,
                t.create_date
            
            FROM Fe_trainings t
            LEFT JOIN Fe_agency a ON a.agency_id = t.agency_id AND ISNULL(a.is_deleted, 0) = 0
            LEFT JOIN Fe_training_enrollment te ON t.training_id = te.training_id AND te.customer_id = @CustomerId
            LEFT JOIN Fe_trainers tr ON tr.trainer_id = t.trainer_id AND ISNULL(tr.is_deleted, 0) = 0
            Left  JOIN fe_training_media tm ON tm.training_id = t.training_id AND ISNULL(tr.is_deleted, 0) = 0
            WHERE
                te.customer_id IS NULL
                AND ISNULL(t.is_deleted, 0) = 0
            GROUP BY 
            a.company_name,
            a.photo_path,
            a.phone,
            tr.first_name,
            t.training_category,
            tr.last_name,
            te.create_date,
            t.training_id,
            t.agency_id,
            t.training_title,
            t.training_progress,
            t.training_status,
            t.location_lat,
            t.location_lng,
            t.from_date,
            t.to_date,
            t.duration,
            t.photo_path,
            t.fee,
            t.training_id,
            t.create_date";
    }
}
