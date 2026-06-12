namespace AlbumPipeline.Models;

public class UserAlbumStats
{
    public int UserId { get; set; }
    public int TotalAlbums { get; set; }
    public int ShortTitleCount { get; set; }
    public int LongTitleCount { get; set; }
}