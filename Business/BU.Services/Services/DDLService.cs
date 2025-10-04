using DAO;
using DAO.DAO.Core;
using DTO.Core;
using Entities.Core;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
   public class DDLService : IDDLService
    {
        private readonly IRepository<list_item> _repDDLlist;

        public DDLService(IRepository<list_item> repDDLlist)
        {
            _repDDLlist = repDDLlist;
        }

        public IEnumerable<list_item> LoadData(DDLItemsDTO ddlDTO)
        {
            string query = string.Empty;

            object parameters = null;
      

            switch (ddlDTO.Code)
            {
               
                case "roles":
                    query = DDLlistDAO.rolelist;
                    break;
            }
            return _repDDLlist.GetList(Common.Database.MAIN, query, parameters);
        }
    }
}
