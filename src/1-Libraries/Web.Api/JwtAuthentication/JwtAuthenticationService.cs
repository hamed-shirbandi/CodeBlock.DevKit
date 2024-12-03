// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CodeBlock.DevKit.Web.Api.JwtAuthentication;

public class JwtAuthenticationService
{
    private readonly JwtAuthenticationOptions _options;

    public JwtAuthenticationService(IOptions<JwtAuthenticationOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateJwtToken(string userId, string userName, IEnumerable<string> roles)
    {
        var claims = GetClaims(userId, userName, roles);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_options.ExpireDays));

        var token = new JwtSecurityToken(_options.Issuer, _options.Issuer, claims, expires: expires, signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GetClaims(string userId, string userName, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}
