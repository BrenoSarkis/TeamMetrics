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