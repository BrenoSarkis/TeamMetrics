using System.Collections.Immutable;

namespace TeamMetrics.Common.Domain;

public class Aggregate : Entity {
    private readonly List<Event> events = new();

    public virtual IReadOnlyCollection<Event> Events => events.ToImmutableList();

    protected virtual void AddEvents(params Event[] events) {
        this.events.ForEach(AddEvent);
    }

    protected virtual void AddEvent(Event @event) {
        events.Add(@event);
    }

    public virtual void ClearEvents() {
        events.Clear();
    }
}