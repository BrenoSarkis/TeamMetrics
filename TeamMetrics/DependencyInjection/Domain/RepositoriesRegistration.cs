using TeamMetrics.Common;
using TeamMetrics.Domain.Application;
using TeamMetrics.Domain.Boundaries;

namespace TeamMetrics.Api.DependencyInjection.Domain;

public static class RepositoriesRegistration {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.ThrowIfNull();

        var repos =
            from @class in typeof(TeamRepository).Assembly.GetTypes()
            from @interface in @class.GetInterfaces()
            where @class.ImplementsGenericInterface(typeof(Repository<>)) && !@interface.IsGenericType
            select new {classe = @class, @interface };

        foreach (var repository in repos) {
            services.AddTransient(repository.@interface, repository.classe);
        }

        return services;
    }
}