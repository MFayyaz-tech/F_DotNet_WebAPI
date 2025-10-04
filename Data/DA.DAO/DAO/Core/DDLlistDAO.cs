using Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.DAO.Core
{
    public class DDLlistDAO : IDAO
    {
        public static string rolelist = "select role_id t1, role_name t2 from [role] where isnull(is_deleted, 0) <> 1";

		public string GetAllQyery => throw new NotImplementedException();

		public string GetSingleQuery => throw new NotImplementedException();

		public string GridDataQuery => throw new NotImplementedException();

		public string DoArchiveQuery => throw new NotImplementedException();
	}
}
