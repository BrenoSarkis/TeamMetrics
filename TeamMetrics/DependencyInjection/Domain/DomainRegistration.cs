using TeamMetrics.Common;

namespace TeamMetrics.Api.DependencyInjection.Domain;

public static class DomainRegistration {
    public static IServiceCollection RegisterDomain(this IServiceCollection services, IConfiguration config) {
        services.ThrowIfNull();
        config.ThrowIfNull();

        return services
            .AddCommands()
            .AddQueries()
            .AddRepositories()
            //.AddEntityFramework(config)
            ;
    }
}