using ETL.Models;
using ETL.Csv;
using ETL.Json;
using ETL.Extensions;

class Program
{
    static void Main()
    {
        string inputPath = "input/customers.csv";
        string outputPath = "output/customers.json";

        Directory.CreateDirectory("input");
        Directory.CreateDirectory("output");

        if (!File.Exists(inputPath))
        {
            File.WriteAllLines(inputPath, new []
            {
                "1,John Smith,john.smith@example.com,34",
                "2,Sarah Johnson,sarah.johnson@example.com,28",
                "3,Michael Brown,michael.brown@example.com,45",
                "4,Emily Davis,emily.davis@example.com,31",
                "5,Daniel Wilson,daniel.wilson@example.com,52",
            });
            Logger.Info("Sample CSV created.");
        }

        var extractor = new CustomerCsvExtractor(inputPath);
        var transformer = new CustomerTransformer();
        var loader = new CustomerJsonLoader(outputPath);

        try
        {
            var extracted = extractor.Extract().LogCount("Extracted");
            var transformed = transformer.Transform(extracted).LogCount("Transformed");
            loader.Load(transformed);

            Logger.Info("ETL pipeline complete.");
        }
        catch (System.Exception ex)
        {
            
            Logger.Error($"Unhandled error in ETL pipeline: {ex.Message}");
        }

        System.Console.WriteLine("\nPress any key to exit");
        Console.ReadKey();
    }
}