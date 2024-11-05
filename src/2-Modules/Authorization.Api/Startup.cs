using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Authorization.Api;

public static class Startup
{
    public static void AddAuthorizationApiModule(this WebApplicationBuilder builder)
    {
        builder.AddAdminRolePolicy();
    }

    private static void AddAdminRolePolicy(this WebApplicationBuilder builder)
    {
        var authorizationOptions = builder.Configuration.GetSection("Authorization").Get<Infrastructure.AuthorizationOptions>();

        builder.Services.PostConfigure(
            (Microsoft.AspNetCore.Authorization.AuthorizationOptions options) =>
            {
                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole(authorizationOptions.AdminRole));
            }
        );
    }
}
