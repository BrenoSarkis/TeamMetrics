namespace TeamMetrics.Common.Domain.Repositories;

public class MoreThanOneEntityFound : BusinessError {
    public MoreThanOneEntityFound(String message = "More than one entity found for the specified filter.", Exception originalException = null) : base(message, originalException) {
    }
}