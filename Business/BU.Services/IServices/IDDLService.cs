
using DTO.Core;
using Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IDDLService
    {
        IEnumerable<list_item> LoadData(DDLItemsDTO ddlDTO);
    }
}
