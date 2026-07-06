

using System.Text.Json;
using CustomerSync.Application.Configuration;
using CustomerSync.Application.Models;
using Microsoft.Extensions.Options;

namespace CustomerSync.Application.Services;

public sealed class CustomerReportWriter : IDisposable
{
    public readonly ReportSettings _settings;
    public bool _disposed;

    public CustomerReportWriter(IOptions<ReportSettings> options)
    {
        _settings = options.Value;
        System.Console.WriteLine($"Ouput folder has directory {_settings}");
    }

    public async Task WriteLineAsync(string text, string filePath)
    {
        EnsureNotDisposed();
        await using var writer = new StreamWriter(filePath);
        await writer.WriteLineAsync(text);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
        _disposed = true;
    }

    private void EnsureNotDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public static void CreateDirectoryIfRequired(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory)) Directory.CreateDirectory(directory);
    }

    public async Task WritJsonAsync(IEnumerable<CustomerRegionSummary> summaries, string filePath)
    {
        EnsureNotDisposed();

        await using var stream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(stream, summaries, new JsonSerializerOptions { WriteIndented = true });

    }

    public async Task WriteCsvAsync(IEnumerable<CustomerRegionSummary> summaries, string filePath)
    {
        EnsureNotDisposed();

        await using var writer = new StreamWriter(filePath);

        await writer.WriteLineAsync("Region,CustomCount");

        foreach (var summary in summaries)
            await writer.WriteLineAsync(
                $"{summary.Region},{summary.CustomerCount}");
    }
}