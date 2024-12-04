// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.


namespace CodeBlock.DevKit.Web.Api.JwtAuthentication;

public interface IJwtAuthenticationService
{
    string GenerateJwtToken(string userId, string userName, IEnumerable<string> roles);
}
