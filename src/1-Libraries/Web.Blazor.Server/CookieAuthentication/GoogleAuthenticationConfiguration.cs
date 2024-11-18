using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public static class GoogleAuthenticationConfiguration
{
    public static AuthenticationBuilder AddGoogle(this AuthenticationBuilder builder, CookieAuthenticationSettings cookieAuthenticationOptions)
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
