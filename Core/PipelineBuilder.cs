using AlbumPipeline.Logging;


namespace AlbumPipeline.Core;

public class PipelineBuilder<TInput, TOutput>
{
    private readonly Func<TInput, Task<Result<TOutput>>> _pipeline;
    public PipelineBuilder(Func<TInput, Task<Result<TOutput>>> pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task<Result<TOutput>> ExecuteAsync(TInput input)
    {
        return await _pipeline(input);
    }

    public PipelineBuilder<TInput, TNext> Then<TNext>(Func<TOutput, Task<Result<TNext>>> nextStep, string stepName, ILogger logger = null)
    {
        return new PipelineBuilder<TInput, TNext>(async input =>
        {
            var currentResult = await _pipeline(input);

            if (!currentResult.IsSuccess) return Result<TNext>.Failure(currentResult.Error!);
            try
            {
                await logger.LogAsync($"Running: {stepName}");
                var nextResult = await nextStep(currentResult.Value!);
                if(!nextResult.IsSuccess) await logger.LogAsync($"Step failed: {stepName} - {nextResult.Error}");
                return nextResult;
            }
            catch (System.Exception ex)
            {
                await logger.LogAsync($"FAILED (ignored): {stepName} - {ex.Message}");
                return Result<TNext>.Failure(ex.Message);
            }
            
        });
    }
}