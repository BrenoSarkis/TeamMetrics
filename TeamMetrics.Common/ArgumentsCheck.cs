using System.Runtime.CompilerServices;

namespace TeamMetrics.Common;

public static class ArgumentsCheck {
    public static void ThrowIfNullOrWhiteSpace(this String @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        if (String.IsNullOrWhiteSpace(parameterName)) {
            throw new ArgumentNullException("", $@"The argument ""{nameof(parameterName)}"" was not provided.");
        }

        if (String.IsNullOrWhiteSpace(@this)) {
            throw new ArgumentNullException("", $@"The argument ""{parameterName}"" was not provided.");
        }
    }

    public static void ThrowIfNull<TypeDoArgumento>(this TypeDoArgumento @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        if (Equals(@this, default(TypeDoArgumento))) {
            throw new ArgumentNullException("", $@"The argument ""{parameterName}"" was not provided.");
        }
    }

    public static void ThrowIfNegative(this Int16 @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        if (@this < 0) {
            throw new ArgumentOutOfRangeException("", $@"The argument ""{parameterName}"" can't be negative.");
        }
    }

    public static void ThrowIfNegative(this Int32 @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        if (@this < 0) {
            throw new ArgumentOutOfRangeException("", $@"The argument ""{parameterName}"" can't be negative.");
        }
    }

    public static void ThrowIfNegative(this Decimal @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        if (@this < 0) {
            throw new ArgumentOutOfRangeException("", $@"The argument ""{parameterName}"" can't be negative.");
        }
    }

    public static void ThrowIfNullOrEmpty<TypeDoArgumento>(this IEnumerable<TypeDoArgumento> @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        if (@this == null || !@this.Any()) {
            throw new ArgumentOutOfRangeException("", $@"The argument ""{parameterName}"" can't not be inpty.");
        }
    }

    public static void ThrowIfItinsCountIsBiggerThan<TypeDoArgumento>(this IEnumerable<TypeDoArgumento> @this, Int64 count, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        @this.ThrowIfNull();
        count.ThrowIfNull();
        parameterName.ThrowIfNullOrWhiteSpace();

        if (@this.Count() > count) {
            throw new ArgumentOutOfRangeException("", $@"The argument ""{parameterName}"" must not contain more than {count} itins.");
        }
    }

    public static void ThrowIf<ArgumentType>(this ArgumentType @this, Func<ArgumentType, Boolean> condition, [CallerArgumentExpression(nameof(@this))] String parameterName = null, String message = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        if (condition(@this)) {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $@"The argument ""{parameterName}"" is invalid.");
        }
    }

    public static void ThrowIfItIsNotOfType<Type>(this String @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        @this.ThrowIfNull();
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }

    public static void ThrowIfItIsNotOfType<Type>(this Int32? @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        @this.ThrowIfNull();
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }

    public static void ThrowIfItIsNotOfType<Type>(this Int32 @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }

    public static void ThrowIfItIsNotOfType<Type>(this Int64? @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        @this.ThrowIfNull();
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }

    public static void EnsureItIsOfType<Type>(this Int64 @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }

    public static void EnsureItIsOfType<Type>(this TimeSpan? @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        @this.ThrowIfNull();
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }

    public static void EnsureItIsOfType<Type>(this TimeSpan @this, [CallerArgumentExpression(nameof(@this))] String parameterName = null) {
        parameterName.ThrowIfNullOrWhiteSpace();

        try {
            _ = (Type)(dynamic)@this;
        } catch (BusinessError) {
            throw;
        } catch {
            throw new ArgumentOutOfRangeException(parameterName, $@"Could not convert ""{@this}"" in ""{typeof(Type)}"".");
        }
    }
}