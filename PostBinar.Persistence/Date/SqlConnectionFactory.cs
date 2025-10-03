using System.Data;
using Npgsql;
using PostBinar.Application.Abstractions.Interfaces;

namespace PostBinar.Persistence.Date
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(this._connectionString);
            connection.Open();

            return connection;
        }
    }
}
