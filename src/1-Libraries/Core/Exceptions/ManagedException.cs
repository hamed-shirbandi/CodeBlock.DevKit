namespace CodeBlock.DevKit.Core.Exceptions;

public abstract class ManagedException : Exception
{
    public string MessageResourceKey { get; }
    public Type MessageResourceType { get; }
    public Dictionary<string, Type> PlaceholderResourceKeys { get; }

    public ManagedException(string messageResourceKey, Type messageResourceType, Dictionary<string, Type> placeholderResourceKeys = null)
        : base()
    {
        MessageResourceKey = messageResourceKey;
        MessageResourceType = messageResourceType;
        PlaceholderResourceKeys = placeholderResourceKeys ?? new Dictionary<string, Type>();
    }

    public ManagedException()
    {
        PlaceholderResourceKeys = new Dictionary<string, Type>();
    }

    public ManagedException(string message)
        : base(message)
    {
        PlaceholderResourceKeys = new Dictionary<string, Type>();
    }

    public bool HasResourceMessage()
    {
        return !string.IsNullOrEmpty(MessageResourceKey) && MessageResourceType != null;
    }
}
