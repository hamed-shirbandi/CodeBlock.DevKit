// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Application.Srvices;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);
}
