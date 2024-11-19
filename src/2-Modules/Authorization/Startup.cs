using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Authorization;

public static class Startup
{
    public static void AddAuthorizationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(handlerAssemblyMarkerTypes: typeof(Startup));
        services.AddMongoDbContext(configuration);
        services.AddAuthorizationSettings(configuration);
        services.AddRepositories();
        services.AddDomainServices();
        services.AddMapper();
    }

    /// <summary>
    ///
    /// </summary>
    private static void AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("MongoDB");
        services.AddScoped<AuthorizationDbContext>().AddOptions<MongoDbOptions>().Bind(options);
    }

    /// <summary>
    ///
    /// </summary>
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }

    private static void AddAuthorizationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var authorizationSettings = configuration.GetSection("Authorization").Get<AuthorizationSettings>();
        if (authorizationSettings is null)
            return;

        services.AddSingleton<AuthorizationSettings>();
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
    }
}
