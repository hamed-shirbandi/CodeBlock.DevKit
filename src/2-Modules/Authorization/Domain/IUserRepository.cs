using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain;

public interface IUserRepository : IBaseAggregateRepository<User>
{
    Task<User> GetByEmailOrMobileAsync(string emailOrMobile);
    bool MobileIsUnique(string id, string mobile);
    public bool EmailIsUnique(string id, string email);
}
