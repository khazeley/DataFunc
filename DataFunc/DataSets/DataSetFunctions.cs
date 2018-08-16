using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataFunc.DataAdapters;

namespace DataFunc.DataSets
{
    public static class DataSetFunctions
    {
        public static DataSet ToDataSet(this DbDataAdapter dbDataAdapter)
        {
            var dataSet = new DataSet();

            dbDataAdapter.Fill(dataSet);
            return dataSet;
        }

        public static DataSet ToDataSetFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName, IEnumerable<DbParameter> parameters)
            where T : DbDataAdapter, new()
            => connection.ToDbDataAdapterFromStoredProcedure<T>(storedProcedureName, parameters).ToDataSet();

        public static DataSet ToDataSetFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName)
            where T : DbDataAdapter, new()
            => connection.ToDbDataAdapterFromStoredProcedure<T>(storedProcedureName).ToDataSet();

        public static Task<DataSet> ToDataSetAsync(this DbDataAdapter dbDataAdapter) => new Task<DataSet>(dbDataAdapter.ToDataSet);
    }
}
