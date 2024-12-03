// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Domain.Events;

namespace CodeBlock.DevKit.Application.Commands;

/// <summary>
///
/// </summary>
public abstract class BaseCommandHandler
{
    #region Fields


    private readonly IRequestDispatcher _requestDispatcher;

    #endregion

    #region Ctors


    protected BaseCommandHandler(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }

    #endregion

    #region Protected Methods



    /// <summary>
    /// publish domain events (in-process)
    /// </summary>
    protected async Task PublishDomainEventsAsync(IReadOnlyCollection<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
            await PublishDomainEventsAsync(domainEvent);
    }

    /// <summary>
    /// publish domain events (in-process)
    /// </summary>
    protected async Task PublishDomainEventsAsync(DomainEvent domainEvent)
    {
        await _requestDispatcher.PublishEvent(domainEvent);
    }

    #endregion

    #region Private Methods






    #endregion
}
