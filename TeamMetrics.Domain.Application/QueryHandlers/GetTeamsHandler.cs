using TeamMetrics.Domain.Boundaries;
using TeamMetrics.Domain.Boundaries.Queries;

namespace TeamMetrics.Domain.Application.QueryHandlers;

public class GetTeamsHandler : QueryHandler<GetTeams, IEnumerable<Team>> {
    public async Task<IEnumerable<Team>> Handle(GetTeams mensagem) {
        return new List<Team> {
            new Team(Guid.NewGuid()),
        };
    }
}
