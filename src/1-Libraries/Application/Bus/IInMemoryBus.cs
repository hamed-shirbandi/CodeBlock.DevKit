using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Events;

namespace CodeBlock.DevKit.Application.Bus;

/// <summary>
/// It is used as a mediator to send and handle requests inside a service (in-process)
/// </summary>
public interface IInMemoryBus
{
    Task<Result<CommandResult>> SendCommand<TCommand>(TCommand cmd)
        where TCommand : BaseCommand;
    Task<Result<TQueryResult>> SendQuery<TQueryResult>(BaseQuery<TQueryResult> query);
    Task PublishEvent(DomainEvent @event);
}
