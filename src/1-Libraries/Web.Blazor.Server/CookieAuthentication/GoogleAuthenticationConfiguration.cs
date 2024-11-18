using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public static class GoogleAuthenticationConfiguration
{
    public static void AddGoogleAuthentication(this AuthenticationBuilder builder, CookieAuthenticationSettings cookieAuthenticationOptions)
    {
        if (!cookieAuthenticationOptions.Google.Enabled)
            return;

        builder.AddGoogle(options =>
        {
            options.ClientId = cookieAuthenticationOptions.Google.ClientId;
            options.ClientSecret = cookieAuthenticationOptions.Google.ClientSecret;
            options.CallbackPath = cookieAuthenticationOptions.Google.CallbackPath;
        });
    }
}
