using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Customer
{
    public class FeCustomerDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();
        public static string GetCustomerByUserIdQuery => @"select * from fe_customer where user_id = @UserId AND ISNULL(is_deleted,0) = 0";
        public static string GetCustomerByCustomerIdQuery => @"select * from fe_customer where customer_id = @CustomerId AND ISNULL(is_deleted,0) = 0";
    }
}
