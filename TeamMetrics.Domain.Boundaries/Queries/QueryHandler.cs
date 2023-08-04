namespace TeamMetrics.Domain.Boundaries.Queries;

public interface QueryHandler<in TQuery, TResult> : Handler<TQuery, TResult> where TQuery : Query<TResult>
{
}