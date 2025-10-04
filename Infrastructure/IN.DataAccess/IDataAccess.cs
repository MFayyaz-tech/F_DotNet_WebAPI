using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace ORM
{
    public interface IDataAccess
    {
        SqlConnection GetActiveConnection(Database database);
        SqlTransaction GetActiveTransaction(SqlConnection connection);
        bool BeginTransaction();
        bool CommitTransaction();
        bool RollbackTransaction();
        void Close();

    }
}
