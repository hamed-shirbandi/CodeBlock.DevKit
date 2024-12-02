using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain.Users;

internal interface IUserRepository : IBaseAggregateRepository<User>
{
    Task<User> GetByEmailAsync(string emailOrMobile);
    public bool EmailIsUnique(string id, string email);
    Task<long> CountByRoleAsync(string name);
}
