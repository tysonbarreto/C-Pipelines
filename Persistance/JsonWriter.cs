

using System.Text.Json;
using AlbumPipeline.Logging;

namespace AlbumPipeline.Persistance;

public class JsonWriter<T> : IDisposable
{
    private StreamWriter? _writer;
    private readonly string _filePath;
    private bool _disposed = false;

    public JsonWriter(string filePath)
    {
        _filePath = filePath;
        _writer = new StreamWriter(_filePath, append: false);
    }

    public async Task WriteAsync(T data, ILogger? logger = null)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(JsonWriter<T>));
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(data, options: options);
            await _writer!.WriteAsync(json);
            await _writer.FlushAsync();

            if (logger is not null) await logger.LogAsync($"JSON written to {_filePath}");
        }
        catch (System.Exception ex)
        {
            if (logger is not null) await logger.LogAsync($"JSON write failed: {ex.Message}");
            throw;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _writer?.Dispose();
        }
        _disposed = true;
    }
}