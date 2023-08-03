namespace TeamMetrics.Common;

public static class ArgumentExtensions {
    public static Boolean WasProvided(this String @this) {
        return !String.IsNullOrWhiteSpace(@this);
    }

    public static Boolean WasNotProvided(this String @this) {
        return !@this.WasProvided();
    }

    public static Boolean WasProvided<Tipo>(this Tipo @this) {
        return !Equals(@this, default(Tipo));
    }

    public static Boolean WasNotProvided<Tipo>(this Tipo @this) {
        return !@this.WasProvided();
    }
}
