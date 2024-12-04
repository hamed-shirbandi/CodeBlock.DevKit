// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Infrastructure.Models;

internal class EmailOptions
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPassword { get; set; }
    public bool EnableSsl { get; set; }
}
