namespace CodeBlock.DevKit.Application.Srvices;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);
}
