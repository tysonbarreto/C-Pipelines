

using System.Reflection;
using AlbumPipeline.Attributes;
using AlbumPipeline.Models;

namespace AlbumPipeline.Utils;

public static class CsvColumnAttributeHelper
{
    public static List<string> GetHeaders<T>()
    {
        return typeof(T)
                    .GetProperties()
                    .Select(p =>
                    {
                        var attr = p.GetCustomAttribute<CsvColumnAttribute>();
                        return attr?.Name ?? p.Name;
                    })
                    .ToList();
    }

    public static List<string> GetValues<T>(T obj)
    {
        return typeof(T)
                    .GetProperties()
                    .Select(p =>
                    {
                        var value = p.GetValue(obj);
                        return value?.ToString() ?? string.Empty;
                    })
                    .ToList();
    }
}