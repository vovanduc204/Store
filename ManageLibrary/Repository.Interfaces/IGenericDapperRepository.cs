using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGenericDapperRepository
    {
        IEnumerable<T> QuerySP<T>(string storedProcedure, object param = null, SqlTransaction transaction = null, bool buffered = true);
        IEnumerable<T> QuerySQL<T>(string sql, object param = null, SqlTransaction transaction = null, bool buffered = true);
        T QuerySPSingleOrDefault<T>(string storedProcedure, object param = null, SqlTransaction transaction = null, bool buffered = true);
        T QuerySQLSingleOrDefault<T>(string sql, object param = null, SqlTransaction transaction = null, bool buffered = true);
        Task<T> QuerySQLSingleOrDefaultAsync<T>(string sql, object param = null, SqlTransaction transaction = null);
        IEnumerable<IEnumerable<T>> QueryMultipleSP<T>(string storedProcedure, object param = null, SqlTransaction transaction = null);
        IEnumerable<IEnumerable<T>> QueryMultipleSQL<T>(string sql, object param = null, SqlTransaction transaction = null);
        T ExecuteScalarSP<T>(string storedProcedure, object param = null, SqlTransaction transaction = null);
        T ExecuteScalarSQL<T>(string sql, object param = null, SqlTransaction transaction = null);
        int ExecuteSP(string storedProcedure, object param = null, SqlTransaction transaction = null);
        int ExecuteSQL(string sql, object param = null, SqlTransaction transaction = null);
        Task<int> ExecuteSQLAsync(string sql, object param = null, SqlTransaction transaction = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, SqlTransaction transaction = null);
    }
}
