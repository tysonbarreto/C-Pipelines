using Npgsql;
using AlbumPipeline.Attributes;
using AlbumPipeline.Logging;
using System.Reflection;

namespace AlbumPipeline.Persistance;

public class PostgreWriter<T> : IDisposable
{
    private readonly NpgsqlConnection _connection;
    private bool _disposed;

    public PostgreWriter(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task InsertAsync(IEnumerable<T> data, ILogger logger)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(PostgreWriter<T>));
        try
        {
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableNameAttribute>();
            var tableName = tableAttr?.Name ?? type.Name;

            var properties = typeof(T).GetProperties();
            var columnName = properties
                                .Select(p =>
                                {
                                    var attr = p.GetCustomAttribute<ColumnNameAttribute>();
                                    return attr?.Name ?? p.Name;
                                })
                                .ToList();
            var columns = string.Join(",", columnName);
            foreach (var item in data)
            {
                var values = properties.Select((p, i) =>
                {
                    var value = p.GetValue(item);
                    return $"@p{i}";
                })
                .ToList();

                var commandText = $"INSERT INTO {tableName} ({columns}) VALUES ({string.Join(",", values)})";
                using var cmd = new NpgsqlCommand(commandText, connection: _connection);
                for (int i = 0; i < properties.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"p{1}, {properties[i].GetValue(item)}");
                }
                await cmd.ExecuteNonQueryAsync();
            }
            await logger.LogAsync($"Inserted {data.Count()} records into {tableName}");
        }
        catch (System.Exception ex)
        {

            await logger.LogAsync($"Postgres insert failed: {ex.Message}");
            throw;
        }

    }
    public void Dispose()
    {
        if (_disposed) return;
        _connection.Dispose();
        _disposed = true;
    }
}