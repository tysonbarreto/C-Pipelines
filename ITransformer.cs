namespace ETL;

public interface ITransformer<TIn, TOut>
{
    IEnumerable<TOut> Transform(IEnumerable<TIn> input);
}