

using AlbumPipeline.Attributes;

namespace AlbumPipeline.Models;

[TableName("album_with_user")]
public class AlbumWithUser
{
    [CsvColumn("album_id")]
    public int AlbumId { get; set; }

    [CsvColumn("title")]
    public string? Title { get; set; }

    [CsvColumn("user_id")]
    public int UserId { get; set; }

    [CsvColumn("user_name")]
    public string? UserName { get; set; }

    [CsvColumn("email")]
    public string? Email { get; set; }
}
