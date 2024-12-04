// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CodeBlock.DevKit.Web.Api.Swagger;

internal static class SwaggerConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddSwaggerPreConfigured(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerOptions = configuration.GetSection("Swagger").Get<SwaggerOptions>();
        if (swaggerOptions == null)
            return;

        // Register the Swagger generator, defining one or more Swagger documents
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            //swagger doc info
            c.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo { Title = swaggerOptions.Title, Version = swaggerOptions.Version });

            //include xml comments from xml files referred in appsetting
            var xmlFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml");

            foreach (var xmlFile in xmlFiles)
                c.IncludeXmlComments(xmlFile);

            //define Bearer security
            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                }
            );
            //add Bearer input to swagger ui to authorize by a jwt token that get from login api
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        },
                        Array.Empty<string>()
                    },
                }
            );
        });
    }

    /// <summary>
    ///
    /// </summary>
    public static void UseSwaggerPreConfigured(this WebApplication app, IConfiguration configuration)
    {
        var swaggerOptions = configuration.GetSection("Swagger").Get<SwaggerOptions>();
        if (swaggerOptions == null)
            return;

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/" + swaggerOptions.Version + "/swagger.json", swaggerOptions.Version);
            //redirect root url to swagger ui
            c.RoutePrefix = "";
        });
    }
}
