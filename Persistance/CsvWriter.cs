
using System.Reflection;
using System.Text;
using AlbumPipeline.Attributes;
using AlbumPipeline.Logging;

namespace AlbumPipeline.Persistance;

public class CsvWriter<T>
{
    public async Task WriteAsync(IEnumerable<T> data, string filePath, ILogger? logger)
    {
        try
        {

            var stringBuilder = new StringBuilder();

            var properties = typeof(T).GetProperties();


            var headers = typeof(T)
                            .GetProperties()
                            .Select(p =>
                            {
                                var attr = p.GetCustomAttribute<CsvColumnAttribute>();
                                return attr?.Name ?? p.Name;
                            })
                            .ToList();
            stringBuilder.AppendLine(string.Join(",", headers));
            foreach (var line in data)
            {
                var value = properties
                                .Select(
                                    p =>
                                    {
                                        var value = p.GetValue(line)?.ToString() ?? string.Empty;
                                        if (value.Contains(",")) value = $"\"{value}\"";
                                        return value;
                                    }
                                );
                stringBuilder.AppendLine(string.Join(",",value));
            }
            await File.WriteAllTextAsync(filePath,stringBuilder.ToString());
            if(logger is not null)
            {
                await logger.LogAsync($"CSV file is written to {filePath}");
            }
        }catch(System.Exception ex)
        {
            if(logger is not null)
            {
                await logger.LogAsync($"CSV write failed: {ex.Message}");
                throw;
            }
        }
    }
}