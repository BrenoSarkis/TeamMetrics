using System.ComponentModel;
using System;

namespace TeamMetrics.Common.Domain.Repositories;

public class EntityNotFound : BusinessError {
    public EntityNotFound(String message = "Entity not found.", Exception originalError = null) : base(message, originalError) {
    }
}

public class EntityNotFound<T> : EntityNotFound where T : Entity {
    public EntityNotFound(String message = null, Exception originalError = null) : base(message ?? ErrorMessage(), originalError) {
    }

    private static String ErrorMessage() {
        var type = typeof(T);
        var entityName = type.Name;

        if (type.HasAttribute<DescriptionAttribute>()) {
            entityName = type.Attribute<DescriptionAttribute>().Description;
        }

        return $@"{entityName} not found.";
    }

    public EntityNotFound(Guid id, Exception originalError = null) : base(ErrorMessage(id), originalError) {
    }

    private static String ErrorMessage(Guid id) {
        var type = typeof(T);
        var entityName = type.Name;


        if (type.HasAttribute<DescriptionAttribute>()) {
            entityName = type.Attribute<DescriptionAttribute>().Description;
        }

        return $@"{entityName} not found with id ""{id}"".";
    }
}