using TeamMetrics.Common.Specifications;

namespace TeamMetrics.Domain.Specifications.ForTeam;

public static class AllSpecifications {
    public static Specification<Team> WithId(Guid id) => new WithId(id);
    public static Specification<Team> All() => Specification<Team>.identification;
}   