namespace TeamMetrics.Domain.Boundaries;

public interface QueryHandler<in TQuery, TResult> : Handler<TQuery, TResult> where TQuery : Query<TResult> {
}