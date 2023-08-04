namespace TeamMetrics.Domain;

public class Team {
    public Guid Id { get; set; }

    public Team(Guid id) {
        Id = id;
    }
}
