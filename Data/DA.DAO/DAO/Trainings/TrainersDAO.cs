using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Trainings
{
    public class TrainersDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();

        public static string GetTrainersByAgencyId => @"select * From fe_trainers where isnull(is_deleted,0) = 0 and agency_id = @AgencyId";
    }
}
