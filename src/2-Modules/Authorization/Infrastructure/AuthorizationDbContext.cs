using CodeBlock.DevKit.Authorization.Domain;
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
    }

    public IMongoCollection<User> Users { get; }
}
