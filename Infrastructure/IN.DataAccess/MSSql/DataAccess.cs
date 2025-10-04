using Common;
using Common.Helper;
using ORM;
using ORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM.MSSql
{
    public class DataAccess : IDataAccess
    {
        private Dictionary<Database, SqlConnection> _ActiveConnections = new Dictionary<Database, SqlConnection>();
        private Dictionary<SqlConnection, SqlTransaction> _ActiveTransactions = new Dictionary<SqlConnection, SqlTransaction>();

        public Dictionary<SqlConnection, SqlTransaction> ActiveTransactions
        {
            get { return _ActiveTransactions; }
        }

        private bool _IsTransactionStarted = false;

        public bool IsTransactionStarted
        {
            get { return _IsTransactionStarted; }

        }

        List<DBConnection> _dbConnections;
        string connectionStringBuilder = "server={0};User Id={1};password={2};database={3};";

        public DataAccess(List<DBConnection> dbConnection)
        {
            _dbConnections = dbConnection;
            _IsTransactionStarted = false;
        }

        private string GetConnectionString(Database db)
        {
            string ConnectionString = "";
            DBConnection dbConnection = _dbConnections.Where(c => c.Name == db.ToString()).FirstOrDefault();
            if (dbConnection == null)
                throw new Exception("Unable to load database configurations.");

            //decrypt password
            var decryptedpassword = CryptoEngine.Decrypt(dbConnection.Password);

            var csb = new SqlConnectionStringBuilder(string.Format(connectionStringBuilder,
                        dbConnection.Server, dbConnection.UserId, decryptedpassword, dbConnection.Database));

            ConnectionString = csb.ConnectionString;

            return ConnectionString;
        }

        public SqlConnection GetActiveConnection(Database db)
        {
            SqlConnection mConnection = null;

            try
            {
                if (_ActiveConnections.ContainsKey(db) == true)
                {
                    mConnection = _ActiveConnections[db];

                    //open connection if it is not opened
                    if (mConnection.State != ConnectionState.Open)
                    {
                        mConnection.ConnectionString = GetConnectionString(db);
                        mConnection.Open();
                    }
                }
                else
                {
                    mConnection = new SqlConnection();
                    mConnection.ConnectionString = GetConnectionString(db);
                    mConnection.Open();
                    _ActiveConnections.Add(db, mConnection);

                }

                // if transaction flag is set then begin transaction if it is not started
                if (mConnection != null && this.IsTransactionStarted == true)
                {
                    if (_ActiveTransactions.ContainsKey(mConnection) == false)
                    {
                        _ActiveTransactions.Add(mConnection, mConnection.BeginTransaction());
                    }

                }
            }
            catch (SqlException ex)
            {
                _ActiveTransactions.Clear();
                _IsTransactionStarted = false;
                throw ex;
            }
            catch (Exception ex)
            {
                _ActiveTransactions.Clear();
                _IsTransactionStarted = false;
                throw ex;
            }

            return mConnection;
        }

        public bool BeginTransaction()
        {
            _IsTransactionStarted = true;
            return _IsTransactionStarted;
        }

        public bool CommitTransaction()
        {
            try
            {
                if (_IsTransactionStarted == true)
                {
                    foreach (SqlTransaction transaction in _ActiveTransactions.Values)
                    {
                        transaction.Commit();
                    }
                    _ActiveTransactions.Clear();
                }
                _IsTransactionStarted = false;
            }

            catch (SqlException ex)
            {
                _ActiveTransactions.Clear();
                _IsTransactionStarted = false;

                if (!ex.Message.Contains("This SqlTransaction has completed"))
                {
                    //throw new Exception("SYSTEM_STARTUP", ex.Message);
                }

            }
            catch (Exception ex)
            {
                _ActiveTransactions.Clear();
                _IsTransactionStarted = false;

                if (!ex.Message.Contains("This SqlTransaction has completed"))
                {
                    //throw new Exception("SYSTEM_STARTUP", ex.Message);
                }
            }
            finally
            {
                Close();
            }

            return true;
        }

        public bool RollbackTransaction()
        {
            try
            {
                if (_IsTransactionStarted == true)
                {
                    foreach (SqlTransaction transaction in _ActiveTransactions.Values)
                    {
                        transaction.Rollback();
                    }
                    _ActiveTransactions.Clear();
                }
            }
            catch (SqlException ex)
            {
                _ActiveTransactions.Clear();
                _IsTransactionStarted = false;

                if (!ex.Message.Contains("This SqlTransaction has completed"))
                {
                    //throw new AppException("SYSTEM_STARTUP", ex.Message, LogLevelType.SQLERROR);
                }
            }
            catch (Exception ex)
            {
                _ActiveTransactions.Clear();
                _IsTransactionStarted = false;

                if (!ex.Message.Contains("This SqlTransaction has completed"))
                {
                    //throw new AppException("SYSTEM_STARTUP", ex.Message, LogLevelType.SQLERROR);
                }
            }
            finally
            {
                _IsTransactionStarted = false;

                Close();
            }

            return true;
        }

        public void Close()
        {

            if (_IsTransactionStarted == false)
            {
                try
                {
                    foreach (SqlConnection conn in _ActiveConnections.Values)
                    {
                        if (conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _ActiveTransactions.Clear();
                    _IsTransactionStarted = false;
                    throw ex;
                }
                catch (Exception ex)
                {
                    _ActiveTransactions.Clear();
                    _IsTransactionStarted = false;
                    throw ex;
                }
                finally
                {
                    _ActiveConnections.Clear();
                }
            }
        }

        public SqlTransaction GetActiveTransaction(SqlConnection connection)
        {
           return _ActiveTransactions[connection];
        }
    }
}
