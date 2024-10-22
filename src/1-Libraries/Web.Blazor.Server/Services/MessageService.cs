namespace CodeBlock.DevKit.Web.Blazor.Server.Services;

public class MessageService
{
    public event Action<string> OnMessage;

    public void SendMessage(string messageKey)
    {
        OnMessage?.Invoke(messageKey);
    }
}
