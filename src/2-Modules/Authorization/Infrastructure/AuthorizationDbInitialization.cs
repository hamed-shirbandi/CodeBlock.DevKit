using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

/// <summary>
///
/// </summary>
public static class AuthorizationDbInitialization
{
    /// <summary>
    ///
    /// </summary>
    public static void InitialAuthorizationDb(this IServiceProvider serviceProvider)
    {
        serviceProvider.SeedEssentialData();
        serviceProvider.CreateIndexes();
    }

    /// <summary>
    /// Seed the necessary data that system needs
    /// </summary>
    private static void SeedEssentialData(this IServiceProvider serviceProvider)
    {
        SeedSuperUser(serviceProvider);
    }

    /// <summary>
    ///
    /// </summary>
    private static void SeedSuperUser(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AuthorizationDbContext>();
        var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();

        if (configuration["SuperUser:Email"] == null)
            return;

        if (dbContext.Users.Find(u => u.Email == configuration["SuperUser:Email"]).Any())
            return;

        var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();
        var encryptionService = serviceScope.ServiceProvider.GetService<IEncryptionService>();

        var user = User.Register(
            userRepository,
            encryptionService,
            configuration["SuperUser:Mobile"],
            configuration["SuperUser:Email"],
            configuration["SuperUser:Password"]
        );
        userRepository.AddAsync(user).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Create index for collections
    /// </summary>
    private static void CreateIndexes(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AuthorizationDbContext>();

        dbContext.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(x => x.Email),
                new CreateIndexOptions() { Name = nameof(User.Email), Unique = false }
            )
        );

        dbContext.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(x => x.Mobile),
                new CreateIndexOptions() { Name = nameof(User.Mobile), Unique = false }
            )
        );
    }
}
