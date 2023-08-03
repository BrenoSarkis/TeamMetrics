namespace TeamMetrics.Common;

public abstract class BusinessError : Exception {
    protected BusinessError(String message, Exception originalException = null) : base(message, originalException) {
    }
}