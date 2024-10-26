using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;

public static class CookieAuthenticationConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddCookieAuthentication(this WebApplicationBuilder builder)
    {
        var authConfig = builder.Configuration.GetSection("CookieAuthentication");
        if (!authConfig.Exists())
            return;

        Action<CookieAuthenticationOptions> setupAction = authConfig.Bind;

        builder.Services.Configure(setupAction);

        var options = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<CookieAuthenticationOptions>>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<CookieAuthenticationService>();

        builder
            .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.Cookie.SecurePolicy = CookieSecurePolicy.None; // env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                option.Cookie.Name = options.Value.CookieName;
                option.Cookie.HttpOnly = options.Value.CookieHttpOnly;
                option.Cookie.SameSite = SameSiteMode.Lax;
                option.LoginPath = options.Value.LoginPath;
                option.LogoutPath = options.Value.LogoutPath;
                option.ExpireTimeSpan = TimeSpan.FromMinutes(options.Value.ExpireFromMinute);
                option.SlidingExpiration = options.Value.SlidingExpiration;
            });
    }
}
