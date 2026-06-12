

using AlbumPipeline.Models;

namespace AlbumPipeline.Transformations;

public static class GroupingExamples
{
    public static Dictionary<int, List<T>> GroupByUser<T>(List<T> jsonList)
    where T : Album
    {
        Dictionary<int, List<T>> results = jsonList
                                            .GroupBy(a => a.UserId)
                                            .ToDictionary(g => g.Key, g => g.ToList())
                                            ;
        return results;
    }

    public static List<U> GroupByUserWithCount<T, U>(List<T> jsonList)
    where T : Album
    where U : UserAlbumSummary, new()
    {
        List<U> results = jsonList
                    .GroupBy(a => a.UserId)
                    .Select(g => new U { UserId = g.Key, AlbumCount = g.Count() })
                    .OrderByDescending(x => x.AlbumCount)
                    .ToList();
        return results;
    }

    public static List<U> GroupAndFlattened<T, U>(List<T> jsonList)
    where T : Album
    where U : AlbumFlattened, new()
    {
        List<U> results = jsonList
                            .GroupBy(a => a.UserId)
                            .SelectMany(g => g.Select(album => new U
                            {
                                UserId = album.UserId,
                                AlbumId = album.Id,
                                Title = album.Title
                            }))
                            .ToList();
        return results;
    }

    public static Dictionary<int, Dictionary<string, List<T>>>? MultiLevelGrouping<T>(List<T> jsonList)
    where T : Album
    {
        Dictionary<int, Dictionary<string, List<T>>>? results = jsonList
                        .GroupBy(a => a.UserId)
                        .ToDictionary(
                            g => g.Key,
                            g => g.GroupBy(album => album.Title is not null && album.Title.Length < 30 ? "Short" : "Long")
                                    .ToDictionary(tg => tg.Key, tg => tg.ToList())
                            );
        return results;
    }

    public static Dictionary<int, U> GroupIntoStatsDictionary<T, U>(
        List<T> jsonList,
        Func<IGrouping<int, T>, U> statsCalc
    )
    where T : Album
    where U : UserAlbumStats
    {
        var results = jsonList
                        .GroupBy(a => a.UserId)
                        .ToDictionary(g => g.Key, g => statsCalc(g));
        return results;
    }
}