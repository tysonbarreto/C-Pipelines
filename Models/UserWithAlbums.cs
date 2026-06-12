
namespace AlbumPipeline.Models;

public class UserWithAlbums
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public List<Album> Albums { get; set; } = [];
}