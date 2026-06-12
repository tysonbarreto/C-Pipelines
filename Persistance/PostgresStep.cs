

using AlbumPipeline.Core;
using AlbumPipeline.Logging;
using AlbumPipeline.Models;

namespace AlbumPipeline.Persistance;

public class PostgresStep
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public PostgresStep(string connectionString, ILogger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }
    public async Task<Result<List<AlbumWithUser>>> ProcessAsync(List<AlbumWithUser> data)
    {
        var writer = new PostgreWriter<AlbumWithUser>(_connectionString);
        await writer.InsertAsync(data,_logger);
        return Result<List<AlbumWithUser>>.Success(data);
    }
}