using CodeBlock.DevKit.Web.Blazor.Server.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Authentication;

public static class TwitterAuthentication
{
    public static AuthenticationBuilder AddTwitter(this AuthenticationBuilder builder, AuthenticationSettings cookieAuthenticationOptions)
    {
        if (!cookieAuthenticationOptions.Twitter.Enabled)
            return builder;

        builder.AddTwitter(options =>
        {
            options.ConsumerKey = cookieAuthenticationOptions.Twitter.ConsumerKey;
            options.ConsumerSecret = cookieAuthenticationOptions.Twitter.ConsumerSecret;
            options.CallbackPath = cookieAuthenticationOptions.Twitter.CallbackPath;

            options.RetrieveUserDetails = true;
        });

        return builder;
    }
}
