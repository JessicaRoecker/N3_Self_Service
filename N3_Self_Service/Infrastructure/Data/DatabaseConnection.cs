using Dapper;
using MySql.Data.MySqlClient;


namespace N3_Self_Service.Infrastructure.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters)
        {
            string connectionString = _configuration.GetConnectionString("MysqlConnection");

            using (var con = new MySqlConnection(connectionString))
            {
                return await con.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters)
        {
            string connectionString = _configuration.GetConnectionString("MysqlConnection");

            using (var con = new MySqlConnection(connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<T>(query, parameters);
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters)
        {
            string connectionString = _configuration.GetConnectionString("MysqlConnection");

            using (var con = new MySqlConnection(connectionString))
            {
                return await con.ExecuteAsync(query, parameters);
            }
        }
    }
}
