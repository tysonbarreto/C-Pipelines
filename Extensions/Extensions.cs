
using ETL.Models;

namespace ETL.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> LogCount<T>(this IEnumerable<T> source, string label)
    {
        var list = source.ToList();
        Logger.Info($"{label}: {list.Count} item(s).");
        return list;
    }
}