// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class AuthorizationSettings
{
    public AuthorizationRoleSettings Roles { get; set; }
    public AuthorizationAdminUserSettings AdminUser { get; set; }
}

public class AuthorizationAdminUserSettings
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthorizationRoleSettings
{
    public string AdminRole { get; set; }
    public string DefaultRole { get; set; }
}
