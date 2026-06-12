

namespace AlbumPipeline.Core;

// public class Pipeline<T>
// {
//     private readonly List<Func<T, Task<T>>> _steps = [];

//     public Pipeline<T> AddStep(Func<T, Task<T>> step)
//     {
//         _steps.Add(step);
//         return this;
//     }
//     public async Task<T> ExecuteAsync(T input)
//     {
//         var result = input;
//         foreach(var step in _steps)
//         {
//             result = await step(input);
//         }
//         return result;
//     }
// }

public static class Pipeline
{
    public static PipelineBuilder<TInput, TInput> Start<TInput>()
    {
        return new Core.PipelineBuilder<TInput, TInput>(input => Task.FromResult(Result<TInput>.Success(input)));
    }
}