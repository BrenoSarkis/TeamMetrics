//using Microsoft.EntityFrameworkCore;
//using TeamMetrics.Common;
//using TeamMetrics.Domain.Application;
//using TeamMetrics.Domain.Boundaries.Commands;
//using Command = TeamMetrics.Domain.Boundaries.Commands.Command;

//namespace TeamMetrics.Domain.Interceptors.Commands;

//public sealed class UnitOfWork<TCommand> : CommandHandler<TCommand> where TCommand : Command {
//    private readonly CommandHandler<TCommand> handler;
//    private readonly TeamMetricsDbContext context;

//    public UnitOfWork(CommandHandler<TCommand> handler, TeamMetricsDbContext context) {
//        handler.ThrowIfNull();
//        context.ThrowIfNull();

//        this.handler = handler;
//        this.context = context;
//    }

//    public async Task Handle(TCommand command) {
//        command.ThrowIfNull();

//        var strategy = context.Database.CreateExecutionStrategy();

//        await strategy.ExecuteAsync(async () => {
//            await using var transaction = await context.Database.BeginTransactionAsync();
//            await handler.Handle(command);
//            await context.SaveChangesAsync();
//            await transaction.CommitAsync();
//        });
//    }
//}