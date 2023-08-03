using System.Security.Claims;
using TeamMetrics.Common;
using TeamMetrics.Domain.Application;
using TeamMetrics.Domain.Boundaries;

namespace TeamMetrics.Api.DependencyInjection.Domain;

public static class QueryRegistration {
    public static IServiceCollection AddQueries(this IServiceCollection services) {
        services.ThrowIfNull();

        QueryHandlers().ForEach(handler => {
            AddExecutorDeConsulta(services, handler);
        });

        return services;
    }

    private static IEnumerable<Type> QueryHandlers() {
        var handleres = typeof(GetTeams).Assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(IsAQueryHandler))
            .Where(x => !x.IsAbstract)
            .ToArray();

        return handleres;
    }

    private static Boolean IsAQueryHandler(Type tipo) {
        if (!tipo.IsGenericType) {
            return false;
        }

        var tipoGenerico = tipo.GetGenericTypeDefinition();
        var resultado = tipoGenerico == typeof(QueryHandler<,>);
        return resultado;
    }

    private static void AddExecutorDeConsulta(IServiceCollection services, Type queryHandler) {
        var pipeline = Decorators(queryHandler).Concat(new []{ queryHandler }).ToArray();
        var @interface = queryHandler.GetInterfaces().Single(IsAQueryHandler);
        var factory = BuildHandlerPipeline(pipeline, @interface);

        services.AddTransient(@interface, factory);
    }

    private static IEnumerable<Type> Decorators(Type handlerDeConsulta) {
        handlerDeConsulta.ThrowIfNull();

        yield break;
    }

    private static Func<IServiceProvider, Object> BuildHandlerPipeline(IEnumerable<Type> pipeline, Type @interface) {
        var reversedPipeline = pipeline.Reverse().ToArray();

        var constructors = reversedPipeline.Select(x => {
            var type = x.IsGenericType ? x.MakeGenericType(@interface.GenericTypeArguments) : x;
            return type.GetConstructors().Single();
        }).ToArray();

        return serviceLocator => {
            var handler = null as Object;

            constructors.ForEach(construtor => {
                var message = construtor.GetParameters();

                var arguments = message.Select(parameter => {
                    var parameterType = parameter.ParameterType;

                    if (IsAQueryHandler(parameterType)) {
                        return handler;
                    }

                    var argument = serviceLocator.GetRequiredService(parameterType);
                    return argument;
                });

                handler = construtor.Invoke(arguments.ToArray());
            });

            return handler;
        };
    }
}