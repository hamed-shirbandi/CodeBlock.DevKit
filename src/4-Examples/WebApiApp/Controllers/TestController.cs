using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers;

[Route("test")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : BaseApiController
{
    public TestController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public async Task<Result> Get()
    {
        var smtpServer = "mail.codeblock.dev"; // SMTP server hostname
        var smtpPort = 587; // Use 587 for TLS or 465 for SSL
        var smtpUser = "support@codeblock.dev";
        var smtpPass = "hamedHAMED99"; // Use the actual email password

        var emailSender = new EmailSender(smtpServer, smtpPort, smtpUser, smtpPass);

        await emailSender.SendEmailAsync("hamed.shirbandi@gmail.com", "Test Email", "<h1>This is a test email</h1><p>Sent from C#</p>");

        return Result.Success();
    }
}
