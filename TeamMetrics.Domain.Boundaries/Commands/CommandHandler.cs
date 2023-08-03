namespace TeamMetrics.Domain.Boundaries.Commands;

public interface CommandHandler<in T> : Handler<T> where T : Command {
}