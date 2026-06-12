namespace AlbumPipeline.Logging;

public interface ILogger
{
    Task LogAsync(string message);
}