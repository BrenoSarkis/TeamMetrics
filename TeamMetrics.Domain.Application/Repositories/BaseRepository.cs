using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using TeamMetrics.Common;
using TeamMetrics.Common.Domain;
using TeamMetrics.Common.Domain.Repositories;
using TeamMetrics.Common.Specifications;

namespace TeamMetrics.Domain.Application.Repositories;

public abstract class BaseRepository<T> : Repository<T> where T : Aggregate {
    private readonly TeamMetricsDbContext dbContext;

    protected BaseRepository(TeamMetricsDbContext dbContext) {
        dbContext.ThrowIfNull();

        this.dbContext = dbContext;
    }

    public async Task Add(T entity) {
        entity.ThrowIfNull();

        await dbContext.AddAsync(entity);
    }

    public Task Remove(T entity) {
        entity.ThrowIfNull();

        return Task.FromResult(dbContext.Remove(entity));
    }

    public async Task<ImmutableList<T>> All() {
        var dados = await Query().ToArrayAsync();
        return dados.ToImmutableList();
    }

    protected virtual IQueryable<T> Query() {
        return dbContext.Set<T>();
    }

    public virtual async Task<T> Get(Guid id) {
        id.ThrowIfNull();

        var entity = await dbContext.Set<T>().FindAsync(id);

        if (entity is null) {
            throw new EntityNotFound<T>(id);
        }

        return entity;
    }

    public async Task<ImmutableList<T>> Filter(params Specification<T>[] filter) {
        var query = FilterQuery(filter);
        var result = await query.ToArrayAsync();
        return result.ToImmutableList();
    }

    public async Task<T> Find(params Specification<T>[] filter) {
        filter.ThrowIfNull();

        var query = FilterQuery(filter);

        try {
            var result = await query.SingleOrDefaultAsync();

            return result;
        } catch (InvalidOperationException error) {
            throw new MoreThanOneEntityFound(originalException: error);
        }
    }

    public async Task<Boolean> Exists(params Specification<T>[] filter) {
        filter.ThrowIfNull();

        var query = FilterQuery(filter);
        var result = await query.AnyAsync();

        return result;
    }

    public async Task<Int64> CountRecords(params Specification<T>[] filter) {
        filter.ThrowIfNull();

        var query = FilterQuery(filter);
        var result = await query.CountAsync();

        return result;
    }

    protected virtual IQueryable<T> FilterQuery(params Specification<T>[] filter) {
        var query = Query();

        foreach (var specification in filter) {
            query = query.Where(specification.Rule());
        }

        return query;
    }
}