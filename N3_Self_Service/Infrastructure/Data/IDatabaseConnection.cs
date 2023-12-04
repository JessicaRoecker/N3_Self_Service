namespace N3_Self_Service.Infrastructure.Data
{
    public interface IDatabaseConnection
    {
        Task<int> ExecuteAsync(string query, object parameters);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters);
    }
}