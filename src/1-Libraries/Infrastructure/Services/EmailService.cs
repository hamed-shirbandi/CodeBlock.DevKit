// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net;
using System.Net.Mail;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Infrastructure.Services;

internal class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;

    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        using (var client = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.SmtpPort))
        {
            client.Credentials = new NetworkCredential(_emailOptions.SmtpUser, _emailOptions.SmtpPassword);
            client.EnableSsl = _emailOptions.EnableSsl;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailOptions.SmtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
