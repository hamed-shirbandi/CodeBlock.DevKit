// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Security;

public static class Policies
{
    public const string ADMIN_ROLE = "AdminRole";

    public static void AddAdminRolePolicy(this WebApplicationBuilder builder, string adminRoleName)
    {
        builder.Services.PostConfigure(
            (AuthorizationOptions options) =>
            {
                options.AddPolicy(ADMIN_ROLE, policy => policy.RequireRole(adminRoleName));
            }
        );
    }
}
