using System.Collections.Immutable;
using TeamMetrics.Common.Specifications;
using TeamMetrics.Common.Domain.Repositories;

namespace TeamMetrics.Common.Domain.Repositories;

public class InMemoryRepository<TAggregate> : Repository<TAggregate> where TAggregate : Aggregate {
    private readonly List<TAggregate> entities;

    public InMemoryRepository() {
        entities = new();
    }

    public Task Add(TAggregate entity) {
        entity.ThrowIfNull();

        entities.Add(entity);
        return Task.CompletedTask;
    }

    public Task Remove(TAggregate entity) {
        entity.ThrowIfNull();

        entities.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<ImmutableList<TAggregate>> All() {
        return Task.FromResult(entities.ToImmutableList());
    }

    public Task<TAggregate> Get(Guid identificador) {
        var entity = entities.Get(identificador);
        return Task.FromResult(entity);
    }

    public Task<ImmutableList<TAggregate>> Filter(params Specification<TAggregate>[] filter) {
        var dados = entities.Filter(filter);
        return Task.FromResult(dados.ToImmutableList());
    }

    public Task<TAggregate> Find(params Specification<TAggregate>[] filter) {
        var entity = entities.Find(filter);
        return Task.FromResult(entity);
    }

    public Task<Boolean> Exists(params Specification<TAggregate>[] filter) {
        var exists = entities.Exists(filter);
        return Task.FromResult(exists);
    }

    public Task<Int64> CountRecords(params Specification<TAggregate>[] filter) {
        var count = entities.CountRecords(filter);
        return Task.FromResult(count);
    }
}