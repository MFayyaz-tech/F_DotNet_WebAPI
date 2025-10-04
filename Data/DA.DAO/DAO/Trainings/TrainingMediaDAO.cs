using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Trainings
{
    public class TrainingMediaDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();
        public static string GetTrainingMedia => @"select * from fe_training_media 
                                                    where isnull(is_deleted,0) = 0 AND training_id = @TrainingId AND ISNULL(category,'Media') = 'Media'";

        public static string GetTrainingBanner => @"select * from fe_training_media 
                                                    where isnull(is_deleted,0) = 0 AND training_id = @TrainingId AND category = 'Banner'";
    }
}
