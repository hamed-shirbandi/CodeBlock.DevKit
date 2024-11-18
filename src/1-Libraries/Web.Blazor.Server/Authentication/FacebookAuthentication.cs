using CodeBlock.DevKit.Web.Blazor.Server.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Authentication;

public static class FacebookAuthentication
{
    public static AuthenticationBuilder AddFacebook(this AuthenticationBuilder builder, AuthenticationSettings cookieAuthenticationOptions)
    {
        if (!cookieAuthenticationOptions.Facebook.Enabled)
            return builder;

        builder.AddFacebook(options =>
        {
            options.AppId = cookieAuthenticationOptions.Facebook.AppId;
            options.AppSecret = cookieAuthenticationOptions.Facebook.AppSecret;
            options.CallbackPath = cookieAuthenticationOptions.Facebook.CallbackPath;
        });

        return builder;
    }
}
