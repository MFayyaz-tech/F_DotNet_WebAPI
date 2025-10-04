using Common;
using Common.Helper;
using Entities.Base;
using ORM;
using ORM.MSSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        public readonly IDataContext<T> _dataContext;
        public readonly IDAO _objSO;

        public IDataAccess DataAccess => _dataContext.DataAccess;

        public Repository(IDataContext<T> dataContext)
        {
            _dataContext = dataContext;
            _objSO = DAOFactory<T>.GetDAO();
        }
      

        public IEnumerable<T> SelectBySP(Database database, string spName, object criteria)
        {
            return  _dataContext.SelectBySP(database, spName, criteria).ToList();
        }

        public SPResult ExecuteQuery(string query, object parameters = null, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            return _dataContext.ExecuteQuery(Database.MAIN, query, parameters, connection, transaction);
        }

        public long Insert(T entity, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            entity.Create_date = DateTime.Now;
            entity.Update_date = null;
            entity.Update_date = null;
            entity.Updated_by = null;
            return _dataContext.Insert(Database.MAIN, entity, connection, transaction);
        }
        public bool BulkInsert(List<T> entities, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            entities.ForEach(e => { e.Create_date = DateTime.Now; e.Update_date = null; }) ;
            return _dataContext.BulkInsert(Database.MAIN, entities, connection, transaction);
        }

        public bool Update(T entity, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            entity.Update_date = DateTime.Now;
            return _dataContext.Update(Database.MAIN, entity, connection, transaction);
        }

        public bool Delete(T entity, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            entity.Update_date = DateTime.Now;
            return _dataContext.Delete(Database.MAIN, entity, connection, transaction);
        }

        public T Get(Database database, long id, SqlConnection connection = null)
        {
            return _dataContext.Get(database, id, connection);
        }

        public IEnumerable<T> GetList(Database database, string getQuery, object parameters = null, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            return _dataContext.GetByQuery(Database.MAIN, getQuery, parameters, connection, transaction);
        }

        public IEnumerable<T> GetAll(Database database, string[] criteria)
        {
            if (string.IsNullOrEmpty(_objSO.GetAllQyery))
                throw new Exception("Get All query is not implemented on given <T>");
            return _dataContext.GetByQuery(Database.MAIN, string.Format(_objSO.GetAllQyery, criteria));
        }
        public T GetByQuery(Database database, object parameters = null)
        {
            return _dataContext.GetByQuery(Database.MAIN,  _objSO.GetSingleQuery, parameters).FirstOrDefault();
        }

        //public IEnumerable<T> CustomRead(Database database)
        //{
        //    return _objSO.CustomRead(database, _dataContext);
        //}

        //public object CustomOperation(Database database)
        //{
        //    return _objSO.CustomOperation(database, _dataContext);
        //}

        public List<Dictionary<string, object>> GetCustomData(Database database, string query)
        {
            return _dataContext.GetCustomData(database, query);
        }

        public IEnumerable<T> GetSearchData(Database database, object parameters = null)
        {
            if (string.IsNullOrEmpty(_objSO.GridDataQuery))
                throw new Exception("Grid query is not implemented on given <T>");
            return _dataContext.GetByQuery(Database.MAIN, _objSO.GridDataQuery, parameters);
        }

        public long GetNextIdentityId(string tableName, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            string query = $" SELECT  IDENT_CURRENT('{tableName}') Id";
            SPResult result = _dataContext.ExecuteQuery(Database.MAIN, query, null, connection, transaction);
            return result.Id;
        }

        public bool Archive(object parameters, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            if (string.IsNullOrEmpty(_objSO.DoArchiveQuery))
                throw new Exception("Archive query is not implemented on given <T>");
             _dataContext.ExecuteQuery(Database.MAIN, _objSO.DoArchiveQuery, parameters, connection, transaction);
            return true;
        }
    }
}
