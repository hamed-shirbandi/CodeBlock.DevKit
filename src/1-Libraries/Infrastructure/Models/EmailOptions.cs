namespace CodeBlock.DevKit.Infrastructure.Models;

public class EmailOptions
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPassword { get; set; }
    public bool EnableSsl { get; set; }
}
