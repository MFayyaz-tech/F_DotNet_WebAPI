using Common;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAO
{
    public interface IDAO
    {
		string GetAllQyery { get; }
		string GetSingleQuery { get; }
		string GridDataQuery { get; }
		string DoArchiveQuery { get; }
	}
}
