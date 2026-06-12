using AlbumPipeline.Core;
using AlbumPipeline.Logging;
using AlbumPipeline.Models;

namespace AlbumPipeline.Persistance;

public class JsonExportStep
{
    private ILogger _logger;
    public JsonExportStep(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<Result<List<AlbumWithUser>>> ProcessAsync(List<AlbumWithUser> data)
    {
        using var writer = new JsonWriter<List<AlbumWithUser>>("albums.json");
        await writer.WriteAsync(data);
        return Result<List<AlbumWithUser>>.Success(data);
    }
}