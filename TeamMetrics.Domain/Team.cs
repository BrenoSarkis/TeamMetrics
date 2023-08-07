using TeamMetrics.Common.Domain;

namespace TeamMetrics.Domain;

public class Team : Aggregate {
    public Guid Id { get; set; }

    public Team(Guid id) {
        Id = id;
    }
}
