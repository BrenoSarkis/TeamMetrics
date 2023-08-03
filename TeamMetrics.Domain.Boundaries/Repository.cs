using System.Collections.Immutable;

namespace TeamMetrics.Domain.Boundaries;

public interface Repository<T> where T : Aggregate {
    Task Add(T entidade);
    Task Remove(T entidade);
    Task<ImmutableList<T>> All();
    Task<T> Get(Guid identificador);
    Task<ImmutableList<T>> Filter(params Specification<T>[] filter);
    Task<T> Find(params Specification<T>[] filter);
    Task<Boolean> Exists(params Specification<T>[] filter);
    Task<long> Count(params Specification<T>[] filter);
}