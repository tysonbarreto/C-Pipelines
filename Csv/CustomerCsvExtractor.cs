

using ETL.Models;

namespace ETL.Csv;

public class CustomerCsvExtractor(string filepath) : IExtractor<Customer>
{
    private readonly string _filePath = filepath;

    public IEnumerable<Customer> Extract()
    {
        if (File.Exists(_filePath))
        {
            Logger.Info($"Reading CSV from '{_filePath}'");
            System.IO.StreamReader reader = new (_filePath);
            string? line;
            int lineNumber = 0;

            while ((line = reader.ReadLine()) is not null)
            {
                lineNumber ++;
                if(string.IsNullOrWhiteSpace(line)) continue;
                string [] parts = line.Split(',');

                Customer customer;
                try
                {
                    if(parts.Length != 4) throw new FormatException("Expected 4 columns per line");
                    customer = new()
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1].Trim(),
                        Email = parts[2].Trim(),
                        Age = int.Parse(parts[3]),
                    };
                }
                catch (System.Exception ex)
                {
                    Logger.Error($"Error parsing line {lineNumber}: {ex.Message}");
                    throw;
                }

                yield return customer;
            }
        }
        else
        {
            Logger.Error($"Error extracting CSV file '{_filePath}'");
            throw new FileLoadException($"Error extracting CSV file '{_filePath}'");
        }

    }
}