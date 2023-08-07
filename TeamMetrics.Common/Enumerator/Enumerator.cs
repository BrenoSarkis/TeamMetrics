using System.Collections.Immutable;
using System.ComponentModel;
using System.Reflection;

namespace TeamMetrics.Common.Enumerator;

public interface IEnumerator {
}

public abstract class Enumerator<TEnum> : IEnumerator, IEquatable<Enumerator<TEnum>>, IComparable<Enumerator<TEnum>> where TEnum : Enumerator<TEnum> {
    private static readonly Lazy<TEnum[]> all = new(RetrieveAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);
    private static readonly Lazy<Dictionary<String, TEnum>> dictionaryByName = new(() => all.Value.ToDictionary(item => item.Name));
    private static readonly Lazy<Dictionary<String, TEnum>> dictionaryByNameIgnoringCase = new(() => all.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));
    private static readonly Lazy<Dictionary<String, TEnum>> dictionaryById = new(() => all.Value.ToDictionary(item => item.Id));
    private static readonly Lazy<Dictionary<String, TEnum>> dictionaryByIdIgnoringCase = new(() => all.Value.ToDictionary(item => item.Id, StringComparer.OrdinalIgnoreCase));

    private static TEnum[] RetrieveAllOptions() {
        var type = typeof(TEnum);

        return Assembly.GetAssembly(type)
            ?.GetTypes()
            .Where(t => type.IsAssignableFrom(t))
            .SelectMany(t => t.FielsOfType<TEnum>())
            .OrderBy(t => t.Name)
            .ToArray();
    }

    public static ImmutableList<TEnum> Todas() => Todos();
    public static ImmutableList<TEnum> Todos() => dictionaryByName.Value.Values.ToImmutableList();

    private readonly String name;
    private readonly String id;

    protected Enumerator(String id, String name) {
        id.ThrowIfNullOrWhiteSpace();
        name.ThrowIfNullOrWhiteSpace();

        this.id = id;
        this.name = name;
    }

    public String Id => id;

    public String Name => name;

    public static TEnum WithIdOrName(String idOrName, Boolean ignoreCase = false) {
        idOrName.ThrowIfNull();

        var enumerator = null as TEnum;

        if (TryFindByIdOrName(idOrName, ignoreCase, out enumerator)) {
            return enumerator;
        }

        throw new EnumeratorNotFound(typeof(TEnum), idOrName);
    }

    public static Boolean TryFindByIdOrName(String idOrName, out TEnum enumerator) => TryFindByIdOrName(idOrName, false, out enumerator);

    public static Boolean TryFindByIdOrName(String idOrName, Boolean ignoreCase, out TEnum enumerator) {
        if (idOrName.WasNotProvided()) {
            enumerator = default;
            return false;
        }

        if (TryFindById(idOrName, ignoreCase, out enumerator)) {
            return true;
        }

        if (TryFindByName(idOrName, ignoreCase, out enumerator)) {
            return true;
        }

        return false;
    }

    public static TEnum WithId(String id, Boolean ignoreCase = false) {
        id.ThrowIfNullOrWhiteSpace();

        if (TryFindById(id, ignoreCase, out var enumerator)) {
            return enumerator;
        }

        throw new EnumeratorNotFoundWithId(typeof(TEnum), id);
    }

    public static Boolean TryFindById(String id, out TEnum enumerator) => TryFindById(id, false, out enumerator);

    public static Boolean TryFindById(String id, Boolean ignoreCase, out TEnum enumerator) {
        if (id.WasNotProvided()) {
            enumerator = default;
            return false;
        }

        if (ignoreCase) {
            return dictionaryByIdIgnoringCase.Value.TryGetValue(id, out enumerator);
        }

        return dictionaryById.Value.TryGetValue(id, out enumerator);
    }

    public static TEnum WithName(String name, Boolean ignoreCase = false) {
        name.ThrowIfNullOrWhiteSpace();

        if (TryFindByName(name, ignoreCase, out var enumerator)) {
            return enumerator;
        }

        throw new EnumeratorNotFoundWithName(typeof(TEnum), name);
    }

    public static Boolean TryFindByName(String name, out TEnum enumerator) => TryFindByName(name, false, out enumerator);

    public static Boolean TryFindByName(String name, Boolean ignoreCase, out TEnum enumerator) {
        if (name.WasNotProvided()) {
            enumerator = default;
            return false;
        }

        if (ignoreCase) {
            return dictionaryByNameIgnoringCase.Value.TryGetValue(name, out enumerator);
        }

        return dictionaryByName.Value.TryGetValue(name, out enumerator);
    }

    public override String ToString() => name;

    public override Int32 GetHashCode() => id.GetHashCode();

    public override Boolean Equals(Object @object) => (@object is Enumerator<TEnum> other) && Equals(other);

    public virtual Boolean Equals(Enumerator<TEnum> other) {
        if (ReferenceEquals(this, other)) {
            return true;
        }

        if (other is null) {
            return false;
        }

        return id.Equals(other.id);
    }

    public static Boolean operator ==(Enumerator<TEnum> x, Enumerator<TEnum> y) {
        if (x is null) {
            return y is null;
        }

        return x.Equals(y);
    }

    public static Boolean operator !=(Enumerator<TEnum> x, Enumerator<TEnum> y) => !(x == y);

    public virtual Int32 CompareTo(Enumerator<TEnum> outroEnumerator) {
        if (outroEnumerator is null) {
            return Int32.MaxValue;
        }

        return id.CompareTo(outroEnumerator.id);
    }

    public static Boolean operator <(Enumerator<TEnum> x, Enumerator<TEnum> y) => x.CompareTo(y) < 0;

    public static Boolean operator <=(Enumerator<TEnum> x, Enumerator<TEnum> y) => x.CompareTo(y) <= 0;

    public static Boolean operator >(Enumerator<TEnum> x, Enumerator<TEnum> y) => x.CompareTo(y) > 0;

    public static Boolean operator >=(Enumerator<TEnum> x, Enumerator<TEnum> y) => x.CompareTo(y) >= 0;

    public static implicit operator String(Enumerator<TEnum> enumerator) => enumerator?.ToString();

    public static implicit operator Enumerator<TEnum>(String @string) => WithIdOrName(@string);
}

