

namespace AlbumPipeline.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class StepNameAttribute : Attribute
{
    public string Name { get; }

    public StepNameAttribute(string name)
    {
        Name = name;
    }
}