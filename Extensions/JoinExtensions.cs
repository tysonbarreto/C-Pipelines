

namespace AlbumPipeline.Extensions;

public static class JoinExtensions
{
    public static IEnumerable<TResult> InnerJoinExt<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> left,
        IEnumerable<TInner> right,
        Func<TOuter, TKey> leftKeySelector,
        Func<TInner, TKey> rightKeySelector,
        Func<TOuter, TInner, TResult> resultSelector
        )
    {
        return left.Join(
            right,
            leftKeySelector,
            rightKeySelector,
            resultSelector
        );
    }

    public static IEnumerable<TResult> LeftJoinExt<TLeft, TRight, TKey, TResult>(
        this IEnumerable<TLeft> left,
        IEnumerable<TRight> right,
        Func<TLeft, TKey> leftKeySelector,
        Func<TRight, TKey> rightKeySelector,
        Func<TLeft, TRight?, TResult> resultSelector 
    )
    {
        return left
            .GroupJoin(
                right,
                leftKeySelector,
                rightKeySelector,
                (l,rGroup)=> new {l, rGroup}
            )
            .SelectMany(
                x=> x.rGroup.DefaultIfEmpty(),
                (x,r)=> resultSelector(x.l,r)
            );
    }
}