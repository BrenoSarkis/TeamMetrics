using System.Collections.Immutable;
using TeamMetrics.Common.Domain;

namespace TeamMetrics.Common.Specifications;

public interface Repository<T> where T : Aggregate {
    Task Add(T entidade);
    Task Remove(T entidade);
    Task<ImmutableList<T>> All();
    Task<T> Get(Guid identificador);
    Task<ImmutableList<T>> Filter(params Specification<T>[] filter);
    Task<T> Find(params Specification<T>[] filter);
    Task<Boolean> Exists(params Specification<T>[] filter);
    Task<long> CountRecords(params Specification<T>[] filter);
}