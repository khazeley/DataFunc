using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DataFunc.Commands;

namespace DataFunc.DataReaders
{
    public static class DataReaders
    {
        public static DbDataReader ToDbDataReaderFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName, IEnumerable<DbParameter> parameters)
            where T : DbDataReader, new()
            => connection.ToStoredProcedureDbCommand(storedProcedureName, parameters).ExecuteReader();

        public static SqlDataReader ToDataReaderFromStoredProcedure(this SqlConnection connection, string storedProcedureName, SqlParameter[] parameters)
            => connection.ToStoredProcedureCommand(storedProcedureName, parameters).ExecuteReader();

        public static SqlDataReader ToDataReaderFromStoredProcedure(this SqlConnection connection, string storedProcedureName)
            => connection.ToStoredProcedureCommand(storedProcedureName).ExecuteReader();

        public static async Task<SqlDataReader> ToDataReaderFromStoredProcedureAsync(this SqlConnection connection, string storedProcedureName, SqlParameter[] parameters)
            => await connection.ToStoredProcedureCommand(storedProcedureName, parameters).ExecuteReaderAsync();

        public static async Task<SqlDataReader> ToDataReaderFromStoredProcedureAsync(this SqlConnection connection, string storedProcedureName)
            => await connection.ToStoredProcedureCommand(storedProcedureName).ExecuteReaderAsync();
    }
}
