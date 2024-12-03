// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Infrastructure;
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
        var authorizationSettings = builder.Configuration.GetSection("Authorization").Get<AuthorizationSettings>();

        builder.Services.PostConfigure(
            (Microsoft.AspNetCore.Authorization.AuthorizationOptions options) =>
            {
                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole(authorizationSettings.Roles.AdminRole));
            }
        );
    }
}
