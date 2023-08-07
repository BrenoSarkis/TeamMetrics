using TeamMetrics.Common.Domain.Repositories;
using TeamMetrics.Common.Specifications;

namespace TeamMetrics.Common.Domain;

public static class SpecificationExtensions {
    public static T Get<T>(this IEnumerable<T> @this, Guid id) where T : Entity {
        @this.ThrowIfNull();

        try {
            var entity = @this.First(x => x.Id == id);
            return entity;
        } catch (InvalidOperationException) {
            throw new EntityNotFound<T>(id);
        }
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> @this, params Specification<T>[] filter) where T : Entity {
        @this.ThrowIfNull();

        var query = @this.FilteredQuery(filter);
        return query.ToArray();
    }

    public static T Find<T>(this IEnumerable<T> @this, params Specification<T>[] filter) where T : Entity {
        @this.ThrowIfNull();
        filter.ThrowIfNull();

        var query = @this.FilteredQuery(filter);

        try {
            var result = query.SingleOrDefault();
            return result;
        } catch (InvalidOperationException error) {
            throw new MoreThanOneEntityFound(originalException: error);
        }
    }

    public static Boolean Exists<T>(this IEnumerable<T> @this, params Specification<T>[] filter) where T : Entity {
        @this.ThrowIfNull();
        filter.ThrowIfNull();

        var query = @this.FilteredQuery(filter);
        var result = query.Any();
        return result;
    }

    public static Int64 CountRecords<T>(this IEnumerable<T> @this, params Specification<T>[] filter) where T : Entity {
        @this.ThrowIfNull();
        filter.ThrowIfNull();

        var query = @this.FilteredQuery(filter);
        var result = query.Count();
        return result;
    }

    private static IEnumerable<T> FilteredQuery<T>(this IEnumerable<T> @this, params Specification<T>[] filter) where T : Entity {
        @this.ThrowIfNull();

        var query = @this;

        foreach (var specification in filter) {
            query = query.Where(specification.Rule().Compile());
        }

        return query;
    }
}