using TeamMetrics.Common.Domain;

namespace TeamMetrics.Domain.Boundaries.Queries;

public interface Query<out TResult> : Message {
}