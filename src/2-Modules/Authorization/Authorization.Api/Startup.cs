// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Web.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

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

        builder.AddAdminRolePolicy(authorizationSettings.Roles.AdminRole);
    }
}
