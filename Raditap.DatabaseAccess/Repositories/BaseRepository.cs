using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Raditap.DatabaseAccess.EfRepositories;
using Raditap.DatabaseAccess.Entities;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Helpers;

namespace Raditap.DatabaseAccess.Repositories
{
    public class BaseRepository<T> : EfRepository<T>, IDisposable where T : class
    {
        private SqlTransaction _sqlTransaction;
        protected readonly DatabaseSettings DatabaseSettings;

        protected BaseRepository(RaditapContext writeContext, ReadRaditapContext readContext, DatabaseSettings databaseSettings, ProcessingTimeHelper timeHelper) :
                base(writeContext, readContext, timeHelper)
        {
            DatabaseSettings = databaseSettings;
        }

        public void Dispose()
        {
            //  Dispose something here
        }

        protected SqlConnection GetConnection()
        {
            var connection = _sqlTransaction != null ? new SqlConnection(DatabaseSettings.Connection) : new SqlConnection(DatabaseSettings.ReadOnlyConnection);
            connection.Open();

            return connection;
        }

        protected SqlConnection GetWriteConnection()
        {
            var connection = new SqlConnection(DatabaseSettings.Connection);
            connection.Open();

            return connection;
        }

        public async Task StartTransaction(SqlConnection connection)
        {
            if (_sqlTransaction != null) throw new Exception("Transaction can only start once");

            _sqlTransaction = await Task.Run(() => connection.BeginTransaction());
        }

        public async Task Commit()
        {
            await _sqlTransaction.CommitAsync();
            await _sqlTransaction.DisposeAsync();
            _sqlTransaction = null;
        }

        public async Task Rollback()
        {
            await _sqlTransaction.RollbackAsync();
            await _sqlTransaction.DisposeAsync();
            _sqlTransaction = null;
        }

        protected SqlCommand InitCommand(string spName, SqlConnection conn)
        {
            var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = _sqlTransaction,
                CommandTimeout = DatabaseSettings.TimeoutInSeconds,
            };

            return cmd;
        }
    }
}
