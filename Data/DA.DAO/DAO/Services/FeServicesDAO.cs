using System;
using DAO;

namespace DA.DAO.DAO.Services
{
	public class FeServicesDAO : IDAO
    {
		 
        public string GetAllQyery => throw new NotImplementedException();

        public string GetSingleQuery => throw new NotImplementedException();

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => throw new NotImplementedException();

        public static string getCustomerServices => @"select * from fe_services where is_obsulate = 0";
        public static string getServicesByCategories => @"select * from fe_services where category_id =  @CategoryId and is_obsulate = 0";

        public static string getAgencyServices => @"select * from fe_services where agency_id = @AgencyId AND is_obsulate = @isObsulate";

        public static string GetServiceDetailById => @"
                                                           select 
                                                           fa.company_name,
                                                           fa.phone,
                                                           fc.email_address,
                                                           fc.user_id,
                                                           fs.*
                                                           from fe_services  fs
                                                           LEFT JOIN fe_agency fa ON fs.agency_id = fa.agency_id
                                                           LEFT JOIN fe_users fc on fa.user_id = fc.user_id
                                                           WHERE services_id = @ServiceId";
                                                         
                                                             }
}