public class EnumeratorNotFound : BusinessError {
    public EnumeratorNotFound(String message = "Enumerator not found.", Exception originalErrorException = null) : base(message, originalErrorException) {
    }

    public EnumeratorNotFound(Type type, String value, Exception erroOriginalError = null) : base(ErrorMessage(type, value), erroOriginalError) {
    }

    private static String ErrorMessage(Type type, String value) {
        if (type.HasAttribute<DescriptionAttribute>()) {
            var name = ((Object)type).Attribute<DescriptionAttribute>().Description;
            return $@"""{value}"" is not a valid {name}.";
        }

        return $@"""{value}"" is not a valid ""{type.Name}"".";
    }
}

public class EnumeratorNotFoundWithName : EnumeratorNotFound {
    public EnumeratorNotFoundWithName(String message = "Enumerator não encontrado com o name informado.", Exception originalErrorException = null) : base(message, originalErrorException) {
    }

    public EnumeratorNotFoundWithName(Type type, String name, Exception originalErrorException = null) : base(ErrorMessage(type, name), originalErrorException) {
    }

    private static String ErrorMessage(Type type, String value) {
        if (type.HasAttribute<DescriptionAttribute>()) {
            var name = ((Object) type).Attribute<DescriptionAttribute>().Description;
            return $@"""{value}"" is not a valid name for {name}.";
        }

        return $@"""{value}"" is not a valid name for ""{type.Name}"".";
    }
}

public class EnumeratorNotFoundWithId : EnumeratorNotFound {
    public EnumeratorNotFoundWithId(String message = "Enumerator não encontrado com o código informado.", Exception originalErrorException = null) : base(message, originalErrorException) {
    }

    public EnumeratorNotFoundWithId(Type type, String id, Exception originalErrorException = null) : base(ErrorMessage(type, id), originalErrorException) {
    }

    private static String ErrorMessage(Type type, String id) {
        if (type.HasAttribute<DescriptionAttribute>()) {
            var name = ((Object) type).Attribute<DescriptionAttribute>().Description;
            return $@"""{id}"" is not a valid id for {name}.";
        }

        return $@"""{id}"" is not a valid id for ""{type.Name}"".";
    }
}

public readonly struct IfItIsAnEnumerator<TEnum> where TEnum : IEnumerator {
    private readonly IEnumerator enumerator;
    private readonly Boolean stopEvaluating;

    internal IfItIsAnEnumerator(Boolean stopEvaluating, IEnumerator enumerator) {
        this.stopEvaluating = stopEvaluating;
        this.enumerator = enumerator;
    }

    public void Otherwise(Action @do) {
        if (!stopEvaluating) {
            @do();
        }
    }

    public ThenDo<TEnum> IfItIs(IEnumerator enumerator) => new(isMatch: this.enumerator.Equals(enumerator), stopEvaluating: stopEvaluating, enumerator: this.enumerator);

    public ThenDo<TEnum> IfItIs(params IEnumerator[] enumeratores) => new(isMatch: enumeratores.Contains(enumerator), stopEvaluating: stopEvaluating, enumerator: enumerator);

    public ThenDo<TEnum> IfItIs(IEnumerable<IEnumerator> enumeratores) => new(isMatch: enumeratores.Contains(enumerator), stopEvaluating: stopEvaluating, enumerator: enumerator);
}


public readonly struct ThenDo<TEnum> where TEnum : IEnumerator {
    private readonly Boolean isMatch;
    private readonly IEnumerator enumerator;
    private readonly Boolean stopEvaluating;

    internal ThenDo(Boolean isMatch, Boolean stopEvaluating, IEnumerator enumerator) {
        this.isMatch = isMatch;
        this.enumerator = enumerator;
        this.stopEvaluating = stopEvaluating;
    }

    public IfItIsAnEnumerator<TEnum> Then(Action facaIsso) {
        if (!stopEvaluating && isMatch) {
            facaIsso();
        }

        return new(stopEvaluating || isMatch, enumerator);
    }
}

public static class ExtensoesDeEnumerator {
    public static ThenDo<TEnum> IfItIs<TEnum>(this Enumerator<TEnum> @this, Enumerator<TEnum> enumerator) where TEnum : Enumerator<TEnum> => new(@this is not null && @this.Equals(enumerator), false, @this);

    public static ThenDo<TEnum> IfItIs<TEnum>(this Enumerator<TEnum> @this, params Enumerator<TEnum>[] enumeratores) where TEnum : Enumerator<TEnum> => new(enumeratores.Contains(@this), false, @this);

    public static ThenDo<TEnum> IfItIs<TEnum>(this Enumerator<TEnum> @this, IEnumerable<Enumerator<TEnum>> enumeratores) where TEnum : Enumerator<TEnum> => new(enumeratores is not null && enumeratores.Contains(@this), false, @this);
}