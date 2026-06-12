

namespace AlbumPipeline.Core;

public class PipelineException : Exception
{
    public string StepName { get; }
    public PipelineException(string stepName, Exception innerException) : base($"Error in pipeline in step: {stepName}", innerException)
    {
        StepName = stepName;
    }
}