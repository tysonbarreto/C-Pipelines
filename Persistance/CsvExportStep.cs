using AlbumPipeline.Core;
using AlbumPipeline.Logging;
using AlbumPipeline.Models;

namespace AlbumPipeline.Persistance;

public class CsvExportStep
{
    private readonly CsvWriter<AlbumWithUser> _writer;
    private readonly ILogger _logger;

    public CsvExportStep(CsvWriter<AlbumWithUser> writer, ILogger logger)
    {
        _writer = writer;
        _logger = logger;
    }

    public async Task<Result<List<AlbumWithUser>>> ProcessAsync(List<AlbumWithUser> data)
    {
        await _writer.WriteAsync(data, "albums.csv", _logger);
        return Result<List<AlbumWithUser>>.Success(data);
    }
}