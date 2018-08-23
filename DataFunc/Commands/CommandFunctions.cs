using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataFunc.Commands
{
    public static class CommandFunctions
    {
        public static DbCommand ToDbCommand(this DbConnection connection, string commandText, IEnumerable<DbParameter> parameters, CommandType commandType)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            
            var parameterArray = parameters.ToArray();
            if (parameterArray.Any())
                command.Parameters.AddRange(parameterArray);
            
            return command;
        }

        public static T ToDbCommand<T>(this DbConnection connection, string commandText, IEnumerable<DbParameter> parameters, CommandType commandType)
            where T : DbCommand, new()
            => connection.ToDbCommand(commandText, parameters, commandType) as T;
        
        public static DbCommand ToDbCommand(this DbConnection connection, string commandText, CommandType commandType)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }

        public static T ToDbCommand<T>(this DbConnection connection, string commandText, CommandType commandType)
            where T : DbCommand, new()
            => connection.ToDbCommand(commandText, commandType) as T;

        public static DbCommand ToStoredProcedureDbCommand(this DbConnection connection, string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return connection.ToDbCommand(storedProcedureName, parameters, CommandType.StoredProcedure);
        }

        public static T ToStoredProcedureDbCommand<T>(this DbConnection connection, string storedProcedureName, IEnumerable<DbParameter> parameters)
            where T : DbCommand, new()
            => connection.ToDbCommand<T>(storedProcedureName, parameters, CommandType.StoredProcedure);

        public static DbCommand ToStoredProcedureDbCommand(this DbConnection connection, string storedProcedureName)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName");

            return connection.ToDbCommand(storedProcedureName, CommandType.StoredProcedure);
        }

        public static T ToStoredProcedureDbCommand<T>(this DbConnection connection, string storedProcedureName)
            where T : DbCommand, new()
            => connection.ToDbCommand<T>(storedProcedureName, CommandType.StoredProcedure);

        public static DbCommand ToTableDirectDbCommand(this DbConnection connection, string commandText, IEnumerable<DbParameter> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return connection.ToDbCommand(commandText, parameters, CommandType.TableDirect);
        }

        public static DbCommand ToTableDirectDbCommand(this DbConnection connection, string commandText)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");

            return connection.ToDbCommand(commandText, CommandType.TableDirect);
        }

        public static DbCommand ToTextDbCommand(this DbConnection connection, string commandText, IEnumerable<DbParameter> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return connection.ToDbCommand(commandText, parameters, CommandType.Text);
        }

        public static DbCommand ToTextDbCommand(this DbConnection connection, string commandText)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");

            return connection.ToDbCommand(commandText, CommandType.Text);
        }

        public static async Task<DbCommand> ToDbCommandAsync(this Task<DbConnection> connection, string commandText, IEnumerable<DbParameter> parameters, CommandType commandType)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));

            var connectionResult = await connection;
            var command = connectionResult.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            var parameterArray = parameters.ToArray();
            if (parameterArray.Any())
                command.Parameters.AddRange(parameterArray);

            return command;
        }

        public static async Task<DbCommand> ToDbCommandAsync(this Task<DbConnection> connection, string commandText, Task<IEnumerable<DbParameter>> parameters, CommandType commandType)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));

            var connectionResult = await connection;
            var command = connectionResult.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            var parameterArray = (await parameters).ToArray();
            if (parameterArray.Any())
                command.Parameters.AddRange(parameterArray);

            return command;
        }

        public static async Task<DbCommand> ToDbCommandAsync(this Task<DbConnection> connection, string commandText, CommandType commandType)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));

            var command = (await connection).CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }

        public static async Task<DbCommand> ToStoredProcedureDbCommandAsync(this Task<DbConnection> connection, string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException(nameof(storedProcedureName), nameof(storedProcedureName));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));

            return await connection.ToDbCommandAsync(storedProcedureName, parameters, CommandType.StoredProcedure);
        }

        public static async Task<DbCommand> ToStoredProcedureDbCommandAsync(this Task<DbConnection> connection, string storedProcedureName, Task<IEnumerable<DbParameter>> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException(nameof(storedProcedureName), nameof(storedProcedureName));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));

            return await connection.ToDbCommandAsync(storedProcedureName, parameters, CommandType.StoredProcedure);
        }

        public static async Task<DbCommand> ToStoredProcedureDbCommandAsync(this Task<DbConnection> connection, string storedProcedureName)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName");

            return await connection.ToDbCommandAsync(storedProcedureName, CommandType.StoredProcedure);
        }

        public static async Task<DbCommand> ToTableDirectDbCommandAsync(this Task<DbConnection> connection, string commandText, IEnumerable<DbParameter> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return await connection.ToDbCommandAsync(commandText, parameters, CommandType.TableDirect);
        }

        public static async Task<DbCommand> ToTableDirectDbCommandAsync(this Task<DbConnection> connection, string commandText, Task<IEnumerable<DbParameter>> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return await connection.ToDbCommandAsync(commandText, parameters, CommandType.TableDirect);
        }

        public static async Task<DbCommand> ToTableDirectDbCommandAsync(this Task<DbConnection> connection, string commandText)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");

            return await connection.ToDbCommandAsync(commandText, CommandType.TableDirect);
        }

        public static async Task<DbCommand> ToTextDbCommandAsync(this Task<DbConnection> connection, string commandText, IEnumerable<DbParameter> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return await connection.ToDbCommandAsync(commandText, parameters, CommandType.Text);
        }

        public static async Task<DbCommand> ToTextDbCommandAsync(this Task<DbConnection> connection, string commandText, Task<IEnumerable<DbParameter>> parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return await connection.ToDbCommandAsync(commandText, parameters, CommandType.Text);
        }

        public static async Task<DbCommand> ToTextDbCommandAsync(this Task<DbConnection> connection, string commandText)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");

            return await connection.ToDbCommandAsync(commandText, CommandType.Text);
        }



















        /// <summary>
        /// Returns a new SqlCommand
        /// </summary>
        /// <param name="connection"><see cref="T:System.Data.SqlClient.SqlConnection"></see> that represents the connection to an instance of SQL Server.</param>
        /// <param name="commandText">The text of the command to be executed</param>
        /// <param name="parameters">The SqlParameters for the command</param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout">The number of seconds for the command's timeout. Defaults to 120</param>
        /// <returns></returns>
        public static SqlCommand ToSqlCommand(this SqlConnection connection, string commandText, SqlParameter[] parameters, CommandType commandType, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));

            var command = new SqlCommand
            {
                CommandTimeout = commandTimeout,
                Connection = connection,
                CommandType = commandType,
                CommandText = commandText
            };

            if (parameters.Any())
                command.Parameters.AddRange(parameters);

            return command;
        }

        /// <summary>
        /// Returns a new SqlCommand
        /// </summary>
        /// <param name="connection"><see cref="T:System.Data.SqlClient.SqlConnection"></see> that represents the connection to an instance of SQL Server.</param>
        /// <param name="commandText">The text of the command to be executed</param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout">The number of seconds for the command's timeout. Defaults to 120</param>
        /// <returns></returns>
        public static SqlCommand ToSqlCommand(this SqlConnection connection, string commandText, CommandType commandType, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), "commandText");

            var command = new SqlCommand
            {
                CommandTimeout = commandTimeout,
                Connection = connection,
                CommandType = commandType,
                CommandText = commandText
            };

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"><see cref="T:System.Data.SqlClient.SqlConnection"></see> that represents the connection to an instance of SQL Server.</param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static SqlCommand ToStoredProcedureCommand(this SqlConnection connection, string storedProcedureName, SqlParameter[] parameters, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return connection.ToSqlCommand(storedProcedureName, parameters, CommandType.StoredProcedure, commandTimeout);
        }

        public static SqlCommand ToStoredProcedureCommand(this SqlConnection connection, string storedProcedureName, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName");

            return connection.ToSqlCommand(storedProcedureName, CommandType.StoredProcedure, commandTimeout);
        }

        public static SqlCommand ToTableDirectCommand(this SqlConnection connection, string commandText, SqlParameter[] parameters, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return connection.ToSqlCommand(commandText, parameters, CommandType.TableDirect, commandTimeout);
        }

        public static SqlCommand ToTableDirectCommand(this SqlConnection connection, string commandText, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");

            return connection.ToSqlCommand(commandText, CommandType.TableDirect, commandTimeout);
        }

        public static SqlCommand ToTextCommand(this SqlConnection connection, string commandText, SqlParameter[] parameters, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException("commandText", "commandText");
            if (parameters == null)
                throw new ArgumentNullException("parameters", "parameters");

            return connection.ToSqlCommand(commandText, parameters, CommandType.Text, commandTimeout);
        }

        public static SqlCommand ToTextCommand(this SqlConnection connection, string commandText, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), "commandText");

            return connection.ToSqlCommand(commandText, CommandType.Text, commandTimeout);
        }

        /// <summary>
        /// Returns a new SqlCommand
        /// </summary>
        /// <param name="connection"><see cref="T:System.Data.SqlClient.SqlConnection"></see> that represents the connection to an instance of SQL Server.</param>
        /// <param name="commandText">The text of the command to be executed</param>
        /// <param name="parameters">The SqlParameters for the command</param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout">The number of seconds for the command's timeout. Defaults to 120</param>
        /// <returns></returns>
        public static async Task<SqlCommand> ToSqlCommand(this Task<SqlConnection> connection, string commandText, SqlParameter[] parameters, CommandType commandType, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), nameof(connection));
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), nameof(commandText));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), nameof(parameters));
                        
            var command = new SqlCommand
            {
                CommandTimeout = commandTimeout,
                Connection = await connection,
                CommandType = commandType,
                CommandText = commandText
            };

            if (parameters.Any())
                command.Parameters.AddRange(parameters);

            return command;
        }

        /// <summary>
        /// Returns a new SqlCommand
        /// </summary>
        /// <param name="connection"><see cref="T:System.Data.SqlClient.SqlConnection"></see> that represents the connection to an instance of SQL Server.</param>
        /// <param name="commandText">The text of the command to be executed</param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout">The number of seconds for the command's timeout. Defaults to 120</param>
        /// <returns></returns>
        public static async Task<SqlCommand> ToSqlCommand(this Task<SqlConnection> connection, string commandText, CommandType commandType, int commandTimeout = 120)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection), "connection");
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(nameof(commandText), "commandText");
                       
            var command = new SqlCommand
            {
                CommandTimeout = commandTimeout,
                Connection = await connection,
                CommandType = commandType,
                CommandText = commandText
            };

            return command;
        }

    }
}
