using Common;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ORM
{
    public interface IDataContext<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IDataAccess DataAccess { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="procedureName"></param>
        /// <param name="paramsObjects"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> SelectBySP(Database database, string procedureName, object paramsObjects = null, int? commandTimeout = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="procedureName"></param>
        /// <param name="paramsObjects"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        SPResult UpdateBySP(Database database, string procedureName, object paramsObjects = null, SqlConnection sqlConnection = null, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="procedureName"></param>
        /// <param name="paramsObjects"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T UpdateAndGet(Database database, string procedureName, object paramsObjects = null, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="query"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        SPResult ExecuteQuery(Database database,  string query, object paramsObjects = null, SqlConnection sqlConnection = null, SqlTransaction transaction = null);
        /// <summary>
        /// Insert single entity
        /// </summary>
        /// <param name="database"></param>
        /// <param name="entity"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        long Insert(Database database, T entity, SqlConnection sqlConnection = null, SqlTransaction transaction = null);
        /// <summary>
        /// Insert list of entities in one go.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="entities"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        bool BulkInsert(Database database, List<T> entities, SqlConnection sqlConnection = null, SqlTransaction transaction = null);
        /// <summary>
        /// Update single entity
        /// </summary>
        /// <param name="database"></param>
        /// <param name="entity"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        bool Update(Database database, T entity, SqlConnection sqlConnection = null, SqlTransaction transaction = null);
        /// <summary>
        /// Update list entities in one go.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="entities"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        //bool BulkUpdate(Database database, List<T> entities, SqlConnection sqlConnection = null);
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="database"></param>
        /// <param name="entity"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        bool Delete(Database database, T entity, SqlConnection sqlConnection = null, SqlTransaction transaction = null);
        /// <summary>
        /// Get single entity based on id.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(Database database, long id, SqlConnection connection = null);
        /// <summary>
        /// Get list of entities based on given query.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> GetByQuery(Database database, string query, object parameters=null, SqlConnection connection = null, SqlTransaction transaction = null);
        /// <summary>
        /// return data in list of dictionary..Just like json format.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetCustomData(Database database, string sqlQuery);
    }
}
