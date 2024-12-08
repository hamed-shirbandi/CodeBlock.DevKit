// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CodeBlock.DevKit.Web.Security;

internal static class SecurityConfiguration
{
    internal static void AddSecurity(this WebApplicationBuilder builder)
    {
        var securitySettings = builder.Configuration.GetSection("Security").Get<SecuritySettings>();
        securitySettings ??= SecuritySettings.CreateDefault();

        if (!securitySettings.Enabled)
            return;

        builder.AddGlobalFixedRateLimiter(securitySettings.RateLimiter);
    }
}
