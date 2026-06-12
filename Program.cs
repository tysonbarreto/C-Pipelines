



using System.Text.Json;
using AlbumPipeline.ApiService;
using AlbumPipeline.Logging;
using AlbumPipeline.Models;
using AlbumPipeline.Utils;
using AlbumPipeline.Transformations;
using AlbumPipeline.Extensions;
using AlbumPipeline.Core;
using AlbumPipeline.Transformations.Steps;
using AlbumPipeline.Persistance;

var logger = new FileLogger();
ApiService apiService = new();

try
{
    await logger.LogAsync("Calling Api...");

    string albumJsonString = await apiService.GetAlbumRawAsync();
    string userJsonString = await apiService.GetUserRawAsync();

    await logger.LogAsync("Data fetched");


    var albums = Deserializer.Deserialize<Album>(albumJsonString);

    if (albums == null || albums.Count == 0)
    {
        await logger.LogAsync("No data found");
        return;
    }

    await logger.LogAsync($"Total Albums: {albums.Count}");

    var users = Deserializer.Deserialize<User>(userJsonString);
    await logger.LogAsync($"Total Users: {users.Count}");

    var groupedAlbum = GroupingExamples.GroupByUserWithCount<Album, UserAlbumSummary>(albums);
    var flattenedGroup = GroupingExamples.GroupAndFlattened<Album, AlbumFlattened>(albums);
    var multiLevelGroup = GroupingExamples.MultiLevelGrouping(albums);
    var userStats = GroupingExamples.GroupIntoStatsDictionary<Album, UserAlbumStats>(
        albums,
        group => new UserAlbumStats
        {
            UserId = group.Key,
            TotalAlbums = group.Count(),
            ShortTitleCount = group.Count(a => a.Title is not null && a.Title.Length < 30),
            LongTitleCount = group.Count(a => a.Title is not null && a.Title.Length >= 30)
        }
    );
    //GroupingExamples.GroupByUser<Album>(albums);

    // foreach (Album album in albums.Take(3))
    // {
    //     logger.Log($"Album -> Id: {album.Id}, User: {album.UserId}, Title: {album.Title}");
    // }

    await logger.LogAsync("\n===Joins===\n");
    var innerJoin = albums.Join(
        users,
        album => album.UserId,
        user => user.Id,
        (album, user) => new
        {
            AlbumId = album.Id,
            Title = album.Title,
            UserName = user.Name
        }
    );

    var step1 = albums.GroupJoin(
        users,
        album => album.UserId,
        user => user.Id,
        (album, userGroup) => new
        {
            album,
            users = userGroup.DefaultIfEmpty()
        }
    )
    .SelectMany(x => x.users,
    (x, user) => new
    {
        AlbumId = x.album.Id,
        Title = x.album.Title,
        UserName = user?.Name ?? "NO USER"
    });


    var groupJoin = users.GroupJoin(
        albums,
        user => user.Id,
        album => album.UserId,
        (user, albumGroup) => new
        {
            UserName = user.Name,
            Albums = albumGroup
        }
    );

    var innerJoinExt = albums.InnerJoinExt(
        users,
        album => album.UserId,
        user => user.Id,
        (album, user) => new
        {
            AlbumId = album.Id,
            Title = album.Title,
            UserName = user.Name
        }
    );

    var leftJoinExt = albums.LeftJoinExt(
        users,
        album => album.UserId,
        user => user.Id,
        (album, userGroup) => new
        {
            AlbumId = album.Id,
            Title = album.Title,
            UserName = userGroup?.Name ?? "NO USER"
        }
    );

    // var pipeline = new Pipeline<List<Album>>();
    var groupingStep = new GroupingStep(logger);
    // pipeline.AddStep(groupingStep.ProcessAsync);
    // var groupedAlbums = await pipeline.ExecuteAsync(albums);

    var joinStep = new JoinStep(users, logger);
    // var pipeline2 = new Pipeline<List<Album>>();
    // pipeline2.AddStep(
    //     async input =>
    //     {
    //         return await joinStep.ProcessAsync(input)
    //     }
    //     );

    // logger.Log($"Pipeline processed {processedAlbums.Count} records");
    var csv = new CsvWriter<AlbumWithUser>();
    var csvExport = new CsvExportStep(writer: csv, logger);

    var jsonExport = new JsonExportStep(logger);


    var result = await Pipeline
                        .Start<List<Album>>()
                        .Then(groupingStep.ProcessAsync, "Grouping set", logger)
                        .Then(joinStep.ProcessAsync, "Join step", logger)
                        .Then(csvExport.ProcessAsync, "Csv save step", logger)
                        .Then(jsonExport.ProcessAsync, "Json save step", logger)
                        .ExecuteAsync(albums);

    //System.Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
    
}
catch (Exception ex)
{
    await logger.LogAsync($"ERROR: {ex.Message}");
}