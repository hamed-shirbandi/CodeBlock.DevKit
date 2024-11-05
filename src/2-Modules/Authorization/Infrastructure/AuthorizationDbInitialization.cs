using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public static class AuthorizationDbInitialization
{
    public static void InitialAuthorizationDb(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AuthorizationDbContext>();

        SeedEssentialData(serviceScope, dbContext);

        CreateIndexes(dbContext);
    }

    private static void SeedEssentialData(IServiceScope serviceScope, AuthorizationDbContext dbContext)
    {
        var authorizationOptions = serviceScope.ServiceProvider.GetService<IOptions<AuthorizationOptions>>();

        if (authorizationOptions == null)
            return;

        serviceScope.SeedDefaultRole(dbContext, authorizationOptions.Value);
        serviceScope.SeedAdminRole(dbContext, authorizationOptions.Value);
        serviceScope.SeedAdminUser(dbContext, authorizationOptions.Value);
    }

    private static void SeedAdminUser(this IServiceScope serviceScope, AuthorizationDbContext dbContext, AuthorizationOptions authorizationOptions)
    {
        if (dbContext.Users.Find(u => u.Email == authorizationOptions.AdminUser.Email).Any())
            return;

        var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();
        var passwordService = serviceScope.ServiceProvider.GetService<IPasswordService>();

        var user = User.Register(userRepository, passwordService, authorizationOptions.AdminUser.Email, authorizationOptions.AdminUser.Password);

        user.AddRole(authorizationOptions.AdminRole);

        userRepository.AddAsync(user).GetAwaiter().GetResult();
    }

    private static void SeedDefaultRole(this IServiceScope serviceScope, AuthorizationDbContext dbContext, AuthorizationOptions authorizationOptions)
    {
        serviceScope.CreateRole(dbContext, authorizationOptions.DefaultRole);
    }

    private static void SeedAdminRole(this IServiceScope serviceScope, AuthorizationDbContext dbContext, AuthorizationOptions authorizationOptions)
    {
        serviceScope.CreateRole(dbContext, authorizationOptions.AdminRole);
    }

    private static void CreateRole(this IServiceScope serviceScope, AuthorizationDbContext dbContext, string roleName)
    {
        if (dbContext.Roles.Find(u => u.Name == roleName).Any())
            return;

        var roleRepository = serviceScope.ServiceProvider.GetService<IRoleRepository>();

        var role = Role.Create(roleRepository, roleName);
        roleRepository.AddAsync(role).GetAwaiter().GetResult();
    }

    private static void CreateIndexes(AuthorizationDbContext dbContext)
    {
        dbContext.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(x => x.Email),
                new CreateIndexOptions() { Name = nameof(User.Email), Unique = true }
            )
        );

        dbContext.Roles.Indexes.CreateOneAsync(
            new CreateIndexModel<Role>(
                Builders<Role>.IndexKeys.Ascending(x => x.Name),
                new CreateIndexOptions() { Name = nameof(Role.Name), Unique = true }
            )
        );
    }
}
