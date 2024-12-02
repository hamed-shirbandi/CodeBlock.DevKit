// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using MediatR;
using Newtonsoft.Json;

namespace CodeBlock.DevKit.Domain.Events;

/// <summary>
///
/// </summary>
public class DomainEvent : INotification
{
    public DomainEvent(string entityId, string entityType)
    {
        EntityId = entityId;
        EntityType = entityType;
        EventType = GetType().Name;
        OccurredOn = DateTime.Now;
    }

    /// <summary>
    /// Use JsonIgnore to prevent adding it to Data in StoredEvent. As it will be mapped to StoredEvent.EntityId and no need to have it in StoredEvent.Data
    /// </summary>
    [JsonIgnore]
    public string EntityId { get; }

    /// <summary>
    /// Use JsonIgnore to prevent adding it to Data in StoredEvent.
    /// </summary>
    [JsonIgnore]
    public string EntityType { get; }

    /// <summary>
    /// Use JsonIgnore to prevent adding it to Data in StoredEvent.
    /// </summary>
    [JsonIgnore]
    public string EventType { get; }

    public DateTime OccurredOn { get; }
}

