using CodeBlock.DevKit.Core.Exceptions;

namespace CodeBlock.DevKit.Domain.Exceptions;

/// <summary>
///
/// </summary>
public class DomainException : ManagedException
{
    public DomainException(string messageResourceKey, Type messageResourceType, IEnumerable<MessagePlaceholder> messagePlaceholders = null)
        : base(messageResourceKey, messageResourceType, messagePlaceholders) { }

    public DomainException(string message)
        : base(message) { }
}
