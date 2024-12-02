using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain.Roles;

public interface IRoleRepository : IBaseAggregateRepository<Role>
{
    bool NameIsUnique(string id, string name);
}
