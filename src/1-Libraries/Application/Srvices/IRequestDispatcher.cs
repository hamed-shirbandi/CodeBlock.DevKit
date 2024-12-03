// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Events;

namespace CodeBlock.DevKit.Application.Srvices;

/// <summary>
/// Dispatches commands, queries, and domain events within the application (in-process).
/// </summary>
public interface IRequestDispatcher
{
    Task<Result<CommandResult>> SendCommand<TCommand>(TCommand cmd)
        where TCommand : BaseCommand;
    Task<Result<TQueryResult>> SendQuery<TQueryResult>(BaseQuery<TQueryResult> query);
    Task PublishEvent(DomainEvent @event);
}
