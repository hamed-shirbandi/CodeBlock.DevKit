using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Authentication;

public static class CookieAuthentication
{
    public static AuthenticationBuilder AddCookie(
        this AuthenticationBuilder builder,
        AuthenticationSettings authenticationSettings,
        IServiceCollection services
    )
    {
        services.AddScoped<CookieAuthenticationService>();

        services.Configure<AuthenticationOptions>(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        return builder.AddCookie(option =>
        {
            option.Cookie.SecurePolicy = CookieSecurePolicy.None;
            option.Cookie.Name = authenticationSettings.Cookie.CookieName;
            option.Cookie.HttpOnly = authenticationSettings.Cookie.CookieHttpOnly;
            option.Cookie.SameSite = SameSiteMode.Lax;
            option.LoginPath = authenticationSettings.Cookie.LoginPath;
            option.LogoutPath = authenticationSettings.Cookie.LogoutPath;
            option.ExpireTimeSpan = TimeSpan.FromMinutes(authenticationSettings.Cookie.ExpireFromMinute);
            option.SlidingExpiration = authenticationSettings.Cookie.SlidingExpiration;
        });
    }
}
