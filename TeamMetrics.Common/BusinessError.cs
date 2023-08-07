namespace TeamMetrics.Common;

public abstract class BusinessError : Exception {
    protected BusinessError(String message, Exception originalErrorException = null) : base(message, originalErrorException) {
    }
}