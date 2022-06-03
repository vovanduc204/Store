using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericDapperRepository : IGenericDapperRepository
    {
        public T ExecuteScalarSP<T>(string storedProcedure, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalarSQL<T>(string sql, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSP(string storedProcedure, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSQL(string sql, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSQLAsync(string sql, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<T>> QueryMultipleSP<T>(string storedProcedure, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<T>> QueryMultipleSQL<T>(string sql, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QuerySP<T>(string storedProcedure, object param = null, SqlTransaction transaction = null, bool buffered = true)
        {
            throw new NotImplementedException();
        }

        public T QuerySPSingleOrDefault<T>(string storedProcedure, object param = null, SqlTransaction transaction = null, bool buffered = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QuerySQL<T>(string sql, object param = null, SqlTransaction transaction = null, bool buffered = true)
        {
            throw new NotImplementedException();
        }

        public T QuerySQLSingleOrDefault<T>(string sql, object param = null, SqlTransaction transaction = null, bool buffered = true)
        {
            throw new NotImplementedException();
        }

        public Task<T> QuerySQLSingleOrDefaultAsync<T>(string sql, object param = null, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
