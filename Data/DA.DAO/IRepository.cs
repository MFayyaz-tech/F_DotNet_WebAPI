using Common;
using Common.Helper;
using Entities.Base;
using ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public interface IRepository<T> where T : BaseEntity 
    {
        IDataAccess DataAccess { get; }
        /// <summary>
        /// Select the list of entities based on passing criteria
        /// </summary>
        ///  <param name="database">Data base where to run store procedure</param>
        /// <param name="criteria">Use to filter the entities</param>
        /// <returns></returns>
        IEnumerable<T> SelectBySP(Database database, string spName, object criteria);
       
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        SPResult ExecuteQuery(string query, object parameters=null, SqlConnection connection = null, SqlTransaction transaction = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        long Insert(T entity, SqlConnection connection = null, SqlTransaction transaction = null);
        /// <summary>
        /// Insert enitities list in one go.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        bool BulkInsert(List<T> entities, SqlConnection connection = null, SqlTransaction transaction = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        bool Update(T entity, SqlConnection connection = null, SqlTransaction transaction = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        bool Delete(T entity, SqlConnection connection = null, SqlTransaction transaction = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(Database database, long id, SqlConnection connection = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="getQuery"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(Database database, string getQuery, object parameters = null, SqlConnection connection = null, SqlTransaction transaction = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Database database, string[] criteria);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        T GetByQuery(Database database, object parameters = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetCustomData(Database database, string query );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        //IEnumerable<T> CustomRead(Database database);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="database"></param>
        ///// <returns></returns>
        //object CustomOperation(Database database);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="database"></param>
        ///// <param name="query"></param>
        ///// <returns></returns>
        ////IEnumerable<T> GetSearchData(Database database, Dictionary<string, string> parameters, bool addWhereClaus = true);

        long GetNextIdentityId(string tableName, SqlConnection connection = null, SqlTransaction transaction = null);
        
        IEnumerable<T> GetSearchData(Database database, object parameters = null);
        bool Archive(object parameters, SqlConnection connection = null, SqlTransaction transaction = null);
    }
}
