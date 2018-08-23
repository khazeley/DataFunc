using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataFunc.Commands;
using DataFunc.Connections;
using DataFunc.DataAdapters;
using DataFunc.DataSets;
using DataFunc.DataTables;

namespace DataFunc.Facades.Sql
{
    public static class SqlFacade
    {
        #region SqlConnection

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection"></see> class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the SQL Server database.</param>
        /// <returns>A new instance of the <see cref="T:System.Data.SqlClient.SqlConnection"></see> class.</returns>
        public static SqlConnection ToSqlConnection(this string connectionString) => connectionString.ToDbConnection<SqlConnection>();

        /// <summary>
        /// Initializes and opens a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection"></see> class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the SQL Server database.</param>
        /// <returns>A new instance of the <see cref="T:System.Data.SqlClient.SqlConnection"></see> class with <see cref="T:System.Data.ConnectionState.Open"></see>.</returns>
        public static SqlConnection ToOpenSqlConnection(this string connectionString) => connectionString.ToOpenDbConnection<SqlConnection>();

        /// <summary>
        /// Asynchronously initializes and opens a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection"></see> class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the SQL Server database.</param>
        /// <returns></returns>
        public static async Task<SqlConnection> ToOpenSqlConnectionAsync(this string connectionString) => await connectionString.ToOpenDbConnectionAsync<SqlConnection>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<SqlConnection> ToOpenSqlConnectionAsync(this string connectionString, CancellationToken token) => await connectionString.ToOpenDbConnectionAsync<SqlConnection>(token);

        #endregion

        #region SqlCommand

        public static SqlCommand ToStoredProcedureSqlCommand(this SqlConnection connection, string storedProcedureName, IEnumerable<SqlParameter> parameters)
            => connection.ToStoredProcedureDbCommand<SqlCommand>(storedProcedureName, parameters);

        public static SqlCommand ToStoredProcedureSqlCommand(this SqlConnection connection, string storedProcedureName)
            => connection.ToStoredProcedureDbCommand<SqlCommand>(storedProcedureName);

        #endregion

        #region SqlDataAdapter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataAdapter ToSqlDataAdapterFromStoredProcedure(this SqlConnection connection, string storedProcedureName, IEnumerable<SqlParameter> parameters)
            => connection.ToStoredProcedureSqlCommand(storedProcedureName, parameters).ToDbDataAdapter<SqlDataAdapter>();

        public static SqlDataAdapter ToSqlDataAdapterFromStoredProcedure(this SqlConnection connection, string storedProcedureName)
            => connection.ToStoredProcedureSqlCommand(storedProcedureName).ToDbDataAdapter<SqlDataAdapter>();

        #endregion

        #region DataSet

        public static DataSet ToDataSet(this SqlConnection connection, string storedProcedureName, IEnumerable<SqlParameter> parameters)
            => connection.ToSqlDataAdapterFromStoredProcedure(storedProcedureName, parameters).ToDataSet();

        public static DataSet ToDataSet(this SqlConnection connection, string storedProcedureName)
            => connection.ToSqlDataAdapterFromStoredProcedure(storedProcedureName).ToDataSet();

        #endregion

        #region DataTable

        public static DataTable ExecuteStoredProcedureToDataTable(this SqlConnection connection, string storedProcedureName, IEnumerable<SqlParameter> parameters)
            => connection.ToSqlDataAdapterFromStoredProcedure(storedProcedureName, parameters).ToDataTable();

        public static DataTable ExecuteStoredProcedureToDataTable(this SqlConnection connection, string storedProcedureName)
            => connection.ToSqlDataAdapterFromStoredProcedure(storedProcedureName).ToDataTable();

        public static DataTable ExecuteStoredProcedureToDataTableInTransaction(this SqlConnection connection, string storedProcedureName, IEnumerable<SqlParameter> parameters)
        {
            var command = connection.ToStoredProcedureSqlCommand(storedProcedureName, parameters);
            var transaction = connection.BeginTransaction();

            DataTable retVal;

            try
            {
                retVal = command.ToDbDataAdapter<SqlDataAdapter>().ToDataTable();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return retVal;
        }

        public static DataTable ExecuteStoredProcedureToDataTableInTransaction(this SqlConnection connection, string storedProcedureName)
        {
            var command = connection.ToStoredProcedureSqlCommand(storedProcedureName);
            var transaction = connection.BeginTransaction();

            DataTable retVal;

            try
            {
                retVal = command.ToDbDataAdapter<SqlDataAdapter>().ToDataTable();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }

            return retVal;
        }

        public static DataTable ExecuteStoredProcedureToDataTableInTransaction(this SqlConnection connection, string storedProcedureName, IEnumerable<SqlParameter> parameters, IsolationLevel isolationLevel)
        {
            var command = connection.ToStoredProcedureSqlCommand(storedProcedureName, parameters);
            var transaction = connection.BeginTransaction(isolationLevel);

            DataTable retVal;

            try
            {
                retVal = command.ToDbDataAdapter<SqlDataAdapter>().ToDataTable();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }

            return retVal;
        }

        public static DataTable ExecuteStoredProcedureToDataTableInTransaction(this SqlConnection connection, string storedProcedureName, IsolationLevel isolationLevel)
        {
            var command = connection.ToStoredProcedureSqlCommand(storedProcedureName);
            var transaction = connection.BeginTransaction(isolationLevel);

            DataTable retVal;

            try
            {
                retVal = command.ToDbDataAdapter<SqlDataAdapter>().ToDataTable();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }

            return retVal;
        }

        #endregion
    }
}
