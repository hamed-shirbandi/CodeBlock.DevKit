using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Web.CookieAuthentication;

public static class CookieAuthenticationConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfig = configuration.GetSection("CookieAuthentication");
        if (authConfig.Value == null)
            return;

        Action<CookieAuthenticationOptions> setupAction = options =>
        {
            authConfig.Bind(options);
        };

        services.Configure(setupAction);

        var options = services.BuildServiceProvider().GetRequiredService<IOptions<CookieAuthenticationOptions>>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICookieAuthenticationService, CookieAuthenticationService>();

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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
