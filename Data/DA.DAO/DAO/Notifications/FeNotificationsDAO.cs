using System;
using DAO;

namespace DA.DAO.DAO.Notifications
{
    public class FeNotificationsDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();

        public static string GetUserToken => @"select * from fe_notification_tokens where user_id = @UserId";
    }
}

