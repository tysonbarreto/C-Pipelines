


using AlbumPipeline.Core;
using AlbumPipeline.Extensions;
using AlbumPipeline.Logging;
using AlbumPipeline.Models;

namespace AlbumPipeline.Transformations.Steps;

public class JoinStep
{
    private readonly ILogger _logger;
    private readonly List<User> _users;
    public JoinStep(List<User> users, ILogger logger)
    {
        _logger = logger;
        _users = users;
    }

    public async Task<Result<List<AlbumWithUser>>> ProcessAsync(List<Album> albums)
    {
        await _logger.LogAsync("Running Join Step...");

        var result = albums
                        .InnerJoinExt(
                            _users,
                            album => album.UserId,
                            user => user.Id,
                            (album, user)=> new AlbumWithUser
                            {
                                AlbumId = album.Id,
                                Title = album.Title,
                                UserId = user.Id,
                                UserName = user.Name,
                                Email = user.Email
                            }
                        ).ToList();
        await Task.CompletedTask;
        return Result<List<AlbumWithUser>>.Success(result);
    }
}