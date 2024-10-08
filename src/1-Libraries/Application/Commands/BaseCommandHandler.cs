﻿using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Domain.Events;

namespace CodeBlock.DevKit.Application.Commands;

/// <summary>
///
/// </summary>
public abstract class BaseCommandHandler
{
    #region Fields


    private readonly IInMemoryBus _inMemoryBus;

    #endregion

    #region Ctors


    protected BaseCommandHandler(IInMemoryBus inMemoryBus)
    {
        _inMemoryBus = inMemoryBus;
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
        await _inMemoryBus.PublishEvent(domainEvent);
    }

    #endregion

    #region Private Methods






    #endregion
}
