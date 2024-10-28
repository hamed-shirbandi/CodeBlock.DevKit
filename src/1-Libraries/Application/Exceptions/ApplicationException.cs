using CodeBlock.DevKit.Core.Exceptions;

namespace CodeBlock.DevKit.Application.Exceptions;

public class ApplicationException : ManagedException
{
    public ApplicationException(string messageResourceKey, Type messageResourceType, Dictionary<string, Type> placeholderResourceKeys = null)
        : base(messageResourceKey, messageResourceType, placeholderResourceKeys) { }

    public ApplicationException(string message)
        : base(message) { }
}
