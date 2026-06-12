using System.Diagnostics;

namespace AlbumPipeline.Core;



public interface IPipelineSetup<TInput, TOuput>
{
    Task<TOuput> ProcessAsync(TInput input);
}