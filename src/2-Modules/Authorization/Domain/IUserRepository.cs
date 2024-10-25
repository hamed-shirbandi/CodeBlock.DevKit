using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain;

public interface IUserRepository : IBaseAggregateRepository<User>
{
    Task<User> GetByEmailAsync(string emailOrMobile);
    public bool EmailIsUnique(string id, string email);
}
