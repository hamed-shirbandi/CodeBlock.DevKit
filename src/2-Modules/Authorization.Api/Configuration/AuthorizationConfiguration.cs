using CodeBlock.DevKit.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Authorization.Api.Configuration;

public static class AuthorizationConfiguration
{
    public static void AddAuthorizationApiModule(this WebApplicationBuilder builder)
    {
        builder.AddAdminRolePolicy();
    }

    private static void AddAdminRolePolicy(this WebApplicationBuilder builder)
    {
        var authorizationSettings = builder.Configuration.GetSection("Authorization").Get<AuthorizationSettings>();

        builder.Services.PostConfigure<AuthorizationOptions>(options =>
        {
            options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole(authorizationSettings.AdminRole));
        });
    }
}
