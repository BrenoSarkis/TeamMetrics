namespace TeamMetrics.Common.Domain;

public interface Evento : Message {
    EventMetadata Metadata { get; set; }
}

public class EventMetadata {
    public Guid Id { get; set; }
    public String Type { get; set; }
    public Guid CorrelationId { get; set; }
    public Guid CausationId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}
