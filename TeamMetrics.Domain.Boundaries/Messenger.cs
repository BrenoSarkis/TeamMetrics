﻿using TeamMetrics.Common;
using TeamMetrics.Domain.Boundaries.Commands;

namespace TeamMetrics.Domain.Boundaries;

public class Messenger {
    private readonly IServiceProvider serviceLocator;

    public Messenger(IServiceProvider serviceLocator) {
        serviceLocator.ThrowIfNull();
        this.serviceLocator = serviceLocator;
    }

    public async Task Send(Command command) {
        var tipo = typeof(CommandHandler<>);
        var commandType = command.GetType();
        var handlertype = tipo.MakeGenericType(commandType);

        dynamic handler = serviceLocator.GetService(handlertype);

        if (handler == null) {
            throw new InvalidOperationException($@"Could not find a handler for command ""{commandType.Name}"".");
        }

        await handler.Handle((dynamic)command);
    }

    public async Task<Result> Send<Result>(Query<Result> query) {
        var tipo = typeof(QueryHandler<,>);
        var queryType = query.GetType();
        var resultType = typeof(Result);
        var handlerType = tipo.MakeGenericType(queryType, resultType);

        dynamic handler = serviceLocator.GetService(handlerType);

        if (handler == null) {
            throw new InvalidOperationException($@"Could not find a handler for query ""{queryType.Name}"".");
        }

        dynamic result = await handler.Handle((dynamic)query);
        return result;
    }
}