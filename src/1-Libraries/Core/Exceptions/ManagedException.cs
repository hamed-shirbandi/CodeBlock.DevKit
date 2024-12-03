// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Core.Exceptions;

public abstract class ManagedException : Exception
{
    public string MessageResourceKey { get; }
    public Type MessageResourceType { get; }
    public IEnumerable<MessagePlaceholder> MessagePlaceholders { get; }

    public ManagedException(string messageResourceKey, Type messageResourceType, IEnumerable<MessagePlaceholder> messagePlaceholders = null)
        : base()
    {
        MessageResourceKey = messageResourceKey;
        MessageResourceType = messageResourceType;
        MessagePlaceholders = messagePlaceholders ?? new List<MessagePlaceholder>();
    }

    public ManagedException()
    {
        MessagePlaceholders = new List<MessagePlaceholder>();
    }

    public ManagedException(string message)
        : base(message)
    {
        MessagePlaceholders = new List<MessagePlaceholder>();
    }

    public bool HasResourceMessage()
    {
        return !string.IsNullOrEmpty(MessageResourceKey) && MessageResourceType != null;
    }
}

public class MessagePlaceholder
{
    private MessagePlaceholder(string plainText)
    {
        Type = MessagePlaceholderType.PlainText;
        PlainText = plainText;
    }

    private MessagePlaceholder(string resourceKey, Type resourceType)
    {
        Type = MessagePlaceholderType.Resource;
        ResourceKey = resourceKey;
        ResourceType = resourceType;
    }

    public static MessagePlaceholder CreatePlainText(string plainText)
    {
        return new MessagePlaceholder(plainText);
    }

    public static MessagePlaceholder CreateResource(string resourceKey, Type resourceType)
    {
        return new MessagePlaceholder(resourceKey, resourceType);
    }

    public MessagePlaceholderType Type { get; }
    public string ResourceKey { get; }
    public Type ResourceType { get; }
    public string PlainText { get; }
}

public enum MessagePlaceholderType
{
    Resource,
    PlainText,
}
