using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DataFunc.Connections
{
    public static class ConnectionFunctions
    {
        public static T ToNewDbConnection<T>(this string connectionString) where T : DbConnection, new() => new T {ConnectionString = connectionString};

        public static Func<string, T> ConnectionFactoryFunction<T>(this string con) where T : DbConnection, new() => ToNewDbConnection<T>;
        public static T ToDbConnection<T>(this string connectionString, Func<string, T> factoryFunction)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return factoryFunction(connectionString);
        }

        public static T ToOpenDbConnection<T>(this string connectionString, Func<string, T> factoryFunction)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var connection =  factoryFunction(connectionString);

            connection.Open();

            return connection;
        }

        public static T ToDbConnection<T>(this string connectionString)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return connectionString.ToNewDbConnection<T>();

        }

        public static T ToOpenDbConnection<T>(this string connectionString)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var connection = connectionString.ToNewDbConnection<T>();

            connection.Open();

            return connection;
        }

        public static async Task<T> ToOpenDbConnectionAsync<T>(this string connectionString, Func<string, T> factoryFunction)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var connection = factoryFunction(connectionString);

            await connection.OpenAsync();

            return connection;
        }

        public static async Task<T> ToOpenDbConnectionAsync<T>(this string connectionString)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            
            var connection = connectionString.ToNewDbConnection<T>();

            await connection.OpenAsync();

            return connection;
        }

        public static async Task<T> ToOpenDbConnectionAsync<T>(this string connectionString, CancellationToken token)
            where T : DbConnection, new()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if(token.IsCancellationRequested)
                token.ThrowIfCancellationRequested();

            var connection = connectionString.ToNewDbConnection<T>();
            
            await connection.OpenAsync(token);

            return connection;
        }

        /// <summary>
        /// Returns a new SqlConnection
        /// </summary>
        /// <param name="connectionString">The connectionString of the target database</param>
        /// <returns></returns>
        public static SqlConnection ToSqlConnection(this string connectionString)
        {
            if(string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Returns a new SqlConnection in an open state
        /// </summary>
        /// <param name="connectionString">The connectionString of the target database</param>
        /// <returns></returns>
        public static SqlConnection ToOpenSqlConnection(this string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            
            var connection = connectionString.ToSqlConnection();
            connection?.Open();
            
            return connection;
        }

        /// <summary>
        /// Returns a Task that returns a new SqlConnection in an open state
        /// </summary>
        /// <param name="connectionString">The connectionString of the target database</param>
        /// <returns></returns>
        public static async Task<SqlConnection> ToOpenSqlConnectionAsync(this string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var connection = connectionString.ToSqlConnection();
            await connection?.OpenAsync();

            return connection;
        }
    }
}
