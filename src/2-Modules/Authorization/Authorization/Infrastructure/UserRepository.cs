// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

internal class UserRepository : MongoDbBaseAggregateRepository<User>, IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(AuthorizationDbContext dbContext)
        : base(dbContext)
    {
        _users = dbContext.GetCollection<User>();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _users.Find(e => e.Email == email).FirstOrDefaultAsync();
    }

    public bool EmailIsUnique(string userId, string email)
    {
        var user = _users.Find(e => e.Email == email).FirstOrDefault();
        return user == null || user.Id == userId;
    }

    public async Task<long> CountByRoleAsync(string role)
    {
        return await _users.CountDocumentsAsync(u => u.Roles.Contains(role));
    }
}

