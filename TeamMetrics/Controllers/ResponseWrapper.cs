using TeamMetrics.Common;
using TeamMetrics.Common.Notification;

namespace TeamMetrics.Api.Controllers;

public static class ResponseWrapper {
    public static ResponseWrapper<Object> Error(params Notification[] messages) {
        messages.ThrowIfNullOrEmpty(nameof(messages));

        return new ResponseWrapper<Object>(null, messages);
    }

    public static ResponseWrapper<Object> Error(IEnumerable<Notification> messages) {
        messages.ThrowIfNullOrEmpty(nameof(messages));

        return new ResponseWrapper<Object>(null, messages);
    }

    public static ResponseWrapper<Object> Error(BusinessError error) {
        error.ThrowIfNull();

        return Error(Notification.Error(error));
    }

    public static ResponseWrapper<Object> Ok() {
        return new ResponseWrapper<Object>(null);
    }

    public static ResponseWrapper<Object> Ok<T>(T result) {
        return new ResponseWrapper<Object>(result);
    }
}

public class ResponseWrapper<TData> {
    public ResponseWrapper(TData data, IEnumerable<Notification> notifications = null) {
        Data = data;
        Notifications = notifications ?? Enumerable.Empty<Notification>();
    }
    public TData Data { get; }

    public IEnumerable<Notification> Notifications { get; }

    public static implicit operator TData(ResponseWrapper<TData> result) {
        return result == null ? default : result.Data;
    }

    public void Deconstruct(out TData data, out IEnumerable<Notification> notifications) {
        data = Data;
        notifications = Notifications;
    }
}