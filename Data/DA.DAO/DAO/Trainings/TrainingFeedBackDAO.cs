using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Trainings
{
    public class TrainingFeedBackDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public static string GetCustomerFeedBacks => @"select tf.training_feedback_id,tf.feedback,tf.rating,tf.customer_id,
t.training_title,
t.create_date
 From fe_training_feedback tf
left join fe_trainings t on tf.training_id = t.training_id AND ISNULL(t.is_deleted,0) = 0
where tf.customer_id = @CustomerId  AND ISNULL(tf.is_deleted,0) = 0";


        public string DoArchiveQuery => throw new NotImplementedException();
        public static string GetTrainingFeedbacksByTrainingId => @"select c.first_name+' '+c.last_name customer_name, tfb.* 
From fe_training_feedback tfb 
JOIN fe_customer c ON c.customer_id = tfb.customer_id
where ISNULL(tfb.is_deleted,0) = 0 AND tfb.training_id = @TrainingId";
    }

}
