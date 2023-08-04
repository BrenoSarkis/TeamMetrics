using System.Linq;
using TeamMetrics.Common;
using TeamMetrics.Domain.Application;
using TeamMetrics.Domain.Application.CommandHandlers;
using TeamMetrics.Domain.Boundaries.Commands;

namespace TeamMetrics.Api.DependencyInjection.Domain;

public static class CommandRegistration {
    public static IServiceCollection AddCommands(this IServiceCollection services) {
        services.ThrowIfNull();

        CommandHandlers().ForEach(handler => {
            AddCommandHandler(services, handler);
        });

        return services;
    }

    private static IEnumerable<Type> CommandHandlers() {
        var handlers = typeof(SynchronizeTeamsHandler).Assembly.GetTypes()
            .Where(IsACommandHandler)
            .ToArray();

        return handlers;
    }

    private static Boolean IsACommandHandler(Type type) {
        type.ThrowIfNull();

        if (type.IsAbstract) {
            return false;
        }

        foreach (var @interface in type.GetInterfaces()) {
            if (IsACommandHandlerInterface(@interface)) {
                return true;
            }
        }

        return false;
    }

    private static Boolean IsACommandHandlerInterface(Type type) {
        type.ThrowIfNull();

        if (!type.IsInterface) {
            return false;
        }

        if (!type.IsGenericType) {
            return false;
        }

        var genericType = type.GetGenericTypeDefinition();
        var result = genericType == typeof(CommandHandler<>);
        return result;
    }

    private static void AddCommandHandler(IServiceCollection services, Type commandHandler) {
        var pipeline = Decorators().Concat(new[] { commandHandler }).ToArray();
        var @interface = commandHandler.GetInterfaces().Single(IsACommandHandlerInterface);
        var factory = BuildHandlerPipeline(pipeline, @interface);

        services.AddTransient(@interface, factory);
    }

    private static IEnumerable<Type> Decorators() {
        //yield return typeof(UnitOfWork<>);
        yield break;
    }

    private static Func<IServiceProvider, Object> BuildHandlerPipeline(IEnumerable<Type> pipeline, Type @interface) {
        var inversedPipeline = pipeline.Reverse().ToArray();

        var constructors = inversedPipeline.Select(x => {
            var type = x.IsGenericType ? x.MakeGenericType(@interface.GenericTypeArguments) : x;
            return type.GetConstructors().Single();
        }).ToArray();

        return serviceLocator => {
            var handler = null as Object;

            constructors.ForEach(construtor => {
                var message = construtor.GetParameters();

                var arguments = message.Select(parameter => {
                    var parameterType = parameter.ParameterType;

                    if (IsACommandHandlerInterface(parameterType)) {
                        return handler;
                    }

                    var argument = serviceLocator.GetRequiredService(parameterType);
                    return argument;
                }).ToArray();

                handler = construtor.Invoke(arguments);
            });

            return handler;
        };
    }
}