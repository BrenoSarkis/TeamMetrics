using TeamMetrics.Common;
using TeamMetrics.Domain.Application;
using TeamMetrics.Domain.Boundaries;

namespace TeamMetrics.Api.DependencyInjection.Messages;

public static class MessengerRegistration {
    public static IServiceCollection AddMessenger(this IServiceCollection services) {
        services.ThrowIfNull();

        services.AddScoped<IMessenger>(serviceLocator => new Messenger(serviceLocator));
        return services;
    }
}