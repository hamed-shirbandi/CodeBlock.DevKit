// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Web.Blazor.Server.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Authentication;

public static class GoogleAuthentication
{
    public static AuthenticationBuilder AddGoogle(this AuthenticationBuilder builder, AuthenticationSettings cookieAuthenticationOptions)
    {
        if (!cookieAuthenticationOptions.Google.Enabled)
            return builder;

        builder.AddGoogle(options =>
        {
            options.ClientId = cookieAuthenticationOptions.Google.ClientId;
            options.ClientSecret = cookieAuthenticationOptions.Google.ClientSecret;
            options.CallbackPath = cookieAuthenticationOptions.Google.CallbackPath;
        });

        return builder;
    }
}
