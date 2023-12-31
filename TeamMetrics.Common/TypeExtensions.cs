﻿using System.Reflection;
using static TeamMetrics.Common.IEnumerableExtensions;

namespace TeamMetrics.Common; 

public static class TypeExtensions {
    public static Boolean ImplementsGenericInterface(this Type @this, Type genericInterface) {
        @this.ThrowIfNull();
        genericInterface.ThrowIfNull();

        if (!genericInterface.IsGenericTypeDefinition) {
            throw new ArgumentException(
                paramName: nameof(genericInterface),
                message: $@"The argument ""{nameof(genericInterface)}"" must be a generic type.");
        }

        var interfaces =
            from @interface in @this.GetInterfaces()
            where @interface.IsGenericType
            let genericTypeDefinition = @interface.GetGenericTypeDefinition()
            where genericTypeDefinition == genericInterface
            select @interface;

        var result = Exists(interfaces);
        return result;
    }

    public static Boolean ImplementsInterface<Interface>(this Type @this) {
        @this.ThrowIfNull();

        return @this.ImplementsInterface(typeof(Interface));
    }

    public static Boolean ImplementsInterface(this Type @this, Type @interface) {
        @this.ThrowIfNull();
        @interface.ThrowIfNull();

        var interfaces =
            from anInterface in @this.GetInterfaces()
            where anInterface == @interface
            select anInterface;

        var result = Exists(interfaces);
        return result;
    }

    public static Boolean HasAttribute<Attribute>(this Type @this) where Attribute : System.Attribute {
        @this.ThrowIfNull();

        return @this.GetCustomAttributes(typeof(Attribute), inherit: true).Any();
    }

    public static TAttribute Attribute<TAttribute>(this Object @this) where TAttribute : Attribute {
        @this.ThrowIfNull();

        var type = @this.GetType();
        var attribute = type.Attribute<TAttribute>();
        return attribute;
    }

    public static TAttribute Attribute<TAttribute>(this Type @this) where TAttribute : Attribute {
        @this.ThrowIfNull();

        var attribute = (TAttribute)@this.GetCustomAttributes(typeof(TAttribute), inherit: true).FirstOrDefault();
        return attribute;
    }

    public static IEnumerable<TAttribute> Attributes<TAttribute>(this Type @this) where TAttribute : Attribute {
        @this.ThrowIfNull();

        var attributes = (TAttribute[])@this.GetCustomAttributes(typeof(TAttribute), inherit: true);
        return attributes;
    }

    public static TAttribute Attribute<TAttribute>(this PropertyInfo @this) where TAttribute : Attribute {
        @this.ThrowIfNull();

        var attribute = (TAttribute)@this.GetCustomAttributes(typeof(TAttribute), inherit: true).FirstOrDefault();
        return attribute;
    }

    public static IEnumerable<TAttribute> Attributes<TAttribute>(this PropertyInfo @this) where TAttribute : Attribute {
        @this.ThrowIfNull();

        var attributes = (TAttribute[])@this.GetCustomAttributes(typeof(TAttribute), inherit: true);
        return attributes;
    }

    public static IEnumerable<Type> FielsOfType<Type>(this System.Type @this) {
        return @this.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(x => @this.IsAssignableFrom(x.FieldType))
            .Select(y => (Type)y.GetValue(null))
            .ToList();
    }
}