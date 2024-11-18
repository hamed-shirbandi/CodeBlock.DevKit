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
