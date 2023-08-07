using System.Linq.Expressions;
using TeamMetrics.Common;
using TeamMetrics.Common.Specifications;

namespace TeamMetrics.Domain.Specifications.ForTeam;

public class WithId : Specification<Team> {
    private readonly Guid id;

    public WithId(Guid id) {
        id.ThrowIfNull();

        this.id = id;
    }

    public override Expression<Func<Team, Boolean>> Rule() {
        return x => x.Id == id;
    }
}