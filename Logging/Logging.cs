namespace ETL.Models;

public static class Logger
{
    public static void Info(string message)=>Write("INFO", message);
    public static void Warn(string message)=>Write("WARN", message);
    public static void Error(string message)=>Write("ERROR", message);

    public static void Write(string level, string message)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        System.Console.WriteLine($"[{timestamp}] [{level}] {message}");
    }
}