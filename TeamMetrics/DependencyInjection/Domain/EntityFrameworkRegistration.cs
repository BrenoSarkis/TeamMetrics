using Microsoft.EntityFrameworkCore;
using TeamMetrics.Common;
using TeamMetrics.Domain.Application;

namespace TeamMetrics.Api.DependencyInjection.Domain;

public static class EntityFrameworkRegistration {
    private static readonly TimeSpan COMMAND_TIMEOUT;

    static EntityFrameworkRegistration() {
        COMMAND_TIMEOUT = TimeSpan.FromMinutes(10);
    }

    public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration config) {
        services.ThrowIfNull();
        config.ThrowIfNull();

        services.DbContextRegistration(config);

        return services;
    }

    private static void DbContextRegistration(this IServiceCollection services, IConfiguration config) {
        var parametros = DbConfig(config);

        services.AddDbContext<TeamMetricsDbContext>((provider, ctx) => {
            ctx.UseSqlServer(parametros.ConnectionString, sql => {
                sql.EnableRetryOnFailure(3);
                sql.CommandTimeout((Int32)parametros.CommandTimeout.TotalSeconds);
            });

            ctx.UseLazyLoadingProxies();
            ctx.EnableDetailedErrors();
            ctx.EnableSensitiveDataLogging();
        });
    }

    private static (String ConnectionString, TimeSpan CommandTimeout) DbConfig(IConfiguration config) {
        config.ThrowIfNull();

        var connectionString = ConnectionString(config);
        var commandTimeout = CommandTimeout();
        return (connectionString, commandTimeout);
    }

    private static String ConnectionString(IConfiguration config) {
        config.ThrowIfNull();

        var connectionString = config.GetValue<String>("ConnectionStrings:fake");

        if (connectionString.WasNotProvided()) {
            throw new Exception("Could not find a connection string to the database.");
        }

        return connectionString;
    }

    private static TimeSpan CommandTimeout() {
        return COMMAND_TIMEOUT;
    }
}
