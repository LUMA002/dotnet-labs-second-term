using Microsoft.Data.SqlClient;

namespace Labs.Data.Repositories.Ado;

/// <summary>
/// Base class for ADO.NET repositories.
/// Contains Connection String and methods for working with SqlConnection.
/// </summary>
public abstract class AdoBaseRepository
{
    protected readonly string _connectionString;

    protected AdoBaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Create SqlConnection (must be closed after execution).
    /// </summary>
    protected SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    protected async Task<int> ExecuteNonQueryAsync(string sql, params SqlParameter[] parameters)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        
        if (parameters != null && parameters.Length > 0)
            command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }

    //protected async Task<object?> ExecuteSacalarAsync(string sql, params SqlParameter[] parameters)
    //{
    //    using var connection = CreateConnection();
    //    using var command = new SqlCommand(sql, connection);
        
    //    if (parameters != null && parameters.Length > 0)
    //        command.Parameters.AddRange(parameters);

    //    await connection.OpenAsync();
    //    return await command.ExecuteScalarAsync();
    //}
}