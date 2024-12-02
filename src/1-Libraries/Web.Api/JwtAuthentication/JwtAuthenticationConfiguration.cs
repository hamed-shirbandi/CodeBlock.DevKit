// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CodeBlock.DevKit.Web.Api.JwtAuthentication;

public static class JwtAuthenticationConfiguration
{
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        var authConfig = builder.Configuration.GetSection("JwtAuthentication");
        if (!authConfig.Exists())
            return;

        Action<JwtAuthenticationOptions> setupAction = authConfig.Bind;

        builder.Services.Configure(setupAction);

        var options = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtAuthenticationOptions>>();

        builder.Services.TryAddSingleton<JwtAuthenticationService>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Value.Issuer,
                    ValidAudience = options.Value.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
    }
}

