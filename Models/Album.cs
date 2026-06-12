

using AlbumPipeline.Attributes;

namespace AlbumPipeline.Models;

[TableName("albums")]
public class Album
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string? Title { get; set; }
}