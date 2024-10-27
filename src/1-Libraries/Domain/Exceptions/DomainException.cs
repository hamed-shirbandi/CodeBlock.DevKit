using CodeBlock.DevKit.Core.Exceptions;

namespace CodeBlock.DevKit.Domain.Exceptions;

/// <summary>
///
/// </summary>
public class DomainException : ManagedException
{
    public DomainException(string messageResourceKey, Type messageResourceType)
        : base(messageResourceKey, messageResourceType) { }

    public DomainException() { }

    public DomainException(string message)
        : base(message) { }
}
