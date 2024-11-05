using CodeBlock.DevKit.Authorization.Domain;
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
        services.AddAddAuthorizationOptions(configuration);
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

    private static void AddAddAuthorizationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfig = configuration.GetSection("Authorization");
        if (!authConfig.Exists())
            return;

        Action<AuthorizationOptions> setupAction = authConfig.Bind;

        services.Configure(setupAction);
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
    }
}
