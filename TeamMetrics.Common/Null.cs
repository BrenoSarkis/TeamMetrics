namespace TeamMetrics.Common;

public class Null {
    public static readonly Null instance;

    static Null() {
        instance = new Null();
    }

    public static Null Instance => instance;

    private Null() {
    }
}