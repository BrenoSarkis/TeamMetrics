using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamMetrics.Common.Domain;

public abstract class Entity {
    public virtual Guid Id { get; protected set; }

    public override Boolean Equals(Object otherObject) {
        var other = otherObject as Entity;

        if (other is null) {
            return false;
        }

        if (ReferenceEquals(this, other)) {
            return true;
        }

        if (GetType() != other.GetType()) {
            return false;
        }

        if (Id == Guid.Empty || other.Id == Guid.Empty) {
            return false;
        }

        return Id == other.Id;
    }

    public static Boolean operator ==(Entity a, Entity b) {
        if (a is null) {
            return b is null;
        }

        return a.Equals(b);
    }

    public static Boolean operator !=(Entity a, Entity b) => !(a == b);

    public override Int32 GetHashCode() => $"{GetType()}_{Id}".GetHashCode();
}
