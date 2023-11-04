using AddressConsultation.Persistence.SqlServer.Infra;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace AddressConsultation.Persistence.SqlServer.Context
{
    public class DapperDbContext : IDapperDbContext, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task ExecuteAsync(string sql, object parameters = null)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, parameters);
        }

        public IDbConnection GetConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
