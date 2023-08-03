namespace TeamMetrics.Common;

public static class IEnumerableExtensions {
    public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action) {
        @this.ThrowIfNull();

        foreach (var item in @this) {
            action(item);
        }
    }

    public static Boolean DoesNotExist<T>(IEnumerable<T> data) => !Exists(data);

    public static Boolean Exists<T>(IEnumerable<T> data) => data is not null && data.Any();
}
