using Dapper;
using Common;
using Common.Helper;
using ORM;
using Entities.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper.Bulk;

namespace ORM.MSSQL
{
    public class DataContext<T> : IDataContext<T> where T: BaseEntity
    {
        private IDataAccess _dataAccess;
        public DataContext(IDataAccess dataAccess)
        { 
            _dataAccess = dataAccess;
        }

        public IDataAccess DataAccess => _dataAccess;

      

        public IEnumerable<T> SelectBySP(Database database, string procedureName, object paramsObjects = null, int? commandTimeout = null)
        {
            using (var connection = _dataAccess.GetActiveConnection(database))
            {
                IEnumerable<T> data = connection.Query<T>(procedureName, paramsObjects, commandTimeout: commandTimeout,
                    commandType: CommandType.StoredProcedure);
                _dataAccess.Close();
                return data;
            }
        }

        public SPResult UpdateBySP(Database database, string procedureName, object paramsObjects = null, SqlConnection sqlConnection = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            SPResult result;
             
            using (var connection = sqlConnection != null ? sqlConnection : _dataAccess.GetActiveConnection(database))
            {
                var task = connection.QueryAsync<SPResult>(procedureName, paramsObjects, transaction, commandTimeout, commandType: CommandType.StoredProcedure);
                result = task.Result.FirstOrDefault();
            }

            if (sqlConnection == null)
                _dataAccess.Close();

            return result;
        }

        public T UpdateAndGet(Database database, string procedureName, object paramsObjects = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            T result;
            using (var connection = _dataAccess.GetActiveConnection(database))
            {
                var task = connection.QueryAsync<T>(procedureName, paramsObjects, transaction, commandTimeout, commandType: CommandType.StoredProcedure);
                result = task.Result.FirstOrDefault();
                _dataAccess.Close();
            }
             
            return result;
        }

        public SPResult ExecuteQuery(Database database, string query, object paramsObjects = null, SqlConnection sqlConnection = null, SqlTransaction transaction = null)
        {
            SPResult result;
            if (sqlConnection != null)
            {
                var task = sqlConnection.Query<SPResult>(query, paramsObjects, transaction, commandType: CommandType.Text);
                result = task.FirstOrDefault();
            }
            else
            {
                using (var connection = _dataAccess.GetActiveConnection(database))
                {
                    var task = connection.Query<SPResult>(query, paramsObjects, transaction, commandType: CommandType.Text);
                    result = task.FirstOrDefault();
                }
                _dataAccess.Close();
            }

            return result;
        }

        public bool BulkInsert(Database database, List<T> entities, SqlConnection sqlConnection = null, SqlTransaction transaction = null)
        {
            bool success = true;
            if (sqlConnection != null)
            {
                sqlConnection.BulkInsert(entities, transaction);
            }
            else
            {
                using (var connection =  _dataAccess.GetActiveConnection(database))
                {
                    connection.BulkInsert(entities);
                }
                _dataAccess.Close();
            }

            return success;
        }

        public long Insert(Database database, T entity, SqlConnection sqlConnection = null, SqlTransaction transaction = null)
        {
            long id = 0;

            if (sqlConnection != null)
            {
                id = sqlConnection.Insert(entity, transaction);
            }
            else
            {
                using (var connection = _dataAccess.GetActiveConnection(database))
                {

                    id = connection.Insert(entity);
                }
                _dataAccess.Close();
            }


            return id;
        }

        public bool Update(Database database, T entity, SqlConnection sqlConnection = null, SqlTransaction transaction = null)
        {
            bool success = false;
            if (sqlConnection != null)
            {
                success = sqlConnection.Update(entity, transaction);
            }
            else
            {
                using (var connection = _dataAccess.GetActiveConnection(database))
                {
                    success = connection.Update(entity);
                }
            }

            if (sqlConnection == null)
                _dataAccess.Close();

            return success;
        }

        public bool Delete(Database database, T entity, SqlConnection sqlConnection = null, SqlTransaction transaction = null)
        {
            bool success = false;
            if (sqlConnection != null)
            {
                success = sqlConnection.Delete(entity, transaction);
            }
            else
            {
                using (var connection = _dataAccess.GetActiveConnection(database))
                {
                    success = connection.Delete(entity);
                }
                _dataAccess.Close();
            }

            return success;
        }

        public T Get(Database database, long id, SqlConnection connection = null)
        {
            if (connection != null)
            {
                return connection.Get<T>(id);
            }
            else
            {
                using (var conn = _dataAccess.GetActiveConnection(database))
                {
                    T data = conn.Get<T>(id);
                    _dataAccess.Close();
                    return data;
                }
            }
           
        }

        public IEnumerable<T> GetByQuery(Database database, string query, object parameters = null, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            if (connection != null)
            {
                IEnumerable<T> data = connection.Query<T>(query, parameters, transaction, commandType: CommandType.Text);
                return data;
            }
            else
            {
                using (var con =  _dataAccess.GetActiveConnection(database))
                {
                    IEnumerable<T> data = con.Query<T>(query, parameters, transaction, commandType: CommandType.Text);
                    _dataAccess.Close();
                    return data;
                }
            }
        }

       
        public List<Dictionary<string, object>> GetCustomData(Database database, string sqlQuery)
        {
            var table = new List<Dictionary<string, object>>();
            using (var connection = _dataAccess.GetActiveConnection(database))
            {

                //var reader = connection.ExecuteReader(
                //    sqlQuery,
                //    null,
                //    null,
                //    commandType: CommandType.Text
                //    );
                var reader = connection.ExecuteReader(sqlQuery);
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[reader.GetName(i)] = reader[i];
                    table.Add(row);
                }

            }
            return table;
        }

    }
}
