using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DataFunc.DataAdapters;
using DataFunc.DataSets;

namespace DataFunc.DataTables
{
    public static class DataTableFunctions
    {
        public static DataTable ToDataTable(this DbDataAdapter dbDataAdapter) => dbDataAdapter.ToDataSet().Tables[0];

        public static DataTable ToDataTableFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName, IEnumerable<DbParameter> parameters) where T : DbDataAdapter, new()
            => connection.ToDbDataAdapterFromStoredProcedure<T>(storedProcedureName, parameters).ToDataTable();

        public static DataTable ToDataTableFromStoredProcedure<T>(this DbConnection connection, string storedProcedureName) where T : DbDataAdapter, new()
            => connection.ToDbDataAdapterFromStoredProcedure<T>(storedProcedureName).ToDataTable();

        public static Task<DataTable> ToDataTableAsync(this DbDataAdapter dbDataAdapter) => new Task<DataTable>(dbDataAdapter.ToDataTable);
    }
}
