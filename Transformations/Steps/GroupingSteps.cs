


using System.Text.RegularExpressions;
using AlbumPipeline.Core;
using AlbumPipeline.Logging;
using AlbumPipeline.Models;

namespace AlbumPipeline.Transformations.Steps;

public class GroupingStep
{
    private readonly ILogger _logger;

    public GroupingStep(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<Result<List<Album>>> ProcessAsync(List<Album> input)
    {
        await _logger.LogAsync("Running Grouping Set...");

        var grouped = input
                        .GroupBy(a => a.UserId)
                        .SelectMany(g => g)
                        .ToList();
        await Task.CompletedTask;
        return Result<List<Album>>.Success(grouped);
    }

}