using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataFunc.Commands;

namespace DataFunc.DataAdapters
{
    public static class DataAdapterFunctions
    {
        public static T ToDbDataAdapter<T>(this DbCommand command)
            where T : DbDataAdapter, new()
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            
            return new T { SelectCommand = command };
        }

        public static T ToDbDataAdapterFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName, IEnumerable<DbParameter> parameters)
            where T : DbDataAdapter, new()
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException(nameof(storedProcedureName));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException();
            
            return connection.ToStoredProcedureDbCommand(storedProcedureName, parameters).ToDbDataAdapter<T>();
        }

        public static T ToDbDataAdapterFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName)
            where T : DbDataAdapter, new()
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException(nameof(storedProcedureName));

            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException();
            
            return connection.ToStoredProcedureDbCommand(storedProcedureName).ToDbDataAdapter<T>();
        }
    }
}
