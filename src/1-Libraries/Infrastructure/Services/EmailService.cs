using System.Net;
using System.Net.Mail;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSetting _emailSetting;

    public EmailService(IOptions<EmailSetting> emailSetting)
    {
        _emailSetting = emailSetting.Value;
    }

    public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        using (var client = new SmtpClient(_emailSetting.SmtpServer, _emailSetting.SmtpPort))
        {
            client.Credentials = new NetworkCredential(_emailSetting.SmtpUser, _emailSetting.SmtpPassword);
            client.EnableSsl = _emailSetting.EnableSsl;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSetting.SmtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
