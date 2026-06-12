

namespace AlbumPipeline.Logging;

public class Logger
{
    public void Log(string message)
    {
        System.Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}