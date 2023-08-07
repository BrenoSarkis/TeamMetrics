using TeamMetrics.Common;
using TeamMetrics.Common.Domain;

namespace TeamMetrics.Domain.Boundaries.Commands;

public interface Command : Message
{
    CommandMetadata Metadata { get; set; }
}

public class CommandMetadata
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public Guid CorrelationId { get; set; }
    public Guid CausationId { get; set; }
}

public class CommandBase : Command
{
    public CommandMetadata Metadata { get; set; }

    public CommandBase()
    {
        var id = Guid.NewGuid();

        Metadata = new CommandMetadata
        {
            Id = id,
            CorrelationId = id,
            CausationId = id,
        };
    }
}