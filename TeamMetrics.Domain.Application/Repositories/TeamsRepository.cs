using TeamMetrics.Domain.Boundaries.Repositories;
namespace TeamMetrics.Domain.Application.Repositories;

public class TeamsRepository : BaseRepository<Team> {
    public TeamsRepository(TeamMetricsDbContext dbContext) : base(dbContext) {
    }
}