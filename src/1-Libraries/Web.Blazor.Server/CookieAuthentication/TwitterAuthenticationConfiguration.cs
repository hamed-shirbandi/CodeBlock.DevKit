using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public static class TwitterAuthenticationConfiguration
{
    public static AuthenticationBuilder AddTwitter(this AuthenticationBuilder builder, CookieAuthenticationSettings cookieAuthenticationOptions)
    {
        if (!cookieAuthenticationOptions.Twitter.Enabled)
            return builder;

        builder.AddTwitter(options =>
        {
            options.ConsumerKey = cookieAuthenticationOptions.Twitter.ConsumerKey;
            options.ConsumerSecret = cookieAuthenticationOptions.Twitter.ConsumerSecret;
            options.CallbackPath = cookieAuthenticationOptions.Twitter.CallbackPath;
        });

        return builder;
    }
}
