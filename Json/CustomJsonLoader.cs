using System.Text.Json;
using ETL.Models;

namespace ETL.Json;

public class CustomerJsonLoader(string filePath) : ILoader<Customer>
{
    private readonly string _filePath=filePath;
    public void Load(IEnumerable<Customer> items)
    {
        Logger.Info($"Writing JSON to '{_filePath}'");
        var options = new JsonSerializerOptions
        {
            WriteIndented=true,
        };
        string json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(_filePath, json);

        Logger.Info("JSON write complete");
    }
}