namespace AlbumPipeline.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CsvColumnAttribute : Attribute
{
    public string Name { get; }
    public CsvColumnAttribute(string name)
    {
        Name = name;
    }
}