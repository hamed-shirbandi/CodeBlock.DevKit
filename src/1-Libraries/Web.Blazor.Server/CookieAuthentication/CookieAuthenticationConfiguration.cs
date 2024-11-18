using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public static class CookieAuthenticationConfiguration
{
    public static void AddCookieAuthentication(this WebApplicationBuilder builder)
    {
        var authenticationSettings = builder.Configuration.GetSection("Authentication").Get<CookieAuthenticationSettings>();
        if (authenticationSettings is null)
            return;

        builder.Services.AddSingleton(authenticationSettings);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<CookieAuthenticationService>();

        builder
            .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.Cookie.SecurePolicy = CookieSecurePolicy.None;
                option.Cookie.Name = authenticationSettings.CookieName;
                option.Cookie.HttpOnly = authenticationSettings.CookieHttpOnly;
                option.Cookie.SameSite = SameSiteMode.Lax;
                option.LoginPath = authenticationSettings.LoginPath;
                option.LogoutPath = authenticationSettings.LogoutPath;
                option.ExpireTimeSpan = TimeSpan.FromMinutes(authenticationSettings.ExpireFromMinute);
                option.SlidingExpiration = authenticationSettings.SlidingExpiration;
            })
            .AddGoogle(authenticationSettings)
            .AddTwitter(authenticationSettings);
    }
}
