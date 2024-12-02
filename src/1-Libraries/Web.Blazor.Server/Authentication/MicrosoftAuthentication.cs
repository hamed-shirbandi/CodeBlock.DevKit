// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Authentication;

public static class MicrosoftAuthentication
{
    public static AuthenticationBuilder AddMicrosoft(this AuthenticationBuilder builder, AuthenticationSettings cookieAuthenticationOptions)
    {
        if (!cookieAuthenticationOptions.Microsoft.Enabled)
            return builder;

        builder.AddMicrosoftAccount(options =>
        {
            options.ClientId = cookieAuthenticationOptions.Microsoft.ClientId;
            options.ClientSecret = cookieAuthenticationOptions.Microsoft.ClientSecret;
            options.CallbackPath = cookieAuthenticationOptions.Microsoft.CallbackPath;
        });

        return builder;
    }
}

