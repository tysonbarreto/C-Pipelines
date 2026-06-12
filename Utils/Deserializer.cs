


using System.Text.Json;
using AlbumPipeline.Models;

namespace AlbumPipeline.Utils;

public static class Deserializer
{
    public static List<T>? Deserialize<T>(string jsonString)
    {
        return JsonSerializer.Deserialize<List<T>>(
            jsonString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }
}