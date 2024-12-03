// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain.Users;

public interface IUserRepository : IBaseAggregateRepository<User>
{
    Task<User> GetByEmailAsync(string emailOrMobile);
    public bool EmailIsUnique(string id, string email);
    Task<long> CountByRoleAsync(string name);
}
