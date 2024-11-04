using System.Net;
using System.Net.Mail;
using CodeBlock.DevKit.Application.Srvices;

namespace CodeBlock.DevKit.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUser = smtpUser;
        _smtpPass = smtpPass;
    }

    public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        using (var client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            client.EnableSsl = false; // Ensure SSL is enabled

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
