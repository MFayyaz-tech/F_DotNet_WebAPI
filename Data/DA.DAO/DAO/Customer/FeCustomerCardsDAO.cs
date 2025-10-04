using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DA.DAO.DAO.Customer
{
    public class FeCustomerCardsDAO : IDAO
    {
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => @"update fe_customer_cards set is_deleted = 1,updated_by = @UserId,update_date = getdate() where customer_card_id = @CustomerCardId";
        public static string GetCustomerCardsByCustomerIdQuery => "select * from fe_customer_cards where ISNULL(is_deleted,0) = 0 and customer_id = @CustomerId";
    }
}
