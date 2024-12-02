// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

internal class RoleRepository : MongoDbBaseAggregateRepository<Role>, IRoleRepository
{
    private readonly IMongoCollection<Role> _roles;

    public RoleRepository(AuthorizationDbContext dbContext)
        : base(dbContext)
    {
        _roles = dbContext.GetCollection<Role>();
    }

    public bool NameIsUnique(string roleId, string name)
    {
        var role = _roles.Find(e => e.Name == name).FirstOrDefault();
        return role == null || role.Id == roleId;
    }
}

