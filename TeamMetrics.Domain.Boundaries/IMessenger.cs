using TeamMetrics.Domain.Boundaries.Commands;
using TeamMetrics.Domain.Boundaries.Queries;

namespace TeamMetrics.Domain.Boundaries;

public interface IMessenger {
    Task Send(Command command);
    Task<Result> Send<Result>(Query<Result> query);
}