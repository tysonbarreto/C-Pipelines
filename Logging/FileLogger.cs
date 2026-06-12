using System.Text;

namespace AlbumPipeline.Logging;

public class FileLogger : ILogger
{
    private readonly string _filePath = "app.log";
    public async Task LogAsync(string message)
    {
        var logLine = $"{DateTime.UtcNow:u} | {message}";
        System.Console.WriteLine(logLine);

        await File.AppendAllTextAsync(
            _filePath, logLine + Environment.NewLine, Encoding.UTF8
        );
    }
}