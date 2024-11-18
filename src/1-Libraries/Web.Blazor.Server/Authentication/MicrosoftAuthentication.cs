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
            options.ClientId = cookieAuthenticationOptions.Google.ClientId;
            options.ClientSecret = cookieAuthenticationOptions.Google.ClientSecret;
            options.CallbackPath = cookieAuthenticationOptions.Google.CallbackPath;
        });

        return builder;
    }
}
