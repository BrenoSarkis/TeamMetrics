using TeamMetrics.Domain;
using TeamMetrics.Domain.Application.QueryHandlers;
using TeamMetrics.Domain.Boundaries;
using TeamMetrics.Domain.Boundaries.Commands;
using TeamMetrics.Domain.Boundaries.Queries;

namespace TeamMetrics.Tests.Controllers;

public class FakeMessenger : IMessenger {
    public Command SentCommand { get; private set; }
    public Query<object> SentQuery { get; private set; }

    public async Task Send(Command command) {
        SentCommand = command;
        await Task.CompletedTask;
    }

    public async Task<Result> Send<Result>(Query<Result> query) {
        SentQuery = (Query<object>) query;
        return await Task.FromResult(default(Result));
    }
}

public class FakeServiceProvider : IServiceProvider {
    public Object? GetService(Type serviceType) {
        if (serviceType == typeof(Query<IEnumerable<Team>>)) {
            return new GetTeamsHandler();
        }

        return null;
    }
}

