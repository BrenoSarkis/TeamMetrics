using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TeamMetrics.Domain.Application;

public class TeamMetricsDbContext : DbContext {

    public TeamMetricsDbContext(DbContextOptions<TeamMetricsDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
