using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

/// <summary>
///
/// </summary>
public class AuthorizationDbContext : MongoDbContext
{
    public AuthorizationDbContext(IOptions<MongoDbOptions> mongoDbOptions)
        : base(mongoDbOptions)
    {
        Users = GetCollection<User>();
        Roles = GetCollection<Role>();
    }

    public IMongoCollection<User> Users { get; }
    public IMongoCollection<Role> Roles { get; }
}
